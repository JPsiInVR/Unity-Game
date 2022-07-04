using UnityEngine;
using UnityEngine.Playables;

//TODO: rename to Section/Scene/S Manager?
public class NextSceneLoader : MonoBehaviour
{
    [SerializeField]
    private SceneData _nextScene;
    [SerializeField]
    private SectionData currentSectionData;
    [SerializeField]
    private string[] introTexts;

    private PlayableDirector _playableDirector;
    private IntroManager _introManager;

    private void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        _introManager = FindObjectOfType<IntroManager>();
    }

    public void ShowIntro()
    {
        StartCoroutine(_introManager.ShowIntroWithText(introTexts, _playableDirector));
    }

    public void LoadNextSection()
    {
        if (currentSectionData.SectionNumber == GameData.Instance.Progress)
        {
            GameData.Instance.Progress += 1;
        }

        SceneController.Instance.Load(_nextScene);
    }
}
