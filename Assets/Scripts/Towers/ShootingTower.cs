using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerView))]
[SelectionBase]
public class ShootingTower : BuffableTower
{
    [SerializeField]
    private GameObject[] _projectilesPrefab;

    [SerializeField]
    private Transform _projectileInitPosition;

    [SerializeField]
    private SphereCollider _attackCollider;

    private float _fireRate;
    public float fireRate
    {
        get
        {
            return _fireRate;
        }
        set
        {
            if(value <= 0)
            {
                throw new System.Exception("Zero _fireRate division exception");
            }
            else
            {
                _fireRate = value;
                _fireTime = 1f / _fireRate;
            }
        }
    }

    private float _damageMultiplier;
    public float damageMultiplier { get => _damageMultiplier; set => _damageMultiplier = value; }

    private float _rangeScaler = 1f;
    private Enemy _enemyScript; // Rid of
    private float _fireTime;
    private int _currentProjectileIndex = 0;

    private LinkedList<GameObject> _enemiesInRange = new LinkedList<GameObject>();
    public LinkedList<GameObject> enemiesInRange { get => _enemiesInRange; }

    protected override void Start()
    {
        currentTowerLevel = 0;
        Instantiate(upgradeList[0]);
        _damageMultiplier = 1f;
        base.Start();
        ToggleObject(false);
    }

    public void Instantiate(TowerUpgradeConfiguration config)
    {
        towerRadius = config.radius;
        fireRate = config.fireRate;
        price = config.price;
        //Debug.Log("Price in shooting:" + price);
        var radius = towerRadius * _rangeScaler;
        _attackCollider.radius = radius;
        _visualRadius.GetComponent<RectTransform>().localScale = new Vector3(radius, radius, 1f);
        _fireTime = 1f / fireRate;
    }

    private void Update()
    {
        //Debug.Log("Enemy count:" + _enemiesInRange.Count);
        if (_fireTime <= 0)
        {
            if(_enemiesInRange.Count > 0) Shoot();
            _fireTime = 1f / fireRate; 
        }
        else _fireTime -= Time.deltaTime;
        //Debug.Log("Effect count:" + effectList.Count);
        //Debug.Log("Fire rate:" + _fireRate);
    }

    public override void Upgrade()
    {
        if(isUpgradeAvailable && LevelController.instance.LevelEconomics.TryTakeMoney(upgradeList[currentTowerLevel+1].price, out _))
        {
            upgradeList[currentTowerLevel].levelTowerPrefab.SetActive(false);
            currentTowerLevel++;
            upgradeList[currentTowerLevel].levelTowerPrefab.SetActive(true);
            Instantiate(upgradeList[currentTowerLevel]);
            _currentProjectileIndex++;
        }
    }

    public override void Sell()
    {
        LevelController.instance.LevelEconomics.AddMoney((upgradeList[currentTowerLevel].price / 2));
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out _enemyScript))
        {
            //Debug.Log("Add " + other.gameObject);
            _enemyScript.OnEnemyDied += RefreshEnemyList;
            _enemiesInRange.AddLast(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out _enemyScript))
        {
            //Debug.Log("Remove " + other.gameObject);
            _enemyScript.OnEnemyDied -= RefreshEnemyList;
            _enemiesInRange.Remove(other.gameObject);
        }
    }

    private void Shoot()
    {
        //Debug.Log("Count: " + _enemiesInRange.Count + " || Value: " + _enemiesInRange.First?.Value);
        if (_enemiesInRange.Count > 0)
        {
            GameObject projectile = Instantiate(_projectilesPrefab[_currentProjectileIndex], _projectileInitPosition.position, _projectileInitPosition.rotation, transform); // cash
            //Debug.Log(projectile);
            //Debug.Log(_enemiesInRange.First.Value);
            //Debug.Log(projectile);
            projectile.GetComponent<Projectile>().Instantiate(_damageMultiplier, 0.2f, _enemiesInRange.First.Value);
            //Debug.Log(_enemiesInRange.First.Value.transform.position);
        }
    }

    private void RefreshEnemyList(GameObject deadEnemy)
    {
        //Debug.Log(1);
        _enemiesInRange.Remove(deadEnemy);
        if (deadEnemy.TryGetComponent(out _enemyScript))
        {
            _enemyScript.OnEnemyDied -= RefreshEnemyList;
        }
    }

    public override void ToggleObject(bool newState)
    {
        if(newState)
        {
            //Debug.Log("E:" + price);
            gameObject.layer = 6;
            _attackCollider.enabled = true;
            _visualRadius.SetActive(false);
            enabled = true;
            base.ToggleObject(newState);
        }
        else
        {
            //Debug.Log("D:" + price);
            gameObject.layer = 2;
            _attackCollider.enabled = false;
            enabled = false;
            base.ToggleObject(newState);
        }
    }
}
