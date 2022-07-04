using UnityEngine;
using UnityEngine.Audio;

public class GameData : Singleton<GameData>
{
    [SerializeField]
    private AudioMixer _musicMixer;

    [SerializeField]
    private AudioMixer _mainMixer;

    private float _musicVolume;
    private float _mainVolume;
    private float _gazeTime;
    private int _progress;
    private bool _subtitlesEnabled;

    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            _musicMixer?.SetFloat(Constants.MusicVolumeKey, MusicVolume);
            PlayerPrefs.SetFloat(Constants.MusicVolumeKey, _musicVolume);
        }
    }
    public float MainVolume
    { 
        get => _mainVolume;
        set 
        {
            _mainVolume = value;
            _mainMixer?.SetFloat(Constants.MainVolumeKey, MainVolume);
            PlayerPrefs.SetFloat(Constants.MainVolumeKey, _mainVolume);
        }
    }
    public float GazeTime
    {
        get => _gazeTime;
        set
        {
            _gazeTime = value;
            PlayerPrefs.SetFloat(Constants.GazeTimeKey, _mainVolume);
        }
    }
    public int Progress
    {
        get => _progress;
        set
        {
            _progress = value;
            PlayerPrefs.SetInt(Constants.ProgressKey, _progress);
        }
    }
    public bool SubtitlesEnabled
    {
        get => _subtitlesEnabled;
        set
        {
            _subtitlesEnabled = value;
            PlayerPrefs.SetInt(Constants.SubtitlesEnabledKey, (_subtitlesEnabled ? 1 : 0));
        }
    }

    protected override void Awake()
    {
        base.Awake();

        MusicVolume = PlayerPrefs.GetFloat(Constants.MusicVolumeKey, 1);
        MainVolume = PlayerPrefs.GetFloat(Constants.MainVolumeKey, 1);
        GazeTime = PlayerPrefs.GetFloat(Constants.GazeTimeKey, 1);
        Progress = PlayerPrefs.GetInt(Constants.ProgressKey, 1);
        SubtitlesEnabled = PlayerPrefs.GetInt(Constants.SubtitlesEnabledKey, 0) == 1;
    }
}
