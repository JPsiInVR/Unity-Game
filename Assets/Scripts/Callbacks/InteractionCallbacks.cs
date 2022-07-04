using UnityEngine;

public class InteractionCallbacks : MonoBehaviour
{     
    public void LoadScene(SceneData sceneToLoad)
    {
        SceneController.Instance.Load(sceneToLoad);
    }
}