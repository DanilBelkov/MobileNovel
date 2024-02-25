using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private float _speedTimer = 1;
    private float _percentPosition;
    private bool _isRunning;
    private float _heightBar;

    public static event Action OnMissDialog;

    private void Awake()
    {
        _heightBar = _container.rect.height;//.sizeDelta.y;
    }
    private void OnEnable()
    {
        SelectedHandler.OnSetTimer += SetTimer;
    }
    private void OnDisable()
    {
        SelectedHandler.OnSetTimer -= SetTimer;
    }
    /// <summary>
    /// Start/stop timer
    /// </summary>
    /// <param name="value">
    /// true - start
    /// false - stop
    /// </param>
    public void SetTimer(bool? value)
    {
        var defaultValue = value ?? false;
        _isRunning = defaultValue;
        _container.gameObject.SetActive(defaultValue);
        if (defaultValue)
            _percentPosition = 0;

    }
    private void SetPosition(float percentPosition)
    {
        if (percentPosition > 100)
        {
            OnMissDialog?.Invoke();
            SetTimer(false);
            return;
        }
        _container.offsetMax = new Vector2(0, -_heightBar * percentPosition / 100);
    }
    private void Update()
    {
        if (_isRunning)
        {
            _percentPosition += Time.deltaTime * _speedTimer;
            SetPosition(_percentPosition);
        }
    }
}
