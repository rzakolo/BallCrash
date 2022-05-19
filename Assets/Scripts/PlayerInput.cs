using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : IPausable, ITickable
{
    private Camera _mainCamera;
    private bool _isPause;


    [Inject]
    private void Construct(PauseManager pauseManager)
    {
        pauseManager.Register(this);
        Start();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    void MouseClick()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable ball))
            {
                ball.ApplyDamage(1);
            }
        }
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0) && !_isPause)
        {
            MouseClick();
        }
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
    }
}
