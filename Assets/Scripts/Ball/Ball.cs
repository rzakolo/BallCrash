using UnityEngine;

public class Ball : MonoBehaviour, IDamageable, IPausable
{
    private ParticleManager death;
    private BallSettings settings;
    private Camera mainCamera;
    private GameManager gameManager;
    private bool onPause;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnPause += Pause;
        gameManager.OnResume += Resume;
        settings = GetComponent<BallSettings>();
        settings.IncreaseDifficult(Time.timeSinceLevelLoad / 50.0f);
        death = new ParticleManager();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!onPause)
        {
            transform.Translate(Vector3.down * Time.deltaTime * settings.Speed, Space.World);
            if (IsOutsideEdge(transform.position))
            {
                gameManager.ApplyDamage(settings.Damage);
                Death();
            }
        }
    }

    private void Death()
    {
        gameManager.OnPause -= Pause;
        Instantiate(death.prefab, transform.position, death.prefab.transform.rotation);
        Destroy(gameObject, settings.BallDestroyDelay);
    }
    private void DeathWithReward()
    {
        gameManager.AddPoint(settings.RewardPoint);
        Death();
    }
    private bool IsOutsideEdge(Vector3 position)
    {
        Vector3 viewport = mainCamera.WorldToViewportPoint(position);
        return (viewport.y < 0f || viewport.x > 1f || viewport.x < 0f);
    }

    public void ApplyDamage(int damage)
    {
        settings.Health -= damage;
        if (settings.Health <= 0f)
            DeathWithReward();
    }

    public void Pause() => onPause = true;
    public void Resume() => onPause = false;
}
