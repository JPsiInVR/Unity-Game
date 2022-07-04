using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public delegate void SceneLoadDelegate(SceneData scene);

    public static SceneController Instance { get; private set; }

    [SerializeField]
    private SceneData _vrMainScene;
    
    [SerializeField]
    private SceneData _targetScene;

    private MenuController _menuController;
    private MenuType _loadingMenu;
    private SceneLoadDelegate _sceneLoadDelegate;
    private bool _isLoading;
    private bool _isSceneAdditive;

    private MenuController MenuController
    {
        get
        {
            if (_menuController == null)
            {
                _menuController = MenuController.Instance;
            }
            if (_menuController == null)
            {
                Debug.LogWarning("You are trying to access the MenuController, but no instance was found.");
            }
            return _menuController;
        }
    }

    private string CurrentSceneName => SceneManager.GetActiveScene().name;
  
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    public void Load(SceneData scene, SceneLoadDelegate sceneLoadDelegate = null, MenuType loadingMenu = MenuType.None,
        bool reload = false)
    {
        if (loadingMenu != MenuType.None && !MenuController || !SceneCanBeLoaded(scene, reload))
        {
            return;
        }

        _isSceneAdditive = _targetScene != null ? _targetScene.IsAdditive : false;
        _isLoading = true;
        _targetScene = scene;
        _loadingMenu = loadingMenu;
        _sceneLoadDelegate = sceneLoadDelegate;

        if (loadingMenu != MenuType.None)
        {
            MenuController.EnableMenu(loadingMenu);
        }

        StartCoroutine(LoadSceneWithDelay(scene.IsAdditive));
    }

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_targetScene == null || _targetScene.SceneName != scene.name)
        {
            return;
        }

        _sceneLoadDelegate?.Invoke(_targetScene);

        if (_loadingMenu != MenuType.None)
        {
            MenuController.DisableMenu(_loadingMenu);
        }

        _isLoading = false;

        //If switching from normal to additive
        if(_targetScene.IsAdditive && !_isSceneAdditive)
        {
            Debug.Log("Doing cleanup");
            
            //Destroy all audio listeners and event systems to avoid duplication
            AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();
            EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();

            for (int i = 0; i < audioListeners.Length; i++)
            {
                DestroyImmediate(audioListeners[i]);
            }

            for (int i = 0; i < eventSystems.Length; i++)
            {
                Destroy(eventSystems[i].gameObject);
            }
        }

        StartCoroutine(SwitchScene(scene));
    }
    
    private void OnSceneUnloaded(Scene scene)
    {
        //VrModeController.Instance.EnterVR();
    }

    private void OnSceneChanged(Scene scene, Scene oldScene)
    {
        UnloadScenes();
    }

    private IEnumerator LoadSceneWithDelay(bool loadAdditively)
    {
        List<AsyncOperation> loadingOperations = new List<AsyncOperation>
        {
            _targetScene.LoadAsync(loadAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single)
        };

        if (loadAdditively && !_vrMainScene.IsLoaded)
        {
            loadingOperations.Add(_vrMainScene.LoadAsync(LoadSceneMode.Additive));
        }

        foreach (var loadingOperation in loadingOperations)
        {
            loadingOperation.allowSceneActivation = false;
        }

        yield return new WaitForSeconds(_targetScene.LoadDelay);

        foreach (var loadingOperation in loadingOperations)
        {
            loadingOperation.allowSceneActivation = true;
        }
    } 

    private bool SceneCanBeLoaded(SceneData scene, bool reload)
    {
        return (CurrentSceneName != scene.SceneName || reload) && !_isLoading;
    }

    private void UnloadScenes()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (_targetScene != null && SceneManager.GetSceneAt(i).buildIndex != _targetScene.SceneIndex && SceneManager.GetSceneAt(i).buildIndex != _vrMainScene.SceneIndex)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            }
        }
    }

    private IEnumerator SwitchScene(Scene scene)
    {
        if (_loadingMenu != MenuType.None)
        {
            yield return new WaitWhile(() => MenuController.Instance.IsMenuGameObjectEnabled(MenuType.Loading));
        }

        SceneManager.SetActiveScene(scene);
    }
}