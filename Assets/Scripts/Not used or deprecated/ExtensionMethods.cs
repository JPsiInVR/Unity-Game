using UnityEngine;

public static class ExtensionMethods
{
    public static int GetVisibleVerticeCount(this MeshFilter meshFilter, Camera camera)
    {
        int visibleVertices = meshFilter.mesh.vertexCount;

        foreach (Vector3 vertice in meshFilter.mesh.vertices)
        {
            if (!new Rect(0, .1f, .8f, 1.1f).Contains(camera.WorldToViewportPoint(meshFilter.transform.TransformPoint(vertice))))
            {
                visibleVertices--;
            }
        }
        return visibleVertices;
    }

    /// <summary>
    /// Not used.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static int GetActiveChildrenCount(this Transform transform)
    {
        int activeCount = 0;
        
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                activeCount++;
            }
        }

        return activeCount;
    }
}
