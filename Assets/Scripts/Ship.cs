using System.Collections;
using UnityEngine;
using System;

public class Ship : MonoBehaviour, IShootable
{
    public event Action OnGetHitted;
    public event Action OnShoot;
    public event Action OnHittedEnemy;
    public event Action OnDied;
    public event Action OnHittedProjectile;

    [SerializeField] private GameObject _rocket;

    private Rigidbody2D _rigidBody;
    private Vector3 _position;
    private float _limitDistance = 9f;
    private float _reloadTime = 0.3f, _speed = 600f, _rocketForce = 500f;
    private bool _canFire = true;
    private int _lives = 3;

    public int Lives { get => _lives; }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Shoot();
        CheckDeath();
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

    private void CheckDeath()
    {
        if (_lives <= 0)
        {
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canFire)
        {
            _canFire = false;

            OnShoot?.Invoke();

            GameObject rocketGameObject = Instantiate(_rocket, _position, Quaternion.identity);
            Rocket rocket = rocketGameObject.gameObject.GetComponent<Rocket>();
            Rigidbody2D rocketRigidBody = rocketGameObject.GetComponent<Rigidbody2D>();
            rocketRigidBody.AddRelativeForce(Vector2.up* _rocketForce);
            rocket.Ship = this;

            StartCoroutine(Reload());
        }
    }

    public void HittingInvader()
    {
        OnHittedEnemy?.Invoke();
    }

    public void HittingProjectile()
    {
        OnHittedProjectile?.Invoke();
    }

    private void HittingShip()
    {
        _lives--;

        if(_lives <= 0)
        {
            _lives = 0;
        }

        OnGetHitted?.Invoke();
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canFire = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvaderProjectile invaderProjectile = collision.gameObject.GetComponent<InvaderProjectile>();

        if (invaderProjectile != null)
        {
            HittingShip();
            Destroy(collision.gameObject);
            return;
        }

        Invader invader = collision.gameObject.GetComponent<Invader>();

        if (invader != null)
        {
            _lives = 0;
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
