using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerPause : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        PauseController.Instance.OnPauseToggled += ToggleVideoPlayer;
    }

    private void OnDisable()
    {
        PauseController.Instance.OnPauseToggled -= ToggleVideoPlayer;
    }

    private void ToggleVideoPlayer()
    {
        if (_videoPlayer != null)
        {
            if (_videoPlayer.isPlaying)
            {
                _videoPlayer.Pause();
            }
            else
            {
                _videoPlayer.Play();
            }
        }
    }
}
