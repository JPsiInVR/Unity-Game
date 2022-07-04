using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    private SubtitleList _subtitleList;

    public override void OnGraphStart(Playable playable)
    {
        _subtitleList = GameObject.FindObjectOfType<SubtitleList>();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        base.ProcessFrame(playable, info, playerData);

        if (GameData.Instance.SubtitlesEnabled)
        {
            float currentAlpha = 0f;

            if (playerData != null)
            {
                _subtitleList = playerData as SubtitleList;
            }

            if (_subtitleList == null || _subtitleList.SubtitleTexts.Count == 0) { return; }

            int inputCount = playable.GetInputCount();
            bool isClipPlaying = false;

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);

                if (inputWeight > 0f)
                {
                    isClipPlaying = true;
                    currentAlpha = inputWeight;
                }
            }

            foreach (var subtitleText in _subtitleList.SubtitleTexts)
            {
                subtitleText.gameObject.SetActive(isClipPlaying);

                TMP_TextInfo textInfo = subtitleText.textInfo;

                for (int i = 0; i < textInfo.characterCount; i++)
                {
                    if (!textInfo.characterInfo[i].isVisible) continue;

                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                    Color32[] colors = textInfo.meshInfo[materialIndex].colors32;

                    colors[vertexIndex + 0].a = (byte)(colors[vertexIndex].a * currentAlpha);
                    colors[vertexIndex + 1].a = (byte)(colors[vertexIndex + 1].a * currentAlpha);
                    colors[vertexIndex + 2].a = (byte)(colors[vertexIndex + 2].a * currentAlpha);
                    colors[vertexIndex + 3].a = (byte)(colors[vertexIndex + 3].a * currentAlpha);
                }

                subtitleText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }

            foreach (var subtitleBackground in _subtitleList.SubtitleBackgrounds)
            {
                subtitleBackground.gameObject.SetActive(isClipPlaying);
            }

            foreach (var speakerText in _subtitleList.SpeakerTexts)
            {
                speakerText.gameObject.SetActive(isClipPlaying);
            }

            foreach (var speakerBackground in _subtitleList.SpeakerBackgrounds)
            {
                speakerBackground.gameObject.SetActive(isClipPlaying);
            }
        }
    }
}
