using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleBehaviour : PlayableBehaviour
{
    public string subtitle;
    public string speaker;
    public Color speakerColor;
    public SubtitleRevealType revealType;
    public float fullRevealPercentage;
    public AnimationCurve speakerFadeCurve;

    private SubtitleList _subtitleList;
    private Dictionary<SubtitleRevealType, ISubtitleRevealStrategy> _strategyDictionary = new Dictionary<SubtitleRevealType, ISubtitleRevealStrategy>();

    public override void OnPlayableCreate(Playable playable)
    {
        base.OnPlayableCreate(playable);

        _strategyDictionary.Add(SubtitleRevealType.Letter, new LetterRevealStrategy());
        _strategyDictionary.Add(SubtitleRevealType.Word, new WordRevealStrategy());
        _strategyDictionary.Add(SubtitleRevealType.All, new AllRevealStrategy());
    }

    public override void OnGraphStart(Playable playable)
    {
        base.OnGraphStart(playable);
        _subtitleList = GameObject.FindObjectOfType<SubtitleList>();
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
       
        if(GameData.Instance.SubtitlesEnabled && _subtitleList != null)
        {
            foreach (var subtitleText in _subtitleList.SubtitleTexts)
            {
                subtitleText.text = subtitle;
            }
            foreach (var speakerText in _subtitleList.SpeakerTexts)
            {
                speakerText.text = speaker;
            }
            foreach (var speakerBackground in _subtitleList.SpeakerBackgrounds)
            {
                speakerBackground.Refresh();
            }
        }
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        base.ProcessFrame(playable, info, playerData);

        if(GameData.Instance.SubtitlesEnabled && _subtitleList != null)
        {
            var percentage = (float)(playable.GetTime() / playable.GetDuration());

            foreach (var subtitleText in _subtitleList.SubtitleTexts)
            {
                _strategyDictionary[revealType].Reveal(subtitleText.textInfo, percentage, fullRevealPercentage);
                subtitleText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            }

            foreach(var speakerText in _subtitleList.SpeakerTexts)
            {
                speakerText.color = new Color(speakerText.color.r, speakerText.color.g, speakerText.color.b, speakerFadeCurve.Evaluate(percentage));
            }

            foreach(var speakerBackground in _subtitleList.SpeakerBackgrounds)
            {
                speakerBackground.Image.color = new Color(speakerColor.r, speakerColor.g, speakerColor.b, speakerFadeCurve.Evaluate(percentage) * speakerColor.a);
            }
        }
    }
}
