using UnityEngine;

public class CalibrationManager : MonoBehaviour
{

    private int _lookAtObjectsCount;
    
    [SerializeField]
    private SceneData _sceneToLoad;

    private void Start()
    {
        _lookAtObjectsCount = FindObjectsOfType<CardboardInteractable>().Length;
    }

    public void ProgressCalibration()
    {
        _lookAtObjectsCount--;
                
        if (_lookAtObjectsCount <= 0)
        {
            SceneController.Instance.Load(_sceneToLoad, null);
        }
    }
}
