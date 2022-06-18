using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotMechanic : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int _clickCount;

    [SerializeField]
    private int _minMoneyValue;

    [SerializeField]
    private int _maxMoneyValue;

    private Animation _animationComp;

    private void Start()
    {
        _animationComp = GetComponent<Animation>();
    }

    void IInteractable.OnClicked()
    {
        _clickCount--;
        LevelController.instance.LevelEconomics.AddMoney(Random.Range(_minMoneyValue, _maxMoneyValue + 1));
        if (_clickCount == 0) Destroy(gameObject);
        else
        {
            if(_animationComp.isPlaying)
            {
                _animationComp.Stop("Pot movement");
            }
            _animationComp.Play("Pot movement");
        }
    }

    void IInteractable.OnSelected()
    {
        
    }

    void IInteractable.OnUnclicked()
    {
        
    }

    void IInteractable.OnUnselected()
    {
        
    }
}
