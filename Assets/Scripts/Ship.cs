using System.Collections;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float _speed = 600f;
    [SerializeField] private GameObject _rocket;

    private Vector3 _position;
    private Rigidbody2D _rigidBody;
    private float _limitDistance = 9f;
    private float _reloadTime = 0.5f;
    private bool _canFire = true;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        Fire();
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

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canFire)
        {
            _canFire = false;

            GameObject rocket = Instantiate(_rocket, _position, Quaternion.identity);
            Rigidbody2D rocketRigidBody = rocket.GetComponent<Rigidbody2D>();
            rocketRigidBody.AddRelativeForce(Vector2.up*500);

            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canFire = true;
    }

}
