using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;

    private Ship _ship;
    private const int SCORE_FOR_TARGET = 100;
    private int _score, _highScore;

    public int Score { get => _score; }
    public int HighScore { get => _highScore; }

    private void Awake()
    {
        _score = 0;
        _highScore = PlayerPrefs.GetInt("High Score", 0);
        ShowScoreInformation();
    }

    private void Start()
    {
        _ship = FindObjectOfType<Ship>();
        _ship.OnHittedEnemy += CollectScore;
    }

    private void OnDisable()
    {
        _ship.OnHittedEnemy -= CollectScore;
    }

    private void CollectScore()
    {
        _score += SCORE_FOR_TARGET;
        ShowScoreInformation();

        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("High Score", _highScore);
        }

    }

    private void ShowScoreInformation()
    {
        _scoreText.text = "SCORE: " + _score;
        _highScoreText.text = "HIGH  SCORE:" + _highScore;
    }

}
