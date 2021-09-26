using UnityEngine;

public class Invader : MonoBehaviour, IShootable
{
    private InvaderManager _invaderManager;
    private GameObject _explosionParticle, _projectile;
    private Vector3 _position;
    private Vector3 _currentDirection;
    private float _speed = 0, _limit, _projectileForce = 0;
    private int _id;

    public Vector3 CurrentDirection { get => _currentDirection; set => _currentDirection = value; }
    public InvaderManager InvaderManager
    {
        get => _invaderManager;

        set
        {
            if (_invaderManager == null)
            {
                _invaderManager = value;
            }
        }
    }
    public GameObject Projectile
    {
        get => _projectile;

        set
        {
            if (_projectile == null)
            {
                _projectile = value;
            }
        }
    }
    public GameObject ExplosionParticle
    {
        set
        {
            if (_explosionParticle == null)
            {
                _explosionParticle = value;
            }
        }
    }
    public int ID { get => _id; set => _id = value; }
    public float Speed
    {
        get => _speed;

        set
        {
            if (_speed == 0 && value > 0)
            {
                _speed = value;
            }
        }

    }
    public float ProjectileForce
    {
        get => _projectileForce;

        set
        {
            if (_projectileForce == 0 && value > 0)
            {
                _projectileForce = value;
            }
        }

    }

    private void Awake()
    {
        GetLimitAxis();
    }

    private void Start()
    {
        _invaderManager.OnChangeDirection += ChangeDirection;
        _invaderManager.OnMakeStepDown += MakeStepDown;
    }

    private void OnDisable()
    {
        _invaderManager.OnChangeDirection -= ChangeDirection;
        _invaderManager.OnChangeDirection -= MakeStepDown;
    }

    private void FixedUpdate()
    {
        MovementLogic();
        CheckCurrentPosition();
    }

    private void GetLimitAxis()
    {
        float offSetHeight = 2f, offSet = 0.5f;
        float height = Camera.main.orthographicSize * offSetHeight;
        float width = height * Camera.main.aspect;
        _limit = width / 2 - offSet;
    }

    private void MovementLogic()
    {
        _position = transform.position;
        transform.position += _speed * Time.deltaTime * _currentDirection;
    }

    private void CheckCurrentPosition()
    {
        if (transform.position.x >= _limit || transform.position.x <= -_limit)
        {
            _invaderManager.SetNewDirection(_currentDirection);
        }
    }

    private void ChangeDirection(Vector3 direction)
    {
        _currentDirection = direction;
    }

    private void MakeStepDown(Vector3 newPosition)
    {
        if (this != null)
        {
            transform.position -= newPosition;
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(_projectile, _position, Quaternion.identity);
        Rigidbody2D rocketRigidBody = projectile.GetComponent<Rigidbody2D>();
        rocketRigidBody.AddRelativeForce(Vector2.down * _projectileForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rocket rocket = collision.gameObject.GetComponent<Rocket>();

        if (rocket != null)
        {
            _invaderManager.RemoveDestroyedInviderFromList(_id);
            GameObject explosionParticle = Instantiate(_explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosionParticle, explosionParticle.gameObject.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
        }
    }
}

