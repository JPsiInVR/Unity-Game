using TMPro;
using UnityEngine;

public class WordRevealStrategy : ISubtitleRevealStrategy
{
    public void Reveal(TMP_TextInfo textInfo, float completionPercentage, float fullRevealPercentage)
    {
        int wordCount = textInfo.wordCount;
        var percentagePerCharacter = fullRevealPercentage / wordCount;

        for (int i = 0; i < wordCount; i++)
        {
            TMP_WordInfo wordInfo = textInfo.wordInfo[i];

            for (int j = 0; j < wordInfo.characterCount; j++)
            {
                int charIndex = wordInfo.firstCharacterIndex + j;
                int materialIndex = textInfo.characterInfo[charIndex].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;

                byte alpha = (byte)Mathf.Lerp(0, 255, (completionPercentage - (percentagePerCharacter * i)) / percentagePerCharacter);
                Color32[] colors = textInfo.meshInfo[materialIndex].colors32;

                colors[vertexIndex + 0].a = alpha;
                colors[vertexIndex + 1].a = alpha;
                colors[vertexIndex + 2].a = alpha;
                colors[vertexIndex + 3].a = alpha;
            }
        }
    }
}
