using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject _scoreManagerGameobject;
    [SerializeField] private GameObject _rocket;
    [SerializeField] private Text _shipLivesText;

    private Vector3 _position;
    private ScoreManager _scoreManagerComponent;
    private Rigidbody2D _rigidBody;
    private float _limitDistance = 9f;
    private float _reloadTime = 0.5f, _speed = 600f;
    private bool _canFire = true;
    private int _shipLives = 3;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _scoreManagerComponent = _scoreManagerGameobject.GetComponent<ScoreManager>();
        ShowLivesInformation();
    }

    private void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    private void LateUpdate()
    {
        MovementLimiter();
    }

    private void MovementLogic()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector3(horizontalInput * _speed * Time.deltaTime, 0, 0);
    }

    private void MovementLimiter()
    {
        _position = transform.position;

        if (_position.x> _limitDistance)
        {
            _position.x = _limitDistance;
        }
        else if(_position.x < -_limitDistance)
        {
            _position.x = -_limitDistance;
        }

        transform.position = _position;
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canFire)
        {
            _canFire = false;

            GameObject rocketGameObject = Instantiate(_rocket, _position, Quaternion.identity);
            Rocket rocket = rocketGameObject.gameObject.GetComponent<Rocket>();
            Rigidbody2D rocketRigidBody = rocketGameObject.GetComponent<Rigidbody2D>();
            rocketRigidBody.AddRelativeForce(Vector2.up*500);
            rocket.Ship = this;

            StartCoroutine(Reload());
        }
    }

    public void HittingInvader()
    {
        _scoreManagerComponent.CollectScore();
    }

    private void HittingShip(GameObject projectile)
    {
        Destroy(projectile);
        _shipLives--;

        if (_shipLives <= 0)
        {
            _shipLives = 0;
            PlayerPrefs.Save();
            Destroy(gameObject);
        }

        ShowLivesInformation();
    }

    private void ShowLivesInformation()
    {
        _shipLivesText.text = "SHIP  LIVES: " + _shipLives;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canFire = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvaderProjectile invaderProjectile = collision.gameObject.GetComponent<InvaderProjectile>();

        if (invaderProjectile != null)
        {
            HittingShip(collision.gameObject);
        }
    }
}
