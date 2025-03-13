using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _Info;
    [SerializeField] private Button _Gods;

    private void Start()
    {
        _Info.onClick.AddListener(Info);
        _Gods.onClick.AddListener(God);
    }

    private void OnDestroy()
    {
        _Info.onClick.RemoveListener(Info);
        _Gods.onClick.RemoveListener(God);

    }

    private void Info()
    {
        Debug.Log("info");
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }
    private void God()
    {
        Debug.Log("God");
        UIManager.Instance.ShowScreen(ScreenTypes.Gods);
    }
}
