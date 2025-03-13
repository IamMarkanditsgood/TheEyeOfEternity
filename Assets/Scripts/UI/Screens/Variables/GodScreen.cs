using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodScreen : BasicScreen
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Button _quizButton;
    [SerializeField] private Button _close;

    [SerializeField] private TMP_Text _godName;
    [SerializeField] private Image _godImage;

    [SerializeField] private GodConfig[] gods;

    private int currentGod;

    private void Start()
    {
        _close.onClick.AddListener(Close);
        _nextButton.onClick.AddListener(NextGod);
        _prevButton.onClick.AddListener(PrevGod);
        _infoButton.onClick.AddListener(InfoGod);
        _quizButton.onClick.AddListener(QuizGod);
    }

    private void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
        _nextButton.onClick.RemoveListener(NextGod);
        _prevButton.onClick.RemoveListener(PrevGod);
        _infoButton.onClick.RemoveListener(InfoGod);
        _quizButton.onClick.RemoveListener(QuizGod);
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        _godImage.sprite = gods[currentGod].godAvatar;
        _godName.text = gods[currentGod].name;
    }

    private void NextGod()
    {
        if(currentGod < gods.Length - 1)
        {
            currentGod++;
            SetScreen();
        }
    }

    private void PrevGod()
    {

        if (currentGod > 0)
        {
            currentGod--;
            SetScreen();
        }
    }

    private void InfoGod()
    {
        GodInfo godInfo = (GodInfo) UIManager.Instance.GetScreen(ScreenTypes.GodInfo);
        godInfo.god = gods[currentGod].types;
        UIManager.Instance.ShowScreen(ScreenTypes.GodInfo);
    }

    private void QuizGod()
    {
        GodQuiz godInfo = (GodQuiz)UIManager.Instance.GetScreen(ScreenTypes.GodQuiz);
        godInfo.GodType = gods[currentGod].types;
        UIManager.Instance.ShowScreen(ScreenTypes.GodQuiz);
    }
    private void Close()
    {
        UIManager.Instance.HideScreen(ScreenTypes.Gods);
    }
}
