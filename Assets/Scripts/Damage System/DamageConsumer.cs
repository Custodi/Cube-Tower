using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageConsumer : MonoBehaviour
{
    private int _currentHealth = 1;
    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Try to set _currentHealth value lower 0");
                _currentHealth = 1;
            }
            else
            {
                _currentHealth = value;
            }
        }
    }

    private int _maxHealth;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Try to set _maxHealth value lower 0");
                _maxHealth = 1;
            }
            else
            {
                _maxHealth = value;
            }
        }
    }

    public delegate void DamageDeathEvent();
    public delegate void DamageHitEvent(int damage);

    public DamageDeathEvent OnDied;
    public DamageHitEvent OnTakeDamage;

    public void Instantiate(int health)
    {
        CurrentHealth = MaxHealth = health;
    }

    public void TakeUntargetDamage(int damage)
    {
        if (CurrentHealth <= damage)
        {
            CurrentHealth = 0;
            OnDied?.Invoke();
        }
        else
        {
            CurrentHealth -= damage;
            OnTakeDamage?.Invoke(damage);
        }
    }

    public void TakeDamage(DamageDealer damageDealer)
    {
        //Debug.Log(damageDealer.Damage);
        //Debug.Log(damageDealer.target + " ++ " + gameObject);
        if(damageDealer.target == gameObject)
        {
            //Debug.Log(1);
            if (CurrentHealth <= damageDealer.Damage)
            {
                CurrentHealth = 0;
                OnDied?.Invoke();
            }
            else
            {
                CurrentHealth -= damageDealer.Damage;
                OnTakeDamage?.Invoke(damageDealer.Damage);
            }
        }
    }
}
