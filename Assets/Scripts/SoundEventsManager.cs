using UnityEngine;

public class SoundEventsManager : MonoBehaviour
{
    [SerializeField] Earth _earth;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] InvaderManager _invaderManager;

    [SerializeField] AudioSource _explosion;
    [SerializeField] AudioSource _shipShoot;
    [SerializeField] AudioSource _invaderShoot;
    [SerializeField] AudioSource _shipHitted;
    [SerializeField] AudioSource _earthDestroyed; 

    private Ship _ship;

    private void Start()
    {
        _ship = _playerManager.Ship;

        _ship.OnShoot += PlayShipShoot;
        _ship.OnGetHitted += PlayShiptGetHitted;
        _ship.OnDied += PlayExplosion;
        _ship.OnHittedEnemy += PlayExplosion;
        _ship.OnHittedProjectile += PlayExplosion;
        _earth.OnHitted += PlayExplosion;
        _earth.OnDestroyed += PlayEarthDestroying;
        _invaderManager.OnShoot += PlayInvaderShoot;
    }

    private void OnDisable()
    {
        _ship.OnShoot -= PlayShipShoot;
        _ship.OnGetHitted -= PlayShiptGetHitted;
        _ship.OnDied -= PlayExplosion;
        _ship.OnHittedEnemy -= PlayExplosion;
        _ship.OnHittedProjectile -= PlayExplosion;
        _earth.OnHitted -= PlayExplosion;
        _earth.OnDestroyed -= PlayEarthDestroying;
        _invaderManager.OnShoot -= PlayInvaderShoot;
    }

    private void PlayExplosion()
    {
        _explosion.Play();
    }

    private void PlayShipShoot()
    {
        _shipShoot.Play();
    }

    private void PlayShiptGetHitted()
    {
        _shipHitted.Play();
    }

    private void PlayInvaderShoot()
    {
        _invaderShoot.Play();
    }

    private void PlayEarthDestroying()
    {
        _earthDestroyed.Play();
    }
}
