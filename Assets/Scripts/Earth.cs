using UnityEngine;
using System;
using UnityEngine.UI;

public class Earth : MonoBehaviour
{
    [SerializeField] Text _earthLives;
    [SerializeField] GameObject _hitExplosion;

    public event Action OnDestroyed;
    public event Action OnHitted;

    private int _lives = 30;

    public int Lives { get => _lives; }

    private void Awake()
    {
        ShowCurrentLives();
    }

    private void Update()
    {
        if (_lives <= 0)
        {
            OnDestroyed?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvaderProjectile invaderProjectile = collision.gameObject.GetComponent<InvaderProjectile>();

        if (invaderProjectile != null)
        {
            _lives--;

            OnHitted?.Invoke();

            GameObject hitExplosion = Instantiate(_hitExplosion, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(hitExplosion, hitExplosion.gameObject.GetComponent<ParticleSystem>().main.startLifetime.constantMax);

            ShowCurrentLives();
        }
    }

    private void ShowCurrentLives()
    {
        _earthLives.text = "EARTH   LIVES" + Environment.NewLine + (_lives);
    }
}
