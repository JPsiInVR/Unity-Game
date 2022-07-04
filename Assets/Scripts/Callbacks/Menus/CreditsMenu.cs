public class CreditsMenu : Menu
{
    public void OnBackClick()
    {
        MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Main, true);
    }
}
