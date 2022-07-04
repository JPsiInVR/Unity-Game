using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubtitleList : MonoBehaviour
{
    [field:SerializeField]
    public List<TextMeshProUGUI> SubtitleTexts { get; set; }

    [field:SerializeField]
    public List<Image> SubtitleBackgrounds { get; set; }

    [field:SerializeField]
    public List<TextMeshProUGUI> SpeakerTexts { get; set; }

    [field:SerializeField]
    public List<SpeakerBackground> SpeakerBackgrounds { get; set; }
}
