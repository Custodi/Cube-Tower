using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageConsumer))]
[RequireComponent(typeof(LevelHealthView))]
public class LevelHealth : MonoBehaviour
{
    [SerializeField]
    private int _initialLevelHealth;

    [SerializeField]
    private FinishPoint[] _finishPoints;
    public int Health { get => _damageConsumer.CurrentHealth; }

    private DamageConsumer _damageConsumer;

    private void Start()
    {
        _damageConsumer = GetComponent<DamageConsumer>();
        foreach (FinishPoint item in _finishPoints)
        {
            item.OnEnemyHit += OnEnemyHit;
        }

        _damageConsumer.OnTakeDamage += OnTakeDamage;
        _damageConsumer.OnDied += OnDied;

        _damageConsumer.CurrentHealth = _damageConsumer.MaxHealth = _initialLevelHealth;
    }

    private void OnEnemyHit(DamageDealer damageDealer)
    {
        _damageConsumer.TakeDamage(damageDealer);
        //Debug.Log(_damageConsumer.CurrentHealth);
    }

    private void OnTakeDamage(int damage)
    {
        //Debug.Log("Damage" + damage);
    }

    private void OnDied()
    {
        LevelController.instance.LoseGame();
    }

    private void OnDestroy()
    {
        _damageConsumer.OnTakeDamage -= OnTakeDamage;
        _damageConsumer.OnDied -= OnDied;

        foreach (FinishPoint item in _finishPoints)
        {
            item.OnEnemyHit -= OnEnemyHit;
        }
    }


}
