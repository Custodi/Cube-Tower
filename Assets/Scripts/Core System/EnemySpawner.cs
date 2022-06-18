using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawnerView))]
public class EnemySpawner : MonoBehaviour
{
    public enum SpawnerState
    {
        Initial,
        Offer,
        Produce
    }

    [SerializeField]
    private EnemyList _enemyList;
    [SerializeField]
    private WaveList _levelWavesList;
    [SerializeField]
    private Waypoints _waypointsPath;
    [SerializeField]
    private GameObject _enemyParent;
    [SerializeField]
    private MetaWaveList _metaWaveList;

    private WaveList.Wave[] _waves;
    private Transform[] _waypoints;
    private MetaWaveList.MetaWave[] _metaWaves;

    private WaitForSeconds DEBUGsubwaveSpawnTime;
    private int _currentWave = 0;

    public int currentWave { get => _currentWave; private set => _currentWave = value; }
    public int waveCount { get => _waves.Length; }
    public WaveList.Wave wave { get => _waves[currentWave]; }

    public int nextSubWavesCount
    {
        get
        {
            try
            {
                return _waves[currentWave + 1].subWaves.Length;
            }
            catch
            {
                return -1;
            }
        }
        
    }

    public MetaWaveList.MetaWave metawave { get => _metaWaves[currentWave]; }

    private IEnumerator _nextWaveReleaseCounter;
    private SpawnerState _spawnerState;

    private float _settedTime, _clickedTime;

    public delegate void NewSpawnerStateEvent(SpawnerState newState);
    public NewSpawnerStateEvent OnChangeSpawnerState;

    private void Awake()
    {
        DEBUGsubwaveSpawnTime = new WaitForSeconds(2f);
        _waypoints = _waypointsPath.Points;
        _waves = _levelWavesList._wavesList;
        _metaWaves = _metaWaveList._metaWaveCollection;

        _nextWaveReleaseCounter = NextWaveReleaseCounter(_currentWave);
    }

    void Start()
    {
        _spawnerState = SpawnerState.Initial;
        OnChangeSpawnerState?.Invoke(_spawnerState);
    }

    // Timer to show up release reward button
    IEnumerator NextWaveOfferCounter(int currentWave)
    {
        //Debug.Log("offer counter");
        if (_metaWaves.Length <= currentWave)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(_metaWaves[currentWave].nextOfferTime);
            _nextWaveReleaseCounter = NextWaveReleaseCounter(_currentWave);
            StartCoroutine(_nextWaveReleaseCounter);
        }
        
    }

    // Timer to release new wave
    IEnumerator NextWaveReleaseCounter(int wave)
    {
        _spawnerState = SpawnerState.Offer;
        OnChangeSpawnerState?.Invoke(_spawnerState);
        _settedTime = Time.realtimeSinceStartup;
        //Debug.Log("Time: " + _metaWaves[currentWave].offerDuration);
        yield return new WaitForSeconds(_metaWaves[wave].offerDuration);
        //Debug.Log(3);
        StartCoroutine(ProduceEnemies(++currentWave));
    }

    IEnumerator ProduceEnemies(int spawnedWave)
    {
        //Debug.Log("Enemy generate" + spawnedWave);
        _spawnerState = SpawnerState.Produce;
        OnChangeSpawnerState?.Invoke(_spawnerState);
        WaveList.SubWave[] waveElems = _waves[spawnedWave].subWaves;
        StartCoroutine(NextWaveOfferCounter(spawnedWave));
        for (int subwaves = 0; subwaves < waveElems.Length; subwaves++)
        {
            //Debug.Log("hehe");
            for (int k = 0; k < waveElems[subwaves].count; k++)
            {
                //Debug.Log("Wave" + i + " | " + _waves.Length + " Subwave: " + subwaves + " | " + waveElems.Length + " Enemy: " + " | " + k + " | " + waveElems[subwaves].count);
                InitEnemy(waveElems[subwaves].id);
                yield return DEBUGsubwaveSpawnTime; // Time between enemy spawn in wave
            }
        }
        if (_metaWaves.Length <= currentWave) LevelController.instance.CheckEndGame(_enemyParent);
    }
    public void OnOfferClicked()
    {
        //Debug.Log(_currentWave);
        if (_spawnerState == SpawnerState.Initial)
        {
            StartCoroutine(ProduceEnemies(currentWave));
        }
        else
        {
            _clickedTime = Time.realtimeSinceStartup;
            StopCoroutine(_nextWaveReleaseCounter);
            AddMoneyForWaveReleasing(_settedTime, _clickedTime, currentWave);
            StartCoroutine(ProduceEnemies(++currentWave));
        }
    }

    private void InitEnemy(int monsterId)
    {
        //Debug.Log(monsterId);
        EnemyConfiguration enemyConfiguration = _enemyList.EnemyListCollection[monsterId];
        Enemy enemyScript = Instantiate(enemyConfiguration.enemyPrefab, transform.position, Quaternion.identity, _enemyParent.transform).GetComponent<Enemy>();
        enemyScript.Instantiate(enemyConfiguration, _waypoints, transform.rotation);
    }

    private void AddMoneyForWaveReleasing(float settedTime, float clickedTime, int currentWave)
    {
        var offerDuration = _metaWaves[currentWave].offerDuration;
        var rewardRatio = (settedTime + offerDuration - clickedTime) / offerDuration;
        //Debug.Log(rewardRatio);
        LevelController.instance.LevelEconomics.AddMoney((int)(rewardRatio * _metaWaves[currentWave].offerReward));
    }
}
