using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject _shipPrefab;
    [SerializeField] Text _shipLivesText;

    private Ship _ship;
    private GameObject _shipGameObject;
    private Vector3 _startPosition = new Vector3(0f, -3.3f, 0f);

    public Ship Ship { get => _ship; }

    private void Awake()
    {
        _shipGameObject = Instantiate(_shipPrefab, _startPosition, Quaternion.identity);
        _ship = _shipGameObject.GetComponent<Ship>();
        _ship.OnGetHitted += ShowLivesInformation;
        ShowLivesInformation();
    }

    private void OnDisable()
    {
        _ship.OnGetHitted -= ShowLivesInformation;
    }

    public void ShowLivesInformation()
    {
        _shipLivesText.text = "SHIP  LIVES: " + _ship.Lives;
    }
}
