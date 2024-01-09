using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsBtn;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button backBtn;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;

        soundEffectsBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicBtn.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        backBtn.onClick.AddListener(() =>
        {
            Hide();
            GamePauseUI.Instance.Show();
        });
       
    }

    private void Start()
    {
        UpdateVisual(); 
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "音效大小：" + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "声音大小：" + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject?.SetActive(false);
    }
}
