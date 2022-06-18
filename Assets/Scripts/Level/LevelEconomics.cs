using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelEconomicsView))]
public class LevelEconomics : MonoBehaviour
{
    [SerializeField]
    private int _money;

    public delegate void MoneyUpdateEvent(int oldValue, int newValue);

    public MoneyUpdateEvent OnMoneyUpdate;

    public int Money
    {
        get
        {
            return _money;
        }
        private set
        {
            if(_money < 0)
            {
                throw new System.Exception("Try to set money value below 0");
            }
            else
            {
                OnMoneyUpdate?.Invoke(_money, _money + value);
                _money = value;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CompareMoney(int takenMoney)
    {
        return takenMoney <= _money;
    }

    public bool TryTakeMoney(int takenMoney, out int newValue)
    {
        if (takenMoney > _money)
        {
            newValue = 0;
            return false;
        }
        else
        {
            //Debug.Log(takenMoney);
            newValue = Money = _money - takenMoney;
            return true;
        }
    }

    public void AddMoney(int addValue)
    {
        //Debug.Log(addValue);
        if (addValue >= 0) Money = Money + addValue;
        else Debug.LogError("Try to add negative money value");
    }
}
