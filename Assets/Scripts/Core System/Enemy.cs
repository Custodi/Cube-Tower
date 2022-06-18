using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(DamageDealer))] // deal damage to level health
[RequireComponent(typeof(DamageConsumer))] // get damage from tower
[RequireComponent(typeof(BuffableEnemy))]
[RequireComponent(typeof(EnemyView))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _root;

    public float speed { get; set; }
    public new string name { get; set; }
    public int reward { get; set; }

    private DamageConsumer _damageConsumer;
    public DamageConsumer DamageConsumer { get => _damageConsumer; private set => _damageConsumer = value; }

    private DamageDealer _damageDealer;
    private BuffableEnemy _buffableEntity;
    private Transform[] _waypoints;

    private int _currentWaypointIndex;
    private Vector3 _movementVector = Vector3.zero;

    public delegate void EnemyDied(GameObject deadEnemy);
    public event EnemyDied OnEnemyDied;


    private float _speedScaler = 1f; // Vector scaler of 1, movement step between frames

    private void Awake()
    {
        _damageConsumer = GetComponent<DamageConsumer>();
        _damageConsumer.OnDied += Die;
        _damageConsumer.OnTakeDamage += TakeDamage;

        _damageDealer = GetComponent<DamageDealer>();
        _buffableEntity = GetComponent<BuffableEnemy>();
    }

    public void Instantiate(EnemyConfiguration config, Transform[] waypoints, Quaternion initialRotation)
    {
        name = config.name;
        speed = config.speed;
        reward = config.reward;
        _waypoints = waypoints;
        _currentWaypointIndex = 0;
        _damageDealer.Instantiate(LevelController.instance.LevelHealth.gameObject, config.damage);
        _root.transform.rotation = initialRotation;
        _damageConsumer.Instantiate(config.health);
    }

    private void Update()
    {
        _movementVector = (_waypoints[_currentWaypointIndex].position - transform.position).normalized * _speedScaler * speed * Time.deltaTime;
        transform.Translate(_movementVector);
        //Debug.Log(gameObject.name + " || " + speed);
    }

    private void TakeDamage(int takenDamage)
    {
        //Debug.Log("Math health: " + _damageConsumer.CurrentHealth);
    }

    public void HitEnemy(int takenDamage)
    {
        _damageConsumer.TakeUntargetDamage(takenDamage);
    }

    public void ReceiveProjectile(Projectile projectile)
    {
        //Debug.Log(projectile.damageDealer.Damage);
        if (projectile.effectList.Count > 0)
        {
            foreach (var item in projectile.effectList)
            {
                _buffableEntity.AddEffect(item);
                //Debug.Log("MySpeed:" + speed);
            }
        }
        _damageConsumer.TakeDamage(projectile.damageDealer);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _waypoints[_currentWaypointIndex].gameObject)
        {
            RotateTo(_waypoints[_currentWaypointIndex].gameObject);
            _currentWaypointIndex++;
        }

        else if(collider.TryGetComponent(out Projectile projectile))
        {
            if(projectile.targetEnemy == gameObject)
            {
                ReceiveProjectile(projectile);
            }
        }
    }

    private void RotateTo(GameObject waypoint)
    {
        //Debug.Log(waypoint.transform.rotation.eulerAngles);
        _root.transform.DORotateQuaternion(waypoint.transform.rotation, 1f);
    }

    private void Die()
    {
        LevelController.instance.LevelEconomics.AddMoney(reward);
        StartCoroutine(DieCoroutine());
    }

    private void OnDestroy()
    {
        _damageConsumer.OnDied -= Die;
        _damageConsumer.OnTakeDamage -= TakeDamage;
    }

    IEnumerator DieCoroutine()
    {
        OnEnemyDied?.Invoke(gameObject);
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
        yield return null;
    }
}
