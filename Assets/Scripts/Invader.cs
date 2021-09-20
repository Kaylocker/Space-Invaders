using UnityEngine;

public class Invader : MonoBehaviour, IShootable
{
    private InvaderManager _invaderManager;
    private GameObject _projectile;
    private Vector3 _position;
    private Vector3 _currentDirection;
    private float _speed = 0, _limit, _projectileForce = 0;
    private int _id;

    public Vector3 CurrentDirection { get => _currentDirection; set => _currentDirection = value; }
    public InvaderManager InvaderManager
    {
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
    public int ID
    {
        get => _id;

        set => _id = value;
    }
    public float Speed
    {
        get => _speed;
        set
        {
            if (_speed == 0 && value>0)
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
            if (_projectileForce == 0 && value>0)
            {
                _projectileForce = value;
            }
        }

    }

    private void Awake()
    {
        GetLimitAxis();
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

    public void InvaderDestroyed()
    {
        _invaderManager.RemoveDestroyedInviderFromList(_id);
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(_projectile, _position, Quaternion.identity);
        Rigidbody2D rocketRigidBody = projectile.GetComponent<Rigidbody2D>();
        rocketRigidBody.AddRelativeForce(Vector2.down * _projectileForce);
    }
}

