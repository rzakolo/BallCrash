using UnityEngine;

public class Ball : MonoBehaviour, IDamageable, IPausable
{
    //private ParticleManager death;
    private BallSettings settings;
    private Camera mainCamera;
    private GameManager gameManager;
    private bool onPause;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnPause += Pause;
        gameManager.OnResume += Resume;
        var renderer = gameObject.GetComponent<Renderer>();
        settings = new BallSettings(renderer);
        //death = new ParticleManager();
        mainCamera = Camera.main;
        //death.StartColor = settings.Color;
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
        //Instantiate(death.prefab, transform.position, Quaternion.identity).Emit(1);
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
