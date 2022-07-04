using UnityEngine;
using UnityEngine.Playables;

public class SubtitleClip : PlayableAsset
{
    [TextArea(5, 10)]
    public string subtitleText;
    [Space]
    public string speaker;
    [Space]
    public Color speakerColor;
    [Space]
    public SubtitleRevealType revealType;
    [Space]
    public AnimationCurve speakerFadeCurve = AnimationCurve.Constant(0, 1, 1);
    [Space]
    [Range(0, 1)]
    public float fullRevealPercentage = 0.9f;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

        SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();
        subtitleBehaviour.speakerColor = speakerColor;
        subtitleBehaviour.subtitle = subtitleText;
        subtitleBehaviour.speaker = speaker;
        subtitleBehaviour.revealType = revealType;
        subtitleBehaviour.fullRevealPercentage = fullRevealPercentage;
        subtitleBehaviour.speakerFadeCurve = speakerFadeCurve;
        return playable;
    }
}
