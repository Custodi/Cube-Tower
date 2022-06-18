using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonTowerView : TowerView
{
    /*public new string name { get; private set; }
    
    public float attackSpeed { get; private set; }*/

    private Tower _tower;
    //private DamageConsumer _damageConsumer;

    private void Start()
    {
        _tower = GetComponent<Tower>();
    }

    public void OnClicked()
    {
        //_tower.towerType
    }

    public void OnSelected()
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnselected()
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnclicked()
    {

    }
}
