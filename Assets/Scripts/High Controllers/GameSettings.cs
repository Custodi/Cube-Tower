using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    private Vector2Int[] _availableResolutions;
    public Vector2Int[] availableResolusions { get => _availableResolutions; }

    private bool _isFullscreen;
    public bool isFullscreen
    {
        get => _isFullscreen;
        set
        {
            isFullscreen = value;
        }
    }

    private Vector2Int _currentResolution;
    public Vector2Int currentResolution
    {
        get => _currentResolution;
        set
        {
            _currentResolution = value;
        }
    }


    private float _musicVolume;
    public float musicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value / 100f;
        }
    }

    private float _soundVolume;
    public float soundVolume
    {
        get => _soundVolume;
        set
        {
            _soundVolume = value / 100f;
        }
    }

    public static GameSettings instance { get; private set; }

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
    }

    private void Start()
    {
        musicVolume = 0.5f;
        soundVolume = 0.5f;
        _isFullscreen = false;
        _currentResolution = new Vector2Int(1920, 1080);
        SetResolution(_currentResolution, _isFullscreen);
        /*SettingsData settings = new SettingsData();
        if (SaveLoadManager.instance.LoadSettingData(out settings) == false)
        {
            musicVolume = 0.5f;
            soundVolume = 0.5f;
            _isFullscreen = false;
            _currentResolution = new Vector2Int(1920, 1080);
            SetResolution(_currentResolution, _isFullscreen);
        }
        else
        {
            musicVolume = settings.musicVolume / 100f;
            soundVolume = settings.soundVolume / 100f;
            _isFullscreen = (settings.isFullscreen == 1 ? true : false);
            _currentResolution = new Vector2Int(settings.resolutionWidth, settings.resolutionHeight);
            SetResolution(_currentResolution, _isFullscreen);
        }*/
    }

    public void SetResolution(Vector2Int resolution, bool isFullscreen)
    {
        _currentResolution = resolution;
        _isFullscreen = isFullscreen;
        Screen.SetResolution(resolution.x, resolution.y, isFullscreen);
    }
}
