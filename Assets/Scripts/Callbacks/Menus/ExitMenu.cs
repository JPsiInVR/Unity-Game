using UnityEngine;

public class ExitMenu : Menu
{
    public void OnBackClick()
    {
        MenuController.Instance.DisableMenu(MenuType.Exit);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
