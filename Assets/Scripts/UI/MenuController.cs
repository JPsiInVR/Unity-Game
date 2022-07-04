using System.Collections;
using System.Linq;
using UnityEngine;
public class MenuController : Singleton<MenuController>
{
    [SerializeField]
    private MenuType _defaultMenu;

    private Hashtable _menusByType;
    private Coroutine _menuExitCoroutine;

    protected override void Awake()
    {
        base.Awake();

        _menusByType = new Hashtable();

        foreach (Menu page in GetComponentsInChildren<Menu>(true).ToList())
        {
            page.gameObject.SetActive(false);

            if (MenuExists(page.Type))
            {
                Debug.LogWarning("Strona tego typu już istnieje");
                return;
            }

            _menusByType.Add(page.Type, page);
        }

        if (_defaultMenu != MenuType.None)
        {
            EnableMenu(_defaultMenu);
        }
    }

    public bool IsMenuEnabled(MenuType type)
    {
        return MenuExists(type) && GetMenu(type).IsEnabled;
    }

    public bool IsMenuGameObjectEnabled(MenuType type)
    {
        return MenuExists(type) && GetMenu(type).gameObject.activeInHierarchy;
    }

    public void EnableMenu(MenuType type)
    {
        if (type == MenuType.None)
        {
            return;
        }

        if (!MenuExists(type))
        {
            Debug.LogWarning("Strona, którą chcesz włączyć nie istnieje");
            return;
        }


        Menu page = GetMenu(type);
        page.gameObject.SetActive(true);
        page.Animate(true);

    }

    public void DisableMenu(MenuType type)
    {
        if (type == MenuType.None)
        {
            return;
        }

        if (!MenuExists(type))
        {
            Debug.LogWarning("Strona, którą chcesz wyłączyć nie istnieje");
            return;
        }

        Menu page = GetMenu(type);

        if (page.gameObject.activeSelf)
        {
            page.Animate(false);
        }
    }

    public void DisableAndEnableMenu(MenuType disableType, MenuType enableType, bool waitForAnimationEnd = false)
    {
        Menu disablePage = GetMenu(disableType);

        DisableMenu(disableType);

        if (enableType != MenuType.None)
        {
            if (waitForAnimationEnd && disablePage.UseAnimation)
            {
                Menu enablePage = GetMenu(enableType);

                if (_menuExitCoroutine != null)
                {
                    StopCoroutine(_menuExitCoroutine);
                }

                _menuExitCoroutine = StartCoroutine(WaitForMenuExit(enablePage, disablePage));
            }
            else
            {
                EnableMenu(enableType);
            }
        }
    }

    private IEnumerator WaitForMenuExit(Menu enablePage, Menu disablePage)
    {
        while (disablePage.TargetState != Menu.InitialState)
        {
            yield return null;
        }

        EnableMenu(enablePage.Type);
    }

    private Menu GetMenu(MenuType type)
    {
        if (!MenuExists(type))
        {
            Debug.LogWarning("Strona tego typu nie istnieje");
            return null;
        }

        return (Menu)_menusByType[type];
    }

    private bool MenuExists(MenuType type)
    {
        return _menusByType.ContainsKey(type);
    }
}
