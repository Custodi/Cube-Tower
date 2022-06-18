using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryProjectile : Projectile
{
    [Range(0f, 1f)]
    [SerializeField]
    private float _damageReduction;

    [SerializeField]
    private float _damageRadius;

    [SerializeField]
    private float _additionalGravity;
    

    private Rigidbody _rigidbody;
    private float _gravity;
    private float _velocity;
    private float _pathToTarget;
    private float _attackRadians;
    private float _damageMultiplier;
    private float a, b;
    private LayerMask _mask;
    private Vector3 _fromTo, _initShootVector;
    private Collider[] _enemyColliders;

    //private LinkedList<GameObject> _enemiesInRange;

    private float _baseDamage;
    private int _damageToEnemy;

    private void Start()
    {
        _mask = LayerMask.GetMask("Enemy");
    }

    public override void Instantiate(float damageMultiplier, float speed, GameObject hitGoalEnemy)
    {
        this.speed = speed;
        this.hitGoalEnemy = hitGoalEnemy;

        damage = (int)(damage * damageMultiplier);
        _baseDamage = damage * damageMultiplier * (1 - _damageReduction);

        _gravity = Physics.gravity.y;
        _rigidbody = GetComponent<Rigidbody>();
        _damageMultiplier = damageMultiplier;
        
        _initShootVector = transform.localEulerAngles * Mathf.Deg2Rad;
        _attackRadians = _initShootVector.z;

        hitGoalPosition = hitGoalEnemy.transform.position;
        Shoot();
    }
    private void Shoot()
    {
        _fromTo = hitGoalPosition - transform.position;
        _pathToTarget = new Vector3(_fromTo.x, 0f, _fromTo.z).magnitude;

        a = _pathToTarget * _pathToTarget * _gravity;
        b = 2 * (_fromTo.y - Mathf.Tan(_attackRadians) * _pathToTarget) * Mathf.Cos(_attackRadians) * Mathf.Cos(_attackRadians);

        _velocity = Mathf.Sqrt(Mathf.Abs(a / b));

        _initShootVector = Vector3.RotateTowards(transform.up, _initShootVector, 0.01f, 0.01f);

        _rigidbody.velocity = _velocity * _initShootVector;
        _rigidbody.AddForce(new Vector3(0f, _additionalGravity, 0f), ForceMode.Force);
    }

    private void Explode()
    {
        _enemyColliders = Physics.OverlapSphere(transform.position, _damageRadius, _mask);
        //Debug.Log(_enemyColliders.Length);
        try
        {
            foreach(Collider item in _enemyColliders)
            {
                _damageToEnemy = (int)((_damageRadius - Vector3.Distance(item.transform.position, transform.position) / _damageRadius) * damage + _baseDamage);
                item.GetComponent<Enemy>().HitEnemy(_damageToEnemy);
            }
        }
        catch
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy _) || other.TryGetComponent(out Earth _))
        {
            Explode();
            Destroy(gameObject);
        }
    }
}
