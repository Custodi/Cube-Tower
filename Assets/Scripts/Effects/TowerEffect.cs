using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TowerEffect : MonoBehaviour, IEffectableTower
{
    private IEffectableTower.TowerEffect _effectType;
    public IEffectableTower.TowerEffect effectType { get => _effectType; protected set => _effectType = value; }

    [Header("Duration settings")]
    [SerializeField]
    protected bool isInfinite;
    [Space(5)]
    [SerializeField]
    protected float inititalDuration;
    protected float currentDuration;

    protected ShootingTower tower;

    public virtual void Start()
    {
        isInfinite = false;
    }

    public virtual void Init(ShootingTower tower)
    {
        this.tower = tower;
        currentDuration = inititalDuration;
    }

    public IEffectableTower.TowerEffect GetEffect()
    {
        return effectType;
    }

    public virtual void ApplyEffect()
    {
        
    }

    public virtual void CancelEffect()
    {
        
    }

    public virtual float UpdateEffect(float time)
    {
        if(isInfinite == false)
        {
            currentDuration = currentDuration - time;
            //Debug.Log("Lefted time:" + currentDuration);
            return currentDuration;
        }
        return 1;
    }
}
