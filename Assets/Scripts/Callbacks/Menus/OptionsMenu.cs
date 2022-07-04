using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Menu
{
    [SerializeField]
    private Slider _mainVolumeSlider;
    
    [SerializeField]
    private Slider _musicVolumeSlider;

    [SerializeField]
    private Slider _gazeTimeSlider;

    [SerializeField]
    private Toggle _subtitlesToggle;

    private void Awake()
    {
        _mainVolumeSlider.value = GameData.Instance.MainVolume;
        _musicVolumeSlider.value = GameData.Instance.MusicVolume;
        _gazeTimeSlider.value = GameData.Instance.GazeTime;
        _subtitlesToggle.isOn = GameData.Instance.SubtitlesEnabled;
        _subtitlesToggle.SetIsOnWithoutNotify(false);
    }

    public void OnBackClick()
    {
        PlayerPrefs.Save();
        MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Main, true);
    }

    public void ChangeSoundVolume(float volume)
    {
        GameData.Instance.MainVolume  = volume;
    }
    
    public void ChangeMusicVolume(float volume)
    {
        GameData.Instance.MusicVolume = volume;
    }

    public void ChangeGazeTime(float gazeTime)
    {
        GameData.Instance.GazeTime = gazeTime;
    }

    public void ToggleSubtitles(bool subtitlesEnabled)
    {
        GameData.Instance.SubtitlesEnabled = subtitlesEnabled;
    }
}
