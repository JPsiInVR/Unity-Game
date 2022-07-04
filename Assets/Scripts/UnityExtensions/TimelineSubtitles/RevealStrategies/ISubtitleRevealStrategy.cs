using TMPro;

public interface ISubtitleRevealStrategy
{
    void Reveal(TMP_TextInfo textInfo, float completionPercentage, float fullRevealPercentage);
}