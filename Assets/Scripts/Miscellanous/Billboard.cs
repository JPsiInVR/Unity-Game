using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class is responsible for rotating objects towards camera (billboarding) and fading objects
/// out when they leave the screen.
/// </summary>

public class Billboard : MonoBehaviour
{
    private List<MeshFilter> _meshFilters;
    private List<MeshRenderer> _meshRenderers;
    private List<TextMeshPro> _textMeshes;
    private int _fullVertexCount;
    private int _visibleVertexCount;
    private float _vertexPercentageVisible;

    private void Start()
    {
        _meshFilters = new List<MeshFilter>(GetComponentsInChildren<MeshFilter>());
        _meshRenderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
        _textMeshes = new List<TextMeshPro>(GetComponentsInChildren<TextMeshPro>());

        foreach (TextMeshPro textMesh in _textMeshes)
        {
            //textMesh.ForceMeshUpdate();
        }

        foreach (MeshFilter meshFilter in _meshFilters)
        {
            _fullVertexCount += meshFilter.mesh.vertexCount;
        }
    }

    void Update()
    {
        if (Camera.main != null)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y,
                transform.eulerAngles.z);

            _visibleVertexCount = 0;

            foreach (MeshFilter meshFilter in _meshFilters)
            {
                _visibleVertexCount += meshFilter.GetVisibleVerticeCount(Camera.main);
            }
        }

        _vertexPercentageVisible = (float)_visibleVertexCount / _fullVertexCount;


        SetAlphaForAllRenderers();
    }

    private void SetAlphaForAllRenderers()
    {
        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            meshRenderer.material.SetFloat("Alpha", 1 - (1 - _vertexPercentageVisible));
        }

        foreach (TextMeshPro textMesh in _textMeshes)
        { 
            textMesh.faceColor = new Color(textMesh.faceColor.r, textMesh.faceColor.g, textMesh.faceColor.b, 1 - (1 - _vertexPercentageVisible));
        }
    }
}
