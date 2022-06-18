using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalEffect : EnemyEffect
{
    [Range(0f, 1f)]
    [SerializeField]
    private float _critChance;

    [SerializeField]
    private float _damageMultiplier;

    private int _damage;

    private new void Start()
    {
        GetComponent<Projectile>().effectList.Add(this);
    }

    public override void Init(Enemy enemy)
    {
        base.enemy = enemy;
        _damage = GetComponent<Projectile>().Damage;
    }

    public override void ApplyEffect()
    {
        var t = Random.Range(0f, 1f);
        //Debug.Log("Chance " + t + " of " + _critChance);
        //Debug.Log("Base dmg: " + _damage);
        if (_critChance != 0 && t <= _critChance)
        {
            enemy.HitEnemy((int)(_damage * _damageMultiplier - _damage));
            //Debug.Log("Critical attack! Damage: " + _damage + " Critical damage: " + ((int)(_damage * _damageMultiplier))); // ”честь множитеть урона за урон нанос€щего урона
        }
    }
}
