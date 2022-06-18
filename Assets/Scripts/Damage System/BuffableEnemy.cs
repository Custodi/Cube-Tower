using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffableEnemy : MonoBehaviour
{
    private Dictionary<IEnemyEffectable.EnemyEffect, EnemyEffect> effectList;

    private Enemy _enemy;

    private void Awake()
    {
        effectList = new Dictionary<IEnemyEffectable.EnemyEffect, EnemyEffect>();
        _enemy = GetComponent<Enemy>();
    }
    public void AddEffect(EnemyEffect effect)
    {
        //Debug.Log("Add effect called" + " | effect list " + effectList.Count);
        foreach (var item in effectList)
        {
            if(item.Key == effect.GetEffect())
            {
                //Debug.Log("add effect checked");
                item.Value.ResetDuration();
                return;
            }
        }
        effectList.Add(effect.GetEffect(), effect);
        effect.Init(_enemy);
        effect.ApplyEffect();
    }

    private void Update()
    {
        OnEffectUpdate();
    }

    public void OnEffectUpdate()
    {
        try
        {
            foreach (var item in effectList)
            {
                if (item.Value.UpdateEffect(Time.deltaTime) <= 0)
                {
                    RemoveEffect(item.Value);
                }
            }

        }
        catch
        {

        }
        
    }

    public void OnEffectEnd()
    {
        _enemy.gameObject.transform.localScale = new Vector3(1.5f, 1.5f);
    }

    public void RemoveEffect(EnemyEffect effect)
    {
        effect.CancelEffect();
        effectList.Remove(effect.GetEffect());
    }
}
