using System.Collections.Generic;
using UnityEngine;

public class ExpandableMenuManager : MonoBehaviour
{
    private static List<ExpandableMenu> _expandableMenus;
    private void Start()
    { 
        _expandableMenus = new List<ExpandableMenu>(GetComponentsInChildren<ExpandableMenu>(true));
    }

    public static void CloseAll()
    {
        foreach (var expandableMenu  in _expandableMenus)
        {
            if (expandableMenu.IsExpanded)
            {
                expandableMenu.ToggleMenu();
            }
        }
    }
}
