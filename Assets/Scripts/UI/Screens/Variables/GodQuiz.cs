using System.Collections;
using System.Collections.Generic;
using JSG.WordPalace.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodQuiz : BasicScreen
{
    [SerializeField] private DataStorage _dataStorage;

    [SerializeField] private Image _bgGod;
    [SerializeField] private Image[] _points;
    [SerializeField] private List<int> replies;

    [SerializeField] private Sprite _currentPoint;
    [SerializeField] private Sprite _defaultPoint;
    [SerializeField] private Sprite _corectPoint;
    [SerializeField] private Sprite _incorectPoint;

    [SerializeField] private Button[] _answersButton;
    [SerializeField] private Image[] _answers;
    [SerializeField] private TMP_Text[] _answerText;

    [SerializeField] private Sprite _defaultButton;
    [SerializeField] private Sprite _selecterdButton;
    [SerializeField] private Sprite _correctButton;
    [SerializeField] private Sprite _incorrectButton;

    [SerializeField] private TMP_Text _question;
    [SerializeField] private Button _reply;
    [SerializeField] private Button _next;

    [SerializeField] private GodConfig[] _godconfigs;

    [SerializeField] private Button _close;

    [SerializeField] private GameObject _winPopup;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _winResultText;
    [SerializeField] private Button _home;
    [SerializeField] private Button _tryAgain;


    public GodTypes GodType;
    private GodConfig currentGod;

    private int _currentQuestion;
    private int _coosedReply = -1;
    private bool _isWaitForReply;


    void Start()
    {
        _close.onClick.AddListener(Close);
        _reply.onClick.AddListener(Reply);
        _next.onClick.AddListener(Next);
        _home.onClick.AddListener(Home);
        _tryAgain.onClick.AddListener(TryAgain);

        for (int i = 0; i < _answersButton.Length; i++)
        {
            int index = i;
            _answersButton[index].onClick.AddListener(() => ChooseAnswer(index));
        }
    }

    void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
        _reply.onClick.RemoveListener(Reply);
        _next.onClick.RemoveListener(Next);
        _home.onClick.RemoveListener(Home);
        _tryAgain.onClick.RemoveListener(TryAgain);

        for (int i = 0; i < _answersButton.Length; i++)
        {
            int index = i;
            _answersButton[index].onClick.RemoveListener(() => ChooseAnswer(index));
        }
    }

    public override void SetScreen()
    {

        foreach (var godConfig in _godconfigs)
        {
            if (godConfig.types == GodType)
            {
                currentGod = godConfig;
            }
        }
        _bgGod.sprite = currentGod.godBG;
        replies.Clear();
        _currentQuestion = 0;
        SetQuestion();
    }

    public override void ResetScreen()
    {
    }

    private void SetQuestion()
    {
        if (_currentQuestion < currentGod.godQuizzes.Length)
        {
            _isWaitForReply = true;
            foreach (var answer in _answers)
            {
                answer.sprite = _defaultButton;
            }
            _reply.interactable = false;
            _next.gameObject.SetActive(false);
            _coosedReply = -1;
            SetPoints();
            _question.text = currentGod.godQuizzes[_currentQuestion].question;

            for (int i = 0; i < _answerText.Length; i++)
            {
                _answerText[i].text = currentGod.godQuizzes[_currentQuestion]._answers[i];
            }
        }
        else
        {
            int correctAnswers = 0;
            foreach (var reply in replies)
            {
                if (reply == 1)
                { correctAnswers++; }
            }
            int newWinScore = (correctAnswers * 10) + _dataStorage.Coin;
            _dataStorage.EarnCoin(newWinScore);
            _winPopup.SetActive(true);
            _winResultText.text = "+" + correctAnswers * 10;
            _winText.text = "You answered " + correctAnswers + "/10\n" + "questions correctly!";
        }
    }

    private void SetPoints()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            if (_currentQuestion == i)
            {
                _points[i].sprite = _currentPoint;
            }
            else if (i > _currentQuestion)
            {
                _points[i].sprite = _defaultPoint;
            }
        }
        for (int i = 0; i < replies.Count; i++)
        {
            if (replies[i] == 1)
            {
                _points[i].sprite = _corectPoint;
            }
            else if (replies[i] == -1)
            {
                _points[i].sprite = _incorectPoint;
            }
        }
    }

    private void Reply()
    {
        bool isCorrect = CheckReply();
        _isWaitForReply = false;
        if (isCorrect)
        {
            _next.gameObject.SetActive(true);
            _answers[_coosedReply].sprite = _correctButton;
            _points[_currentQuestion].sprite = _corectPoint;
            replies.Add(1);
        }
        else
        {
            _next.gameObject.SetActive(true);
            _answers[_coosedReply].sprite = _incorrectButton;
            _points[_currentQuestion].sprite = _incorectPoint;
            replies.Add(-1);
        }
    }

    private void Next()
    {
        _currentQuestion++;
        SetQuestion();
    }

    private void ChooseAnswer(int index)
    {
        if (_isWaitForReply)
        {
            foreach (var answer in _answers)
            {
                answer.sprite = _defaultButton;
            }
            _answers[index].sprite = _selecterdButton;
            _coosedReply = index;
            _reply.interactable = true;
        }
    }

    private bool CheckReply()
    {

        if (_coosedReply == currentGod.godQuizzes[_currentQuestion].correctReply)
        {
            return true;
        }
        return false;
    }

    private void Close()
    {
        UIManager.Instance.HideScreen(ScreenTypes.GodQuiz);
        UIManager.Instance.ShowScreen(ScreenTypes.Gods);
    }

    private void Home()
    {
        UIManager.Instance.HideScreen(ScreenTypes.GodQuiz);
        _winPopup.SetActive(false);
    }

    private void TryAgain()
    {
        UIManager.Instance.HideScreen(ScreenTypes.GodQuiz);
        _winPopup.SetActive(false);
        UIManager.Instance.ShowScreen(ScreenTypes.GodQuiz);
    }
}
