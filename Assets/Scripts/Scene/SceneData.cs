using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Scene Data", menuName = "Scriptable Objects/Scene Data")]
public class SceneData : ScriptableObject, ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset scene;
#endif

	public string SceneName => sceneName;

	public int SceneIndex => sceneIndex;

	public string ScenePath => scenePath;

	public float LoadDelay => loadDelay;

	public bool IsAdditive => isAdditive;

	[SerializeField] private string sceneName = null;

	[SerializeField] private int sceneIndex = -2;

	[SerializeField] private string scenePath = null;

	[Tooltip("Indicates whether the scene is single or additive")]
	[SerializeField] private bool isAdditive = true;
	
	[Tooltip("Loading delay in seconds")]
	[SerializeField] private float loadDelay = 0;

	public bool IsLoaded => SceneManager.GetSceneByName(SceneName).isLoaded;

	public void OnAfterDeserialize()
	{
    }

	public void OnBeforeSerialize()
	{
#if UNITY_EDITOR

		string nameValue = (scene != null) ? scene.name : null;
		int indexValue = -2;
		string pathValue = (scene != null) ? UnityEditor.AssetDatabase.GetAssetPath(scene) : null;

		UnityEditor.EditorBuildSettingsScene[] buildSettingsScenes = UnityEditor.EditorBuildSettings.scenes;
		if (buildSettingsScenes.Length > 0)
		{
			for (int i = 0; i < buildSettingsScenes.Length; i++)
			{
				if (UnityEditor.EditorBuildSettings.scenes[i].path == pathValue)
				{
					indexValue = i;
					break;
				}
			}
		}

		sceneName = nameValue;
		sceneIndex = indexValue;
		scenePath = pathValue;
#endif
	}
	
	public AsyncOperation LoadAsync(LoadSceneMode mode)
	{
		if (sceneIndex >= 0)
		{
			return LoadAsyncFromBuildIndex(mode);
		}
		else
		{
			return LoadAsyncFromPath(mode);
		}
	}

	public AsyncOperation LoadAsync(LoadSceneParameters parameters)
	{
		if (sceneIndex >= 0)
		{
			return LoadAsyncFromBuildIndex(parameters);
		}
		else
		{
			return LoadAsyncFromPath(parameters);
		}
	}

	public AsyncOperation LoadAsyncFromName(LoadSceneMode mode)
	{
		return SceneManager.LoadSceneAsync(sceneName, mode);
	}

	public AsyncOperation LoadAsyncFromName(LoadSceneParameters parameters)
	{
		return SceneManager.LoadSceneAsync(sceneName, parameters);
	}

	public AsyncOperation LoadAsyncFromPath(LoadSceneMode mode)
	{
		return SceneManager.LoadSceneAsync(scenePath, mode);
	}

	public AsyncOperation LoadAsyncFromPath(LoadSceneParameters parameters)
	{
		return SceneManager.LoadSceneAsync(scenePath, parameters);
	}

	public AsyncOperation LoadAsyncFromBuildIndex(LoadSceneMode mode)
	{
		return SceneManager.LoadSceneAsync(sceneIndex, mode);
	}

	public AsyncOperation LoadAsyncFromBuildIndex(LoadSceneParameters parameters)
	{
		return SceneManager.LoadSceneAsync(sceneIndex, parameters);
	}
}