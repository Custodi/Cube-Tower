using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyPanel : MonoBehaviour
{
    [SerializeField] private Text _name, _health, _speed;

    private EnemyView _enemyView;

    public void Instantiate(EnemyView view)
    {
        Debug.Log("Hello");
        _enemyView = view;
        _health.text = "HP: " + _enemyView.health.ToString();
        _name.text = _enemyView.name;
        _speed.text = "Speed: " + _enemyView.speed.ToString();
        _enemyView.OnUpdateUI += OnUpdateUI;
        gameObject.SetActive(true);
    }

    public void OnUpdateUI(ViewType viewType, View view)
    {
        //Debug.Log("It was called");
        _health.text = "HP: " + _enemyView.health.ToString();
        _name.text = _enemyView.name;
        _speed.text = "Speed: " + _enemyView.speed.ToString();
    }

    public void Clear()
    {
        if(_enemyView != null)
        {
            _enemyView.OnUpdateUI -= OnUpdateUI;
            _enemyView = null;
            gameObject.SetActive(false);
        }
    }
}
