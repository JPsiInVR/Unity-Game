using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardboardInteractable : MonoBehaviour
{
    public UnityEvent onHoverEnter = new UnityEvent();
    public UnityEvent onHoverExit = new UnityEvent();
    public UnityEvent onSelectEnter = new UnityEvent();
    public UnityEvent onSelectExit = new UnityEvent();

    public static event Action<float> OnHoverStay;
    public static event Action OnHoverEnter;
    public static event Action OnHoverExit;

    private Button Button
    {
        get
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
            
            return _button;
        }
    }

    public bool Interactable
    {
        get => _interactable;

        set
        {
            if(Button != null)
            {
                Button.interactable = value;
            }

            _interactable = value;
        }
    }

    [SerializeField]
    private float _gazeTime;

    [SerializeField]
    private bool _interactable = true;

    private float _calculatedGazeTime;
    private float _gazeStartTime;
    private float _gazeProgress;
    private bool _isHovering;
    private bool _isSelecting;
    private Button _button;

    private void Awake()
    {
        if (Button != null)
        {
            onSelectExit.AddListener(() =>
            {
                if (Interactable)
                {
                    Button.onClick.Invoke();    
                }
            });
            
            onSelectEnter.AddListener(() =>
            {
                if (Interactable)
                {
                    Button.onClick.Invoke();    
                }
            }); 
        }


        if (_gazeTime == 0)
        {
            _calculatedGazeTime = XRCardboardController.Instance.GlobalGazeTime / 2 + XRCardboardController.Instance.GlobalGazeTime * GameData.Instance.GazeTime;

        }
        else
        {
            _calculatedGazeTime = _gazeTime / 2 + _gazeTime * GameData.Instance.GazeTime;
        }
    }

    private void Update()
    {
        if(!Interactable) return;

        if(_isHovering && _gazeStartTime >= 0)
        {
            if (Time.time - _gazeStartTime > _calculatedGazeTime || XRCardboardController.Instance.IsTriggerPressed())
            {
                _gazeProgress = 1;
                HandleInteraction();
            }
            else
            {
                _gazeProgress = (Time.time - _gazeStartTime) / _calculatedGazeTime;
            }

            OnHoverStay?.Invoke(_gazeProgress);
        }
    }

    public void PointerEnter()
    {
        if(!Interactable) return;
        
        _isHovering = true;
        _gazeStartTime = Time.time;
        onHoverEnter.Invoke();
        OnHoverEnter?.Invoke();
    }

    public void PointerExit()
    {
        if (!Interactable) return;
        _isHovering = false;
        _gazeStartTime = -1;
        onHoverExit.Invoke();
        OnHoverExit?.Invoke();
    }

    private void HandleInteraction()
    {
        if (_isSelecting)
        {
            _gazeStartTime = -1;
            _isSelecting = false;
            onSelectExit?.Invoke();
        }
        else if (_isHovering)
        {
            _gazeStartTime = -1;
            _isSelecting = true;
            onSelectEnter?.Invoke();
        }
    }
}
