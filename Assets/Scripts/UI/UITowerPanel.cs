using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITowerPanel : MonoBehaviour
{
    [SerializeField] private Text _name, _power, _speed, _radius;

    public void Instantiate(string name, int power, float speed, float radius)
    {
        _name.text = name;
        _power.text = power.ToString();
        _speed.text = speed.ToString();
        _radius.text = radius.ToString(); 
    }
    public void Instantiate(TowerView view, Tower.TowerType a)
    {
       /* Debug.Log("Hello");
        _enemyView = view;
        _health.text = "HP: " + _enemyView.health.ToString();
        _name.text = _enemyView.name;
        _speed.text = "Speed: " + _enemyView.speed.ToString();
        _enemyView.OnUpdateUI += OnUpdateUI;
        gameObject.SetActive(true);*/
    }
}
