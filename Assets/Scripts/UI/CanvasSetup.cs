using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for assigning camera to the canvas.
/// This can't be done manually, because camera is kept in the different scene.
/// </summary>
[RequireComponent(typeof(Canvas))]
public class CanvasSetup : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += Test;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= Test;
    }

    private void Test(Scene scene, Scene mode)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        yield return new WaitForSeconds(1);
        var go = FindObjectOfType<DynamicResolution>();
        if (go != null)
        {
            GetComponent<Canvas>().worldCamera = go.gameObject.FindObject("UICamera").GetComponent<Camera>();
            GetComponent<Canvas>().planeDistance = 1;
        }
    }
}

static class test
{
    public static GameObject FindObject(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
