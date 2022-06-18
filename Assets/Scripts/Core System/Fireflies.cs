using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Fireflies : MonoBehaviour, IInteractable, IToggleable
{
    [SerializeField]
    private TowerEffect _effect;

    [SerializeField]
    private float _additionalHeight;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private float _magnetizeRadius = 4f;

    private Vector3 _initialPosition;

    private bool _isMagnetized;
    private bool _isSelected;
    private bool _isUsing = false;
    private BuildCursorHandler _cursorScript;
    private Vector3 _fixedVector;
    private ShootingTower _shootingTowerScript;

    private void Start()
    {
        _initialPosition = transform.position;
        _cursorScript = Camera.main.GetComponent<BuildCursorHandler>();
        _fixedVector = new Vector3(0, _additionalHeight, 0);
    }

    private void Update()
    {
        if(_isUsing == false) // Если не используется
        {
            if (_isMagnetized)
            {
                Debug.Log(Vector3.Distance(transform.position, _cursorScript.GetRaycastPosition()));
                if (Vector3.Distance(transform.position, _cursorScript.GetRaycastPosition()) > _magnetizeRadius) _isMagnetized = false; // Разрыв при достижении лимита
                else if (Input.GetMouseButtonUp(0)) // Активация при клике на закреплённой башне
                {
                    ActivateBoost();
                    _isUsing = true;
                    return;
                }
            }
            else if (_isSelected) // Если выбрано
            {
                if (Input.GetMouseButtonUp(0)) // и было опущено
                {
                    transform.DOMove(_initialPosition, 2f); // Возвращаемся в исходную точку
                    _isSelected = false;
                    ToggleObject(_isSelected);
                }
                else
                {
                    transform.position = _cursorScript.GetRaycastPosition() + _fixedVector; // инчае продолжаем движение за курсором
                }

            }
        }
       

        /*if(_isMagnetized && _isUsing == false)
        {
            if(Input.GetMouseButtonUp(0))
            {
                if(_hasTowerSelected)
                {
                    ActivateBoost();
                    return;
                }
                _isMagnetized = false;
                ToggleObject(_isMagnetized);
                transform.DOMove(_initialPosition, 2f);
            }
            else if(Input.GetMouseButton(1))
            {
                _hasTowerSelected = _isMagnetized = false;
                ToggleObject(_isMagnetized);
                transform.DOMove(_initialPosition, 2f);
            }
            else if(_hasTowerSelected == false)
            {
                transform.position = _cursorScript.GetRaycastPosition() + _fixedVector;
            }
        }*/
    }

    public void OnClicked()
    {
        _isSelected = true;
        ToggleObject(_isSelected);
    }

    public void OnSelected()
    {
        //Debug.Log(2);
    }

    public void OnUnclicked()
    {
        //Debug.Log(3);
    }

    public void OnUnselected()
    {
        //Debug.Log(4);
    }

    public void ToggleObject(bool newState)
    {
        if(newState)
        {
            gameObject.layer = 2;
        }
        else
        {
            gameObject.layer = 8;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out _shootingTowerScript) && _isSelected)
        {
            _isMagnetized = true;
            transform.position = _shootingTowerScript.gameObject.transform.position + _fixedVector;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out _shootingTowerScript))
        {
            //_hasTowerSelected = false;
        }

    }

    private void ActivateBoost()
    {
        Debug.Log(1);
        //StartCoroutine(BoostTower(_duration));
        _isUsing = true;
        DOTween.To(() => particleCount, x => particleCount = x, 0, _duration).OnComplete(() => Destroy(gameObject));
        _shootingTowerScript.AddEffect(_effect);
    }

    private int particleCount
    {
        get
        {
            return _particleSystem.main.maxParticles;
        }
        set
        {
            var mainModule = _particleSystem.main;
            mainModule.maxParticles = value;
        }
    }

    /*IEnumerator BoostTower(float boostDuration)
    {
        // Tween a Vector3 called myVector to 3,4,8 in 1 second


        while (true)
        {
            _particleSystem.main.maxParticles
        }
    }*/
}
