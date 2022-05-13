using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button menuButton;
    [SerializeField] Button replayButton;
    [SerializeField] Button closeButton;
    [SerializeField] Text scoreText;
    [SerializeField] Text recordText;
    [SerializeField] Image health;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Text pauseText;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject adMenu;
    [SerializeField] Button adButton;
    private GameManager _gameManager;
    private float _currentHealth;
    private float _newHealthValue;
    private bool _smoothHealthBar = false;
    private float _progress;
    private ReplayScene _scene;
    private AdService _adService;

    [Inject]
    private void Construct(AdService adService, GameManager gameManager, PauseManager pauseManager)
    {
        _gameManager = gameManager;
        _adService = adService;
        menuButton.onClick.AddListener(() => pauseManager.SetPause(true));
        closeButton.onClick.AddListener(() => pauseManager.SetPause(false));
        menuButton.onClick.AddListener(() => gameManager.LoadLastRecord());


        _scene = new ReplayScene(replayButton);
        _gameManager.OnPointChanged += OnPointChanged;
        _gameManager.OnRecordChanged += OnRecordChanged;
        _gameManager.OnHealthChanged += OnHealthChanged;
        _gameManager.OnLoseGame += LoseGame;
        _adService.InitServices();
        adButton.onClick.AddListener(_adService.ShowRewardedAd);
        adButton.onClick.AddListener(() => adMenu.SetActive(false));
    }
    private void Update()
    {
        if (_smoothHealthBar)
        {
            _progress += 0.01f;
            health.fillAmount = Mathf.Lerp(_currentHealth, _newHealthValue, _progress);
        }
        if (health.fillAmount == _newHealthValue)
        {
            _progress = 0;
            _smoothHealthBar = false;
        }
    }

    private void OnHealthChanged(int value)
    {
        _currentHealth = health.fillAmount;
        _newHealthValue = value / 100.0f;
        _smoothHealthBar = true;
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
        adMenu.SetActive(true);
        _adService.ShowInterstitialAd();
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
