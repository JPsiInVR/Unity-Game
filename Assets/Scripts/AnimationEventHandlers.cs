using UnityEngine;

//This class contains all methods, which are called by animations.
public class AnimationEventHandlers : MonoBehaviour
{
    //This method is fired when credits finish rolling.
    public void HideCredits()
    {
        MenuController.Instance.DisableAndEnableMenu(MenuType.Credits, MenuType.Main, true);
    }
}
