using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffableTower: Tower
{
    protected Dictionary<IEffectableTower.TowerEffect, IEffectableTower> effectList;

    private ShootingTower _tower;

    private void Awake()
    {
        effectList = new Dictionary<IEffectableTower.TowerEffect, IEffectableTower>();
        _tower = GetComponent<ShootingTower>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void AddEffect(IEffectableTower effect)
    {
        if(effectList.ContainsKey(effect.GetEffect()) == false)
        {
            effectList.Add(effect.GetEffect(), effect);
            effect.Init(_tower);
            effect.ApplyEffect();
        }
    }

    private void Update()
    {
        
    }

    public void OnEffectEnd()
    {
        _tower.gameObject.transform.localScale = new Vector3(1.5f, 1.5f);
    }

    public void RemoveEffect(IEffectableTower effect)
    {
        //effect.CancelEffect();
        effectList.Remove(effect.GetEffect());
    }
}
