using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerView : View
{
    [SerializeField]
    private GameObject _towerSubmenu;
    public bool isUpgradeAvailable { get => _tower.isUpgradeAvailable; }

    private Tower _tower;
    private void Start()
    {
        _tower = GetComponent<Tower>();
    }

    public void SellTower()
    {
        _tower.Sell();
    }

    public void UpgradeTower()
    {
        //Debug.Log("upgrade me");
        _tower.Upgrade();
    }

    public int GetNextPrice()
    {
        if(_tower.isUpgradeAvailable)
        {
            return _tower.upgradeList[_tower.currentTowerLevel + 1].price;
        }
        else
        {
            return 0;
        }
    }
}
