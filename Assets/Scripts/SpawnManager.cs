using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, IPausable
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private GameManager gameManager;
    private float boundY = 13, boundX;

    private void Start()
    {
        Camera camera = Camera.main;
        boundX = camera.ViewportToWorldPoint(new Vector3(1, 0, -camera.transform.position.z)).x - ballPrefab.gameObject.transform.localScale.x / 2;
        gameManager.OnPause += Pause;
        gameManager.OnResume += Resume;
        Resume();
    }

    private void Spawn()
    {
        Vector3 position = new Vector3(Random.Range(-boundX, boundX), boundY);
        Instantiate(ballPrefab, position, Quaternion.identity);
    }

    public void Pause()
    {
        if (IsInvoking(nameof(Spawn)))
            CancelInvoke(nameof(Spawn));
    }

    public void Resume()
    {
        InvokeRepeating(nameof(Spawn), 1, 1);
    }
}
