using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public delegate void PauseGameEvent();
    public PauseGameEvent OnPauseGame;
    public delegate void UnpauseGameEvent();
    public PauseGameEvent OnUnpauseGame;

    [HideInInspector]
    public static LevelController instance;

    [SerializeField]
    private int _level;

    [Header("Controllers")]

    [SerializeField]
    private LevelUIManager _levelUIManager;

    [SerializeField]
    private LevelHealth _levelHealth;

    [SerializeField]
    private LevelEconomics _levelEconomics;

    [Header("UI Screens")]
    [SerializeField]
    private GameObject _winScreen;

    [SerializeField]
    private GameObject _loseScreen;

    public LevelUIManager LevelUIManager { get => _levelUIManager; private set => _levelUIManager = value; }
    public LevelHealth LevelHealth { get => _levelHealth; private set => _levelHealth = value; }
    public LevelEconomics LevelEconomics { get => _levelEconomics; private set => _levelEconomics = value; }
    public int currentLevel { get => _level; private set => _level = value; }

    private Coroutine CheckEnemyCoroutine = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
            return;
        }

        LevelUIManager = _levelUIManager;
        LevelHealth = _levelHealth;
        LevelEconomics = _levelEconomics;
    }

    public void PauseGame(bool pause)
    {
        if (pause == false)
        {
            Time.timeScale = 1;
            OnUnpauseGame?.Invoke();
        }
        else
        {
            Time.timeScale = 0;
            OnPauseGame?.Invoke();
        }
    }

    public void CheckEndGame(GameObject enemyRoot)
    {
        if(CheckEnemyCoroutine == null)
        {
            CheckEnemyCoroutine = StartCoroutine(CheckEnemyCount(enemyRoot));
        }
    }

    public void WinGame()
    {
        _winScreen.SetActive(true);
        PauseGame(true);
    }

    public void LoseGame()
    {
        _loseScreen.SetActive(true);
        PauseGame(true);
    }

    public void TryAgain()
    {
        GameManager.instance.ReloadLevel();
        PauseGame(false);
    }

    public void ReturnToMenu(bool hasWon)
    {
        if (hasWon)
        {
            PlayerPrefs.SetInt("Level" + currentLevel, 1);
            Debug.Log("Prefs was setted for level " + currentLevel + " to 1 || " + "Level" + currentLevel);
        }
        PauseGame(false);
        Debug.Log(GameManager.instance);
        GameManager.instance.ReturnToMainMenu();
    }

    IEnumerator CheckEnemyCount(GameObject enemyRoot)
    {
        while(true)
        {
            if (enemyRoot.transform.childCount == 0)
            {
                WinGame();
                yield return null;
            }
            else yield return new WaitForSeconds(1f);
        }
    }

}
