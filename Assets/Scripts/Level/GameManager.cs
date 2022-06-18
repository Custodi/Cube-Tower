using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _currentLevel;
    public int currentLevel { get => _currentLevel; private set => _currentLevel = value; }

    private int _levelDifficulty;
    public int levelDifficulty { get => _levelDifficulty; private set => _levelDifficulty = value; }

    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadLevel(int levelIndex, int difficulty)
    {
        currentLevel = levelIndex;
        levelDifficulty = difficulty;
        Debug.Log("GameManager loaded " + levelIndex + " level");
        SceneManager.LoadScene(levelIndex);
    }

    public void ReloadLevel()
    {
        LoadLevel(_currentLevel, _levelDifficulty);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
        SceneManager.LoadScene(0);
    }

    private void OnSceneLoaded(Scene oldScene, Scene newScene)
    {
        if(newScene.buildIndex == 0)
        {
            Debug.Log("was called");
            Time.timeScale = 1;
        }
        SceneManager.activeSceneChanged -= OnSceneLoaded;
    }
}
