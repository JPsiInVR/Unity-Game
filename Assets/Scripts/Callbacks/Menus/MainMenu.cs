using MobfishCardboard;
using UnityEngine;

public class MainMenu : Menu
{
    [SerializeField]
    private SceneData sceneToLoad;

    public void OnStartClick()
    {

        MenuController.Instance.DisableMenu(Type);
        SceneController.Instance.Load(sceneToLoad, (data) => { 
            CardboardManager.SetVRViewEnable(!CardboardManager.enableVRView);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }, MenuType.Loading);
    }

    public void OnOptionsClick()
    {
        //MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Options, true);
        MenuController.Instance.EnableMenu(MenuType.Options);
    }

    public void OnCreditsClick()
    {
        MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Credits, true);
    }

    public void OnBackClick()
    {
        MenuController.Instance.EnableMenu(MenuType.Exit);
    }
}
