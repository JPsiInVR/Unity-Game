using UnityEngine;
using UnityEngine.UI;

public class UnscaledTimeWrapper : MonoBehaviour
{
    private Material _material;

    void Start()
    {
        _material = GetComponent<Image>().material;
    }

    void Update()
    {
        _material.SetFloat("_UnscaledTime", Time.unscaledTime);
    }
}
