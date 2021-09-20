using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;

    private const int SCORE_FOR_TARGET = 100;
    private int _score, _highScore;

    private void Awake()
    {
        _score = 0;
        _highScore = PlayerPrefs.GetInt("High Score", 0);
        ShowScoreInformation();
    }

    public void CollectScore()
    {
        _score += SCORE_FOR_TARGET;

        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("High Score", _highScore);
        }

        ShowScoreInformation();
    }

    private void ShowScoreInformation()
    {
        _scoreText.text = "SCORE: " + _score;
        _highScoreText.text = "HIGH  SCORE:" + _highScore;
    }

}
