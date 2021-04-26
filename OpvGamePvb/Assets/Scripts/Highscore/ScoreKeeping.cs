using UnityEngine;
using UnityEngine.UI;

    public class ScoreKeeping : MonoBehaviour
    { 
        [SerializeField]  
    private Text _currentScoreText;
    [SerializeField]
    private Text _HighScoreText;
    public int _currentScore = 0;
    public bool _HighscoreBreached = false;

    private int _HighScore = 0;
    // Start is called before the first frame update
    void Start()
    {   
        
        if(PlayerPrefs.GetInt("Highscore" )!= 0)
        {
            _HighScore = PlayerPrefs.GetInt("Highscore");
        }
        UpdateScore(0);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void UpdateScore(int scoreAddition)
    {
        
        _currentScore += scoreAddition;
        if (_HighScore < _currentScore)
        {
            
            PlayerPrefs.SetInt("Highscore" , _currentScore);
            _HighScore = _currentScore;
            _HighscoreBreached = true;
        }
        _currentScoreText.text = "Current Score:" + "\n" + _currentScore;
        _HighScoreText.text = "Current Highscore:" + "\n" + _HighScore;
    }
}
