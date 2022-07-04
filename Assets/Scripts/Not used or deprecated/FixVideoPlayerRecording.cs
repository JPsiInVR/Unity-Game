using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class FixVideoPlayerRecording : MonoBehaviour
{
    [SerializeField] 
    private List<VideoPlayer> _videoPlayers;
    private List<bool> _videosPlay = new List<bool>();
    private void Start()
    {
        for (int i = _videoPlayers.Count - 1; i >= 0; i--)
        {
            _videosPlay.Add(true);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _videoPlayers.Count; i++)
        {
            if (_videoPlayers[i].isActiveAndEnabled) {

                if (_videosPlay[i])
                {
                    _videoPlayers[i].Pause();
                    _videoPlayers[i].StepForward();
                    _videosPlay[i] = false;
                }
                else
                {
                    _videosPlay[i] = true;
                }
            }
        }
    }
}
