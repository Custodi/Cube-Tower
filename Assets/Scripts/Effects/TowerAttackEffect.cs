using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackEffect : TowerEffect, IEffectableTower
{
    [SerializeField]
    private float _attackBonus;

    private float _initialAttack;

    public override void Start()
    {
        effectType = IEffectableTower.TowerEffect.Speed;
    }
    public override void Init(ShootingTower tower)
    {
        base.Init(tower);
    }

    public override void ApplyEffect()
    {
        tower.AddEffect(this);
        _initialAttack = tower.damageMultiplier;
        tower.damageMultiplier = tower.damageMultiplier * (1 + _attackBonus);
    }

    public override void CancelEffect()
    {
        tower.RemoveEffect(this);
        tower.fireRate = _initialAttack;
    }
}

