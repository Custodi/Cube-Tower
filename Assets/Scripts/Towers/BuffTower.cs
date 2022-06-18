using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerView))]
[SelectionBase]
public class BuffTower : Tower
{ 
    private LinkedList<GameObject> _towersInRange = new LinkedList<GameObject>();
    public LinkedList<GameObject> towersInRange { get => _towersInRange; }

    [SerializeField]
    private IEffectableTower.TowerEffect _selectedEffect;

    [SerializeField]
    private TowerEffect[] _towerEffects;

    [SerializeField]
    private SphereCollider _buffCollider;


    private float _rangeScaler = 1f;
    protected override void Start()
    {
        currentTowerLevel = 0;
        Instantiate(upgradeList[0]);
        base.Start();
        ToggleObject(false);
    }

    public void Instantiate(TowerUpgradeConfiguration config)
    {
        towerRadius = config.radius;
        price = config.price;

        var radius = towerRadius * _rangeScaler;
        _buffCollider.radius = radius;
        _visualRadius.GetComponent<RectTransform>().localScale = new Vector3(radius, radius, 1f);
    }

    private void Update()
    {
        //Debug.Log(_towersInRange.Count);
        /*foreach(GameObject item in _towerEffects)
        {
            Debug.Log(item.name);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.TryGetComponent(out ShootingTower _towerScript))
        {
            _towerScript.AddEffect(GetSelectedEffect());
            _towersInRange.AddLast(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ShootingTower _towerScript))
        {
            //Debug.Log("Remove " + other.gameObject);
            _towerScript.RemoveEffect(GetSelectedEffect());
            _towersInRange.Remove(other.gameObject);
        }
    }

    public override void Upgrade()
    {
        if (isUpgradeAvailable && LevelController.instance.LevelEconomics.TryTakeMoney(upgradeList[currentTowerLevel + 1].price, out _))
        {
            upgradeList[currentTowerLevel].levelTowerPrefab.SetActive(false);
            currentTowerLevel++;
            upgradeList[currentTowerLevel].levelTowerPrefab.SetActive(true);
            Instantiate(upgradeList[currentTowerLevel]);
        }
    }

    public override void Sell()
    {
        LevelController.instance.LevelEconomics.AddMoney((upgradeList[currentTowerLevel].price / 2));
        Destroy(gameObject);
    }

    public override void ToggleObject(bool newState)
    {
        if (newState)
        {
            gameObject.layer = 6;
            _buffCollider.enabled = true;
            _visualRadius.SetActive(false);
            enabled = true;
            //base.ToggleObject(newState);
        }
        else
        {
            gameObject.layer = 2;
            _buffCollider.enabled = false;
            enabled = false;
            //base.ToggleObject(newState);
        }
    }
    private void SetNewEffect(IEffectableTower.TowerEffect towerEffect)
    {
        ShootingTower towerScript = null;
        try
        {
            foreach(GameObject item in _towersInRange)
            {
                towerScript = item.GetComponent<ShootingTower>();
                towerScript.RemoveEffect(GetSelectedEffect());
                towerScript.AddEffect(_towerEffects[(int)towerEffect]);
            }
        }
        catch
        {

        }

        _selectedEffect = towerEffect;
    }

    private TowerEffect GetSelectedEffect()
    {
        return _towerEffects[(int)_selectedEffect];
    }
}
