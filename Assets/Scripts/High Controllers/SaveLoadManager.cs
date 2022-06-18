using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance { get; private set; }

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

    public void SaveSettingsData(SettingsData data)
    {
        PlayerPrefs.SetFloat("musicVolume", data.musicVolume);
        PlayerPrefs.SetFloat("soundVolume", data.soundVolume);
        PlayerPrefs.SetInt("isFullscreen", data.isFullscreen);
        PlayerPrefs.SetInt("resolutionWight", data.resolutionWidth);
        PlayerPrefs.SetInt("resolutionHeight", data.resolutionHeight);
        PlayerPrefs.Save();
    }

    public bool LoadSettingData(out SettingsData settings)
    {
        settings = new SettingsData();
        settings.musicVolume = PlayerPrefs.GetFloat("musicVolume");
        settings.soundVolume = PlayerPrefs.GetFloat("soundVolume");
        settings.isFullscreen = PlayerPrefs.GetInt("isFullscreen");
        settings.resolutionWidth = PlayerPrefs.GetInt("resolutionWight");
        settings.resolutionHeight = PlayerPrefs.GetInt("resolutionHeight");
        return (settings != null);
    }
}
