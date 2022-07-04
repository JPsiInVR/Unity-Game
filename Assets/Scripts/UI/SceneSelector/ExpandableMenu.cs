using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ExpandableMenu : MonoBehaviour
{
    public bool IsExpanded { get; private set; }
    
    [SerializeField] private Vector2 itemsSpacing;
    
    [Space]
    [Header("Animation")]
    [SerializeField] private float expandDuration;
    [SerializeField] private Ease expandEase;
    [SerializeField] private float collapseDuration;
    [SerializeField] private Ease collapseEase;

    [Space] 
    [Header("Fade")] 
    [SerializeField] private float expandFadeDuration;
    [SerializeField] private float collapseFadeDuration;
    

    
    private Button _mainButton;
    private Vector2 _mainButtonPosition;
    private int _itemsCount;
    private List<ExpandableMenuItem> _menuItems = new List<ExpandableMenuItem>();
    
    private void Start()
    {
        _menuItems = new List<ExpandableMenuItem>();
        _itemsCount = transform.childCount - 1;

        for (int i = 0; i < _itemsCount; i++)
        {
            _menuItems.Add(transform.GetChild(i + 1).GetComponent<ExpandableMenuItem>());
        }

        _mainButton = transform.GetChild(0).GetComponent<Button>();
        _mainButton.onClick.AddListener(ToggleMenu);    
        _mainButton.transform.SetAsLastSibling();
        _mainButtonPosition = _mainButton.GetComponent<RectTransform>().anchoredPosition;
        
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _itemsCount; i++)
        {
            _menuItems[i].RectTransform.anchoredPosition = _mainButtonPosition;
            _menuItems[i].Image.DOFade(0, 0);
            _menuItems[i].Text.DOFade(0, 0);
            _menuItems[i].ParticleSystem.Stop();
        }
    }

    public void ToggleMenu()
    {
        if (!IsExpanded)
        {
            ExpandableMenuManager.CloseAll();
        }
        
        IsExpanded = !IsExpanded;

        if (IsExpanded)
        {
            for (int i = 0; i < _itemsCount; i++)
            {
                _menuItems[i].RectTransform.DOAnchorPos3D(_mainButtonPosition + itemsSpacing * (i + 1), expandDuration).SetEase(expandEase);
                _menuItems[i].Image.DOFade(1, expandFadeDuration).From(0);
                _menuItems[i].Text.DOFade(1, expandFadeDuration).From(0);
                _menuItems[i].ParticleSystem.Play();

            }
        }
        else
        {
            for (int i = 0; i < _itemsCount; i++)
            {
                _menuItems[i].RectTransform.DOAnchorPos3D(_mainButtonPosition, collapseDuration).SetEase(collapseEase);
                _menuItems[i].Image.DOFade(0, collapseFadeDuration);
                _menuItems[i].Text.DOFade(0, collapseFadeDuration);
                _menuItems[i].ParticleSystem.Stop();
                _menuItems[i].ParticleSystem.Clear();
            }
        }
    }

    private void OnDestroy()
    {
        _mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
