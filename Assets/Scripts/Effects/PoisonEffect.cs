using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : EnemyEffect
{
    [SerializeField]
    private int _poisonDamage;

    [SerializeField]
    private int _hitCount;

    [SerializeField]
    private float _periodTime;

    private float _currentPeriodTime;
    private float _currentHitCount;
    private float _damageMultiplier;

    public override void Start()
    {
        var projectileScript = GetComponent<Projectile>();
        projectileScript.effectList.Add(this);
        _damageMultiplier = projectileScript.DamageMultiplier;
        effectType = IEnemyEffectable.EnemyEffect.Poison;
    }
    public override void Init(Enemy enemy)
    {
        base.Init(enemy);
        _currentPeriodTime = _periodTime;
        _currentHitCount = _hitCount;
        //_damageMultiplier = damageMultiplier;
        //Debug.Log(_freezePower);
    }

    public override void ApplyEffect()
    {
        //enemy.gameObject.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
    }

    public override void CancelEffect()
    {
        //enemy.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public override float UpdateEffect(float time)
    {
        _currentPeriodTime -= time;
        if(_currentPeriodTime <= 0)
        {
            enemy.HitEnemy((int)(_poisonDamage * _damageMultiplier));
            if(--_currentHitCount == 0)
            {
                return -1;
            }
            _currentPeriodTime = _periodTime;
        }
        return 1;
    }

    public override void ResetDuration()
    {
        _currentHitCount = _hitCount;
    }
}
