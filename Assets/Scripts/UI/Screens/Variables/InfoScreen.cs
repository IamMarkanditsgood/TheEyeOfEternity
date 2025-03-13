using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScreen : BasicScreen
{
    [SerializeField] private Button _close;
    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
    }

    private void Start()
    {
        _close.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
    }
    private void Close()
    {
        UIManager.Instance.HideScreen(ScreenTypes.Info);
    }
}
