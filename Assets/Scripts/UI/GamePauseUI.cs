using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button optionsBtn;

    private void Awake()
    {
        Instance = this;

        resumeBtn.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.PauseGame();
        });
        mainMenuBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsBtn.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
            Hide();
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGamePasued += GameManager_OnGamePasued;
        KitchenGameManager.Instance.OnGameUnPasued += GameManager_OnGameUnPasued;
        Hide();
    }

    private void GameManager_OnGameUnPasued(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePasued(object sender, System.EventArgs e)
    {
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }   
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
