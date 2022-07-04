using UnityEngine;

/// <summary>
/// This class is responisble for enabling/disabling doodle effect on text.
/// It also sets parameters of doodle effect.
/// </summary>
public class TextAnimator : MonoBehaviour
{
    [SerializeField]
    private Material _textMaterial;
    [SerializeField]
    private Vector4 _maxOffset;
    [SerializeField]
    private float _frameTime;
    [SerializeField]
    private int _frameCount;
    [SerializeField]
    private Vector4 _noiseScale;
    [SerializeField]
    private bool _doodleEnabled;

    private void Update()
    {
        _textMaterial.SetVector("_DoodleMaxOffset", _maxOffset);
        _textMaterial.SetFloat("_DoodleFrameTime", _frameTime);
        _textMaterial.SetInt("_DoodleFrameCount", _frameCount);
        _textMaterial.SetVector("_DoodleNoiseScale", _noiseScale);

        if(_doodleEnabled)
        {
            _textMaterial.EnableKeyword("DOODLE_ON");
        }
        else
        {
            _textMaterial.DisableKeyword("DOODLE_ON");
        }
    }
}
