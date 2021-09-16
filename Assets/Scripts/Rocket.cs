using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float _limitDistance = 6f;

    private void Update()
    {
        if (transform.position.y > _limitDistance)
        {
            Destroy(gameObject);
        }
    }
}
