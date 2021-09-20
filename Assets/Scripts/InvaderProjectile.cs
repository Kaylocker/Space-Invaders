using UnityEngine;

public class InvaderProjectile : MonoBehaviour
{
    private float _limit;

    private void Start()
    {
        GetLimitAxis();
    }

    private void Update()
    {
        if (transform.position.y <= -_limit)
        {
            Destroy(gameObject);
        }
    }

    private void GetLimitAxis()
    {
        float offSetHeight = 2f;
        float height = Camera.main.orthographicSize * offSetHeight;
        _limit = height / 2 + offSetHeight;
    }
}
