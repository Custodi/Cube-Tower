using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyEffect : MonoBehaviour, IEnemyEffectable
{
    private IEnemyEffectable.EnemyEffect _effectType;
    public IEnemyEffectable.EnemyEffect effectType { get => _effectType; protected set => _effectType = value; }

    protected float inititalDuration;
    protected float currentDuration;

    protected Enemy enemy;

    public virtual void Start()
    {
       
    }

    public virtual void Init(Enemy enemy)
    {
        this.enemy = enemy;
        currentDuration = inititalDuration;
    }

    public IEnemyEffectable.EnemyEffect GetEffect()
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
        currentDuration = currentDuration - time;
        //Debug.Log("Lefted time:" + currentDuration);
        return currentDuration;
    }

    public virtual void ResetDuration()
    {
        
    }
}
