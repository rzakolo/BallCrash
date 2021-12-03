using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPausable
{
    private Camera mainCamera;
    private bool onPause;
    [SerializeField] GameManager gameManager;
    private void Start()
    {
        gameManager.OnPause += Pause;
        gameManager.OnResume += Resume;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !onPause)
        {
            MouseClick();
        }
    }
    void MouseClick()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable ball))
            {
                ball.ApplyDamage(1);
            }
        }
    }
    public void Pause() => onPause = true;
    public void Resume() => onPause = false;
}
