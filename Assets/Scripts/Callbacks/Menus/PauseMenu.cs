using UnityEngine;
using MobfishCardboard;

public class PauseMenu : Menu
{
    [SerializeField]
    private SceneData _sceneToLoad;

    public void OnResumeClick()
    {
        PauseController.Instance.TogglePause();
        MenuController.Instance.DisableMenu(Type);
        FindObjectOfType<CardboardUIOverlay>().Resume();
        //VrModeController.Instance.EnterVR();
    }

    public void OnReturnToMenuClick()
    {
        PauseController.Instance.TogglePause();
        MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Main, true);
        SceneController.Instance.Load(_sceneToLoad, null, MenuType.None);
    }

    public void OnBackClick()
    {
        MenuController.Instance.EnableMenu(MenuType.Exit);
    }
}
