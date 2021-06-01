using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public ScoreKeeping _scoreKeeping;
    [SerializeField]
    private InputField _playernameInputField;
    private bool _highscoreBreached = false;

    public Action<int> OnGameOver; //int returns score
    [SerializeField] private GameObject _nameInputUI;
    [SerializeField] private GameObject _scoreBoardUI;

    [SerializeField] private Text _endScoreTextOfInput;
    [SerializeField] private Text _endScoreTextOfScoreboard;
    [SerializeField] private Text _scoreboardHighscoreName; // name + highscore of highest score
    private int _displayScore;
    private string _displayName;
    private string _displayString;
    
    public static GameOverManager _instance;
    void Awake()
    {
        _instance = this;
    }
    
    private void Start()
    {
        _nameInputUI.SetActive(false);
        _scoreBoardUI.SetActive(false);
        OnGameOver += GameOverAction;
        UpdateScoreBoardText();
    }

    private void GameOverAction(int score)
    {
        StartCoroutine(Finish());
    }

    private void UpdateScoreBoardText()
    {
        _endScoreTextOfInput.text = "Jouw score: " + _scoreKeeping._currentScore;
        _endScoreTextOfScoreboard.text = _endScoreTextOfInput.text;
        _displayName = PlayerPrefs.GetString("HighscoreName");
        _displayScore = PlayerPrefs.GetInt("Highscore");
        _displayString = "Naam: " + _displayName + "\n" + "Score: " + _displayScore;
        _scoreboardHighscoreName.text = _displayString;
        _highscoreBreached = _scoreKeeping._HighscoreBreached;
    }
    
    public void SavePlayerName()
    {
        PlayerPrefs.SetString("HighscoreName", _playernameInputField.text);
        UpdateScoreBoardText();
        _nameInputUI.SetActive(false);
    }

    private IEnumerator Finish()
    {
        UpdateScoreBoardText();
        Time.timeScale = 0;
        if (_highscoreBreached)
        {
            _scoreBoardUI.SetActive(true);
            _nameInputUI.SetActive(true);
        }
        else
        {
            _scoreBoardUI.SetActive(true);
        }

        yield return null;
    }
}
