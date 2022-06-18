using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerView : View
{
    [SerializeField]
    private GameObject _waveOfferBar;

    private EnemySpawner _enemySpawnerScript;
    private WaveOfferBar _waveOfferBarScript;
    private EnemySpawner.SpawnerState _enemySpawnerState;
    public int currentWave 
    { 
        get
        {
            if (_enemySpawnerScript.currentWave == 0 && EnemySpawner.SpawnerState.Initial == _enemySpawnerState) return -1;
            else return _enemySpawnerScript.currentWave;
        }
    }
    public int waveCount => _enemySpawnerScript.waveCount;

    private void Awake()
    {
        _enemySpawnerScript = GetComponent<EnemySpawner>();
        if (_waveOfferBar.TryGetComponent(out _waveOfferBarScript) == false) Debug.LogError("Failed to get WaveOfferBar script");
    }

    private void Start()
    {
        var v = Camera.main.WorldToScreenPoint(transform.position);
        _waveOfferBar.GetComponent<RectTransform>().SetPositionAndRotation(v, Quaternion.identity);

        _enemySpawnerScript.OnChangeSpawnerState += OnStateUpdate;
        LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.WaveUpdate, this);
    }

    private void OnStateUpdate(EnemySpawner.SpawnerState newState)
    {
        _enemySpawnerState = newState;
        switch(newState)
        {
            case EnemySpawner.SpawnerState.Initial:
            {
                if (_enemySpawnerScript.wave.subWaves.Length == 0)
                {
                    _waveOfferBar.SetActive(false);
                }
                else
                {
                    _waveOfferBar.SetActive(true);
                    _waveOfferBarScript.SetFill(1f, 0f);
                }
                break;
            }
            case EnemySpawner.SpawnerState.Offer:
            {
                if (_enemySpawnerScript.nextSubWavesCount == 0)
                {
                    _waveOfferBar.SetActive(false);
                }
                else
                {
                    _waveOfferBar.SetActive(true);
                    _waveOfferBarScript.SetFill(0f, _enemySpawnerScript.metawave.offerDuration);
                }
                break;
            }
            case EnemySpawner.SpawnerState.Produce:
            {
                LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.WaveUpdate, this);
                _waveOfferBar.SetActive(false);
                break;
            }
        }
    }
    public void OnClicked()
    {
        _enemySpawnerScript.OnOfferClicked();
    }

    private void OnDestroy()
    {
        _enemySpawnerScript.OnChangeSpawnerState -= OnStateUpdate;
    }
}
