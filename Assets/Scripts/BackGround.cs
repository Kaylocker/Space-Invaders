using UnityEngine;

public class BackGround : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _speed = 0.1f;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.y <= -_startPosition.y)
        {
            transform.position = _startPosition;
        }
    }

    private void FixedUpdate()
    {
        transform.position += _speed * Time.deltaTime * Vector3.down;
    }
}
