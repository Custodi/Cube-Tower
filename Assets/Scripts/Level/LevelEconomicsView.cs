using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEconomicsView : View
{
    public int Money { get => _levelEconomics.Money; }

    private LevelEconomics _levelEconomics;

    void Start()
    {
        _levelEconomics = GetComponent<LevelEconomics>();
        _levelEconomics.OnMoneyUpdate += OnMoneyUpdate;
        LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.MoneyUpdate, this);
    }

    private void OnMoneyUpdate(int oldValue, int newValue)
    {
        LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.MoneyUpdate, this);
        //Debug.Log("Money: " + Money);
    }

    private void OnDestroy()
    {
        _levelEconomics.OnMoneyUpdate -= OnMoneyUpdate;
    }

}
