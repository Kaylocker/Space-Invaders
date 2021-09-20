using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Ship _ship;
    private float _limitDistance = 6f;

    public Ship Ship
    {
        set
        {
            if (_ship == null)
            {
                _ship = value;
            }
        }
    }

    private void Update()
    {
        if (transform.position.y > _limitDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invader invader = collision.gameObject.GetComponent<Invader>();

        if (invader!=null)
        {
            _ship.HittingInvader();
            invader.InvaderDestroyed();
            Destroy(gameObject);
            Destroy(collision.gameObject);
            return;
        }

        InvaderProjectile invaderProjectile = collision.gameObject.GetComponent<InvaderProjectile>();

        if (invaderProjectile!=null)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }
}
