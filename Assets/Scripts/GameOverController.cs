using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] GameObject _gameOverMenu;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] Text _scoreEndGame;
    [SerializeField] Text _highScoreEndGame;

    private ScoreManager _scoreManagerComponent;
    private Earth _earth;
    private Ship _ship;

    private void Start()
    {
        _gameOverMenu.SetActive(false);

        _earth = FindObjectOfType<Earth>();
        _scoreManagerComponent = FindObjectOfType<ScoreManager>();

        _ship = _playerManager.Ship;

        _earth.OnDestroyed += SetActiveGameOverMenu;
        _ship.OnDied += SetActiveGameOverMenu;
    }

    private void OnDisable()
    {
        _earth.OnDestroyed -= SetActiveGameOverMenu;
        _ship.OnDied -= SetActiveGameOverMenu;
    }

    public void SetActiveGameOverMenu()
    {
        _gameOverMenu.SetActive(true);
        _scoreEndGame.text = "SCORE: " + _scoreManagerComponent.Score.ToString();
        _highScoreEndGame.text = "HIGH SCORE: " + _scoreManagerComponent.HighScore.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Play");
    }

    public void ShowMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
