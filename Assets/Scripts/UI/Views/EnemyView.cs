using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : View, IInteractable
{
    public int health { get; private set; }
    public int speed { get; private set; }
    public new string name { get; private set; }

    private Enemy _enemy;
    private DamageConsumer _damageConsumer;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _damageConsumer = GetComponent<DamageConsumer>();

        _enemy.OnEnemyDied += OnDead;
        _damageConsumer.OnTakeDamage += OnTakeDamage;

        name = _enemy.name;
        health = _damageConsumer.CurrentHealth;
        speed = (int)_enemy.speed;
    }

    public void OnClicked()
    {
        Debug.Log("Selected");
        //LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.EnemySelected, this);
    }

    public void OnSelected()
    {
        
    }

    public void OnUnselected()
    {

    }

    public void OnUnclicked()
    {

    }

    public void OnTakeDamage(int takenDamage)
    {
        health = _damageConsumer.CurrentHealth;
        OnUpdateUI?.Invoke(ViewType.Enemy, this);
    }

    public void OnDead(GameObject enemy)
    {
        LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.DestroyInfoPanel, this);
    }
}
