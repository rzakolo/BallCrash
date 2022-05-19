using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnManager : IPausable, ITickable
{
    private Ball _ballPrefab;
    private float boundY = 13, boundX;
    private DiContainer _container;
    private bool _isPause;
    private float timer;
    private float spawnRate;

    public SpawnManager(DiContainer diContainer, Ball ballPrefab, PauseManager pauseManager)
    {
        _container = diContainer;
        _ballPrefab = ballPrefab;
        pauseManager.Register(this);
        Start();
    }
    private void Start()
    {
        Camera camera = Camera.main;
        boundX = camera.ViewportToWorldPoint(new Vector3(1, 0, -camera.transform.position.z)).x - _ballPrefab.gameObject.transform.localScale.x / 2;
        spawnRate = 10;
    }

    private void Spawn()
    {
        Vector3 position = new Vector3(Random.Range(-boundX, boundX), boundY);
        _container.InstantiatePrefabForComponent<Ball>(_ballPrefab, position, Quaternion.identity, null);
    }

    public void Tick()
    {
        if (!_isPause)
        {
            timer += Time.fixedDeltaTime;
            if (timer > spawnRate)
            {
                Spawn();
                timer = 0;
            }
        }
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
    }
}
