using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This class exists because of the bug in cardboard frustum culling. It increases the bounds of every
/// mesh in the scene so that it doesn't get culled too early.
/// In case it gets fixed this should be removed due to performance reasons.
/// If the performance hit is too big setting custom bounds per object is advised.
/// </summary>
public class IncreaseMeshBounds : MonoBehaviour
{
    [SerializeField]
    private float _boundsScale;

    private HashSet<MeshFilter> _meshFilters;
    private HashSet<TextMeshPro> _textMeshes;

    private void Start()
    {
        _meshFilters = new HashSet<MeshFilter>(FindObjectsOfType<MeshFilter>(true));
        _textMeshes = new HashSet<TextMeshPro>(FindObjectsOfType<TextMeshPro>(true));

        foreach (MeshFilter meshFilter in _meshFilters)
        {
            if (meshFilter.gameObject.name == "Outline" || meshFilter.gameObject.name == "Reticle")
            {
                continue;
            }

            meshFilter.mesh.bounds = new Bounds(meshFilter.transform.position, meshFilter.mesh.bounds.extents * _boundsScale);
        }

        foreach (TextMeshPro textMesh in _textMeshes)
        {
            textMesh.mesh.bounds = new Bounds(textMesh.transform.position, textMesh.mesh.bounds.extents * _boundsScale);
        }
    }
}
