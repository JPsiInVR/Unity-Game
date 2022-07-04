using UnityEngine;
using UnityEngine.UI;

public class SpeakerBackground : MonoBehaviour
{
    [field:SerializeField]
    public Image Image { get; private set; }

    [field:SerializeField]
    public ContentSizeFitter Fitter { get; private set; }

    public void Refresh()
    {
        Fitter.SetLayoutHorizontal();
    }
}