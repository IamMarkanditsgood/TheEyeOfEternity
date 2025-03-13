using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodInfo : BasicScreen
{
    [SerializeField] private GodConfig[] _gods;

    [SerializeField] private Image godBG;
    [SerializeField] private Image godInfo;
    [SerializeField] private Button _close;
    public GodTypes god = GodTypes.Ra;

    void Start()
    {
        _close.onClick.AddListener(Close);
    }

    void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
    }

    public override void SetScreen()
    {
        GodConfig currentGod = null;
        foreach(var godConfig in _gods)
        {
            if(godConfig.types == god)
            {
                currentGod = godConfig;
            }
        }
        godBG.sprite = currentGod.godBG;
        godInfo.sprite = currentGod.godInfo;
    }

    public override void ResetScreen()
    {
    }
    private void Close()
    {
        UIManager.Instance.HideScreen(ScreenTypes.GodInfo);
        UIManager.Instance.ShowScreen(ScreenTypes.Gods);
    }
}
