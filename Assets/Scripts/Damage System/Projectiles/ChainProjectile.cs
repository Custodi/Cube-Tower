using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainProjectile : Projectile
{
    [SerializeField]
    private int _targetCount; // Enemy count to effect

    [Range(0f, 1f)]
    [SerializeField]
    private float _chainReduction;

    [SerializeField]
    private float _chainRadius;

    private bool _isChild = false;

    private Collider[] _enemyColliders;
    private LinkedList<GameObject> _enemiesInChainRadius;

    private LayerMask _mask;
    public override void Instantiate(float damageMultiplier, float speed, GameObject hitGoalEnemy)
    {
        this.speed = speed;
        this.hitGoalEnemy = hitGoalEnemy;
        damageDealer.Instantiate(this.hitGoalEnemy, (int)(damage*damageMultiplier));

        _enemiesInChainRadius = new LinkedList<GameObject>();
        _mask = LayerMask.GetMask("Enemy");
    }

    public void Instantiate(float speed, GameObject hitGoalEnemy, bool isChild)
    {
        _isChild = isChild;
        Instantiate(1f, speed, hitGoalEnemy);
    }

    private void Update()
    {
        if (hitGoalEnemy)
        {
            hitGoalPosition = hitGoalEnemy.transform.position + fixedVector;
            transform.Translate((hitGoalPosition - transform.position).normalized * speed);
        }
        else
        {
            //Debug.Log("Destroyed cuz hit goal enemy");
            Destroy(gameObject);
        }
        
    }

    private void Spark(Transform transform)
    {
        _enemyColliders = Physics.OverlapSphere(transform.position, _chainRadius, _mask);
        for(int i = 0, availableTargers = _enemyColliders.Length; i < _targetCount && availableTargers > 0; i++, availableTargers--)
        {
            //Debug.Log("Enemies in range: " + _enemyColliders.Length);
            //Debug.Log(_enemyColliders[i].gameObject.name);
            if (_enemyColliders[i].transform == hitGoalEnemy.transform) continue;
            else
            {
                GameObject projectile = Instantiate(gameObject, transform.position + fixedVector, transform.rotation, _enemyColliders[i].transform);
                //Debug.Log(projectile);
                projectile.GetComponent<ChainProjectile>().Instantiate(0.2f, _enemyColliders[i].gameObject, true);
                //Debug.Log(1);
            }
        }
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hitGoalEnemy)
        {
            if (_isChild == false)
            {
                Spark(hitGoalEnemy.transform);
                Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}
