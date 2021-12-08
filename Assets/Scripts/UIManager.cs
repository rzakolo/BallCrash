using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text recordText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Image health;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Text pauseText;
    [SerializeField] private GameObject loseMenu;
    private float currentHealth;
    private float newHealthValue;
    private bool smoothHealthBar = false;
    private float progress;
    private ReplayScene scene;

    private void Awake()
    {
        scene = new ReplayScene(replayButton);
        gameManager.OnPointChanged += OnPointChanged;
        gameManager.OnRecordChanged += OnRecordChanged;
        gameManager.OnHealthChanged += OnHealthChanged;
        gameManager.OnLoseGame += LoseGame;
    }

    private void Update()
    {
        if (smoothHealthBar)
        {
            progress += 0.01f;
            health.fillAmount = Mathf.Lerp(currentHealth, newHealthValue, progress);
        }
        if (health.fillAmount == newHealthValue)
        {
            progress = 0;
            smoothHealthBar = false;
        }
    }

    private void OnHealthChanged(int value)
    {
        currentHealth = health.fillAmount;
        newHealthValue = value / 100.0f;
        smoothHealthBar = true;
    }
    private void LoseGame()
    {
        pauseMenu.SetActive(true);
        loseMenu.SetActive(true);
        menuButton.gameObject.SetActive(false);
        health.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
    }

    private void OnRecordChanged(int newRecord)
    {
        recordText.text = "Record: " + newRecord.ToString();
    }

    private void OnPointChanged(int score)
    {
        scoreText.text = score.ToString();
    }

}
