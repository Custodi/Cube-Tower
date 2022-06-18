using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIHealthBar : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemyScript;

    [SerializeField]
    private Image _hpBar;

    private DamageConsumer _damageConsumer;

    private Camera _mainCamera;
   

    // Start is called before the first frame update
    void Start()
    {
        _damageConsumer = _enemyScript.DamageConsumer;
        _damageConsumer.OnTakeDamage += UpdateHealth;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.back, _mainCamera.transform.rotation * Vector3.down);
    }

    private void UpdateHealth(int takenDamage)
    {
        _hpBar.fillAmount = (float)_damageConsumer.CurrentHealth / (float)_damageConsumer.MaxHealth;
    }

    private void OnDestroy()
    {
        _damageConsumer.OnTakeDamage -= UpdateHealth;
    }
}
