using UnityEngine;
using System.Collections.Generic;
using System;

public class InvaderManager : MonoBehaviour
{
    public event Action OnShoot;

    [SerializeField] private GameObject[] _invadersPrefab;
    [SerializeField] private GameObject[] _projectiles;
    [SerializeField] private GameObject _invaderExplosionParticle;

    private Vector3 _offSet = new Vector3(-5f, 3.75f, 0f);
    private Vector3 _distanceBetweenInvadersXaxis = new Vector3(1f, 0, 0);
    private Vector3 _distanceBetweenInvadersYaxis = new Vector3(0, 0.75f, 0);
    private Vector3 _rightDirection = new Vector3(1f, 0, 0);
    private Vector3 _leftDirection = new Vector3(-1f, 0, 0);

    private List<GameObject> _invadersGameObject;
    private List<Invader> _invadersComponents;
    private const int COLUMN = 10, ROW = 4;
    private const float START_PROJECTILE_FORCE = 100f, START_SPEED = 1f;
    private float _timeStartShooting = 2f, _repeatTimeShoot = 3.5f;
    private float _currentProjectileForce, _currentSpeed;
    private float _levelUpForce = 10f, _levelUpSpeed = 0.1f;

    public List<Invader> Invaders { get => _invadersComponents; }

    private void Awake()
    {
        _currentProjectileForce = START_PROJECTILE_FORCE;
        _currentSpeed = START_SPEED;
        SpawnInvaders();
    }

    private void Start()
    {
        InvokeRepeating("Shooting", _timeStartShooting, _repeatTimeShoot);
    }

    private void SpawnInvaders()
    {
        _invadersGameObject = new List<GameObject>();
        _invadersComponents = new List<Invader>();

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
        Vector3 currentPosition;
        spawnPosition += _offSet;

        int counter = 0;

        for (int i = 0; i < ROW; i++)
        {
            currentPosition = spawnPosition;

            for (int k = 0; k < COLUMN; k++)
            {
                currentPosition += _distanceBetweenInvadersXaxis;
                GameObject invaderGameObject = Instantiate(_invadersPrefab[i], currentPosition, Quaternion.identity);
                Invader invaderComponent = invaderGameObject.GetComponent<Invader>();

                SetInvaderSetting(invaderComponent, counter, i);

                _invadersGameObject.Add(invaderGameObject);
                _invadersComponents.Add(invaderComponent);

                counter++;
            }

            spawnPosition -= _distanceBetweenInvadersYaxis;
        }
    }

    public void SetNewDirection(Vector3 currentDirection)
    {
        if (currentDirection == _rightDirection)
        {
            foreach (Invader item in _invadersComponents)
            {
                item.CurrentDirection = _leftDirection;
            }
        }

        if (currentDirection == _leftDirection)
        {
            foreach (Invader item in _invadersComponents)
            {
                item.CurrentDirection = _rightDirection;
            }
        }

        MakeOneStepDown();
    }

    private void SetInvaderSetting(Invader invader, int id, int type)
    {
        invader.InvaderManager = this;
        invader.CurrentDirection = _leftDirection;
        invader.ID = id;
        invader.Projectile = _projectiles[type];
        invader.ProjectileForce = _currentProjectileForce;
        invader.Speed = _currentSpeed;
        invader.ExplosionParticle = _invaderExplosionParticle;
    }

    private void MakeOneStepDown()
    {
        float lerpValue = 1f;

        foreach (GameObject item in _invadersGameObject)
        {
            item.transform.position -= new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y + _distanceBetweenInvadersYaxis.y, lerpValue), transform.position.z);
        }
    }

    public void RemoveDestroyedInviderFromList(int id)
    {
        _invadersComponents.RemoveAt(id);
        _invadersGameObject.RemoveAt(id);

        CreateNewId();
    }

    private void CreateNewId()
    {
        int counter = 0;

        foreach (Invader item in _invadersComponents)
        {
            item.ID = counter;

            counter++;
        }
    }

    private void Shooting()
    {
        if (_invadersGameObject.Count == 0)
        {
            ResetInvadersGroup();
            return;
        }

        int indexCurrentInvader = UnityEngine.Random.Range(0, _invadersGameObject.Count);

        _invadersComponents[indexCurrentInvader].Shoot();
        OnShoot?.Invoke();
    }

    private void ResetInvadersGroup()
    {
        _currentProjectileForce += _levelUpForce;
        _currentSpeed += _levelUpSpeed;
        SpawnInvaders();
    }
}
