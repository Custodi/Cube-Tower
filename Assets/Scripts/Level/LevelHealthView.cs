using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHealthView : View
{
    public int LevelHP { get; private set; }

    private DamageConsumer _damageConsumer;

    // Start is called before the first frame update
    void Start()
    {
        _damageConsumer = GetComponent<DamageConsumer>();
        _damageConsumer.OnTakeDamage += OnTakeDamage;
        _damageConsumer.OnDied += OnDied;
    }

    private void OnTakeDamage(int damage)
    {
        UpdateHealthUI();
    }

    private void OnDied()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        LevelHP = _damageConsumer.CurrentHealth;
        LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.LevelHealthUpdate, this);
    }

    private void OnDestroy()
    {
        _damageConsumer.OnTakeDamage -= OnTakeDamage;
        _damageConsumer.OnDied -= OnDied;
    }
}
