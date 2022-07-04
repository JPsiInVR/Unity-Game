using TMPro;
using UnityEngine;

public class AllRevealStrategy : ISubtitleRevealStrategy
{
    public void Reveal(TMP_TextInfo textInfo, float completionPercentage, float fullRevealPercentage)
    {
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            Color32[] colors = textInfo.meshInfo[materialIndex].colors32;
            byte alpha = 255;

            colors[vertexIndex + 0].a = alpha;
            colors[vertexIndex + 1].a = alpha;
            colors[vertexIndex + 2].a = alpha;
            colors[vertexIndex + 3].a = alpha;
        }
    }
}
