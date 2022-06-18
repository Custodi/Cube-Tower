using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UISettings : MonoBehaviour
{
    [SerializeField]
    private Slider _soundSlider;

    [SerializeField]
    private TextMeshProUGUI _soundValueText;

    [SerializeField]
    private Slider _musicSlider;

    [SerializeField]
    private TextMeshProUGUI _musicValueText;

    [SerializeField]
    private Toggle _fullScreenToggle;

    [SerializeField]
    private TextMeshProUGUI _toggleTextState;

    private bool _isFullscreen;
    private int _musicVolume;
    private int _soundVolume;
    private Vector2Int _currentResolution;

    private void Awake()
    {
        _fullScreenToggle.onValueChanged.AddListener(
            delegate { 
                ToggleFullscreen(_fullScreenToggle); 
            });
        _soundSlider.onValueChanged.AddListener(
            delegate {
                SoundValueChanged();
            });
        _musicSlider.onValueChanged.AddListener(
            delegate {
                MusicValueChanged();
            });
    }

    private void Start()
    {
        _soundSlider.SetValueWithoutNotify(GameSettings.instance.soundVolume);
        _musicSlider.SetValueWithoutNotify(GameSettings.instance.musicVolume);
        _musicValueText.text = ((int)_musicSlider.value).ToString();
        _soundValueText.text = ((int)_soundSlider.value).ToString();
        _currentResolution = GameSettings.instance.currentResolution;
        _fullScreenToggle.SetIsOnWithoutNotify(GameSettings.instance.isFullscreen);
    }
    public void ToggleFullscreen(Toggle toggle)
    {
        if (toggle.isOn)
        {
            _toggleTextState.text = "On";
        }
        else
        {
            _toggleTextState.text = "Off";
        }
        GameSettings.instance.SetResolution(_currentResolution, toggle.isOn);
    }

    public void MusicValueChanged()
    {
        Debug.Log(((int)_musicSlider.value).ToString());
        _musicValueText.text = ((int)_musicSlider.value).ToString();
        GameSettings.instance.musicVolume = _musicSlider.value;
    }

    public void SoundValueChanged()
    {
        _soundValueText.text = ((int)_soundSlider.value).ToString();
        GameSettings.instance.soundVolume = _soundSlider.value;
    }

    public void SetResolution(int resolutionIndex)
    {
        _currentResolution = GameSettings.instance.availableResolusions[resolutionIndex];
        GameSettings.instance.SetResolution(_currentResolution, _isFullscreen);
    }
}
