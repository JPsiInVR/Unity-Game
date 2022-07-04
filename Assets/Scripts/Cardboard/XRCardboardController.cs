#if !UNITY_EDITOR
//using Google.XR.Cardboard;
#endif
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XRCardboardController : MonoBehaviour
{
    public static XRCardboardController Instance { get; private set; }

    public float GlobalGazeTime => _globalGazeTime;

    [SerializeField]
    private bool _raycastForTarget = true;
    [SerializeField]
    private LayerMask _interactableLayers = -1;
    [SerializeField]
    private float _globalGazeTime;
    [SerializeField]
    private float _raycastDistance = 10;
    [SerializeField]
    private EventSystem _eventSystem;
    [SerializeField]
    private GameObject _rectile;

    private GraphicRaycaster _graphicRaycaster;
    private CardboardInteractable _gazedAtObject = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Only one instance of singleton allowed");
        }

        Instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        _gazedAtObject = null;
        _graphicRaycaster = FindObjectsOfType<GraphicRaycaster>()
            .FirstOrDefault(raycaster => raycaster.gameObject.name != "UI" && raycaster.gameObject.name != "Canvas");
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        if (_raycastForTarget)
        {
            CastForInteractables();
        }
    }

    public bool IsTriggerPressed()
    {
        return Input.GetMouseButtonDown(0);
    }

    private void CastForInteractables()
    {
        if (!CastForUIObjects())
        {
            if (!CastForColliderObjects())
            {
                _gazedAtObject?.PointerExit();
                _gazedAtObject = null;
            }
        }
    }

    private bool CastForUIObjects()
    {

        if (_graphicRaycaster != null)
        {
            PointerEventData pointerEventData = new PointerEventData(_eventSystem);
            List<RaycastResult> results = new List<RaycastResult>();

            //Better accuracy with rectile position than viewport(0.5, 0.5) due to projection matrix being applied
            pointerEventData.position = Camera.main.WorldToScreenPoint(_rectile.gameObject.transform.position);
            _graphicRaycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult raycastResult in results)
            {
                if (_interactableLayers == (_interactableLayers | (1 << raycastResult.gameObject.layer)))
                {
                    SwitchInteractableObject(raycastResult.gameObject.GetComponent<CardboardInteractable>());
                    return true;
                }
            }
        }
        return false;
    }

    private bool CastForColliderObjects()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _raycastDistance, _interactableLayers))
        {
            SwitchInteractableObject(hit.transform.gameObject.GetComponent<CardboardInteractable>());
            return true;
        }

        return false;
    }

    private void SwitchInteractableObject(CardboardInteractable interactableGameobject)
    {
        if (_gazedAtObject != interactableGameobject)
        {
            _gazedAtObject?.PointerExit();
            _gazedAtObject = interactableGameobject;
            _gazedAtObject.PointerEnter();
        }
    }
}
