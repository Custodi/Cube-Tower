using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIInGamePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _wave;

    [SerializeField]
    private TextMeshProUGUI _money;

    [SerializeField]
    private TextMeshProUGUI _levelHP;

    [SerializeField]
    private Slider _waveProgressSlider;

    [SerializeField]
    private Button _pauseButton;

    [SerializeField]
    private GameObject _pauseScreen;

    private void Start()
    {
        UpdateUIMoney(LevelController.instance.LevelEconomics.Money);
        UpdateUIHealth(LevelController.instance.LevelHealth.Health);
    }

    private void Update()
    {
        // Unity bug (UI text doesn't update on screen)
        _levelHP.text = _levelHP.text;
        _money.text = _money.text;
    }

    public void UpdateUIHealth(int newValue)
    {
        if(newValue <= 5)
        {
            _levelHP.color = Color.red;
        }
        _levelHP.text = newValue + "";
    }

    public void UpdateUIMoney(int newValue)
    {
        _money.text = newValue.ToString();
    }

    public void UpdateWave(int currentWave, int countWaves)
    {
        _waveProgressSlider.DOValue((float)currentWave / countWaves, 1.5f);
        _wave.text = "Wave " + currentWave.ToString() + "/" + countWaves.ToString();
    }

    public void PauseGame()
    {
        LevelController.instance.PauseGame(true);
        _pauseScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
