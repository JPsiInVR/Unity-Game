using TMPro;
using UnityEngine;

public class LetterRevealStrategy : ISubtitleRevealStrategy
{
    public void Reveal(TMP_TextInfo textInfo, float completionPercentage, float fullRevealPercentage)
    {
        int characterCount = textInfo.characterCount;
        var percentagePerCharacter = fullRevealPercentage / characterCount;


        for (int i = 0; i < characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;


            Color32[] colors = textInfo.meshInfo[materialIndex].colors32;
            byte alpha = (byte)Mathf.Lerp(0, 255, (completionPercentage - (percentagePerCharacter * i)) / percentagePerCharacter);

            colors[vertexIndex + 0].a = alpha;
            colors[vertexIndex + 1].a = alpha;
            colors[vertexIndex + 2].a = alpha;
            colors[vertexIndex + 3].a = alpha;
        }
    }
}
