using UnityEngine;
using Zenject;

public class Ball : MonoBehaviour, IDamageable, IPausable
{
    private ParticleManager _death;
    private BallSettings _settings;
    private Camera _mainCamera;
    private GameManager _gameManager;
    private bool _isPause;
    private PauseManager _pauseManager;

    [Inject]
    private void Construct(GameManager gameManager, PauseManager pauseManager)
    {
        _gameManager = gameManager;
        _pauseManager = pauseManager;
        _pauseManager.Register(this);
    }

    private void Start()
    {
        _settings = new BallSettings();
        _settings.IncreaseDifficult(Time.timeSinceLevelLoad / 50.0f);
        _death = new ParticleManager();
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!_isPause)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _settings.Speed, Space.World);
            if (IsOutsideEdge(transform.position))
            {
                _gameManager.ApplyDamage(_settings.Damage);
                Death();
            }
        }
    }

    private void Death()
    {
        Instantiate(_death.prefab, transform.position, _death.prefab.transform.rotation);
        _pauseManager.UnRegister(this);
        Destroy(gameObject, _settings.BallDestroyDelay);
    }
    private void DeathWithReward()
    {
        _gameManager.AddPoint(_settings.RewardPoint);
        Death();
    }
    private bool IsOutsideEdge(Vector3 position)
    {
        Vector3 viewport = _mainCamera.WorldToViewportPoint(position);
        return (viewport.y < 0f || viewport.x > 1f || viewport.x < 0f);
    }

    public void ApplyDamage(int damage)
    {
        _settings.Health -= damage;
        if (_settings.Health <= 0f)
            DeathWithReward();
    }

    public void Pause() => _isPause = true;
    public void Resume() => _isPause = false;

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
    }
}
