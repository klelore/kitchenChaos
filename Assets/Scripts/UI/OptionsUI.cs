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
    [SerializeField] private Button moveUpBtn;
    [SerializeField] private Button moveDownBtn;
    [SerializeField] private Button moveLeftBtn;
    [SerializeField] private Button moveRightBtn;
    [SerializeField] private Button interactBtn;
    [SerializeField] private Button interactAltBtn;
    [SerializeField] private Button pauseBtn;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform presstoRebindKeyTranform;

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

        moveUpBtn.onClick.AddListener(() =>
        {
            GameInput.Instance.RebingBinding(GameInput.Binding.Move_Up);
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

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject?.SetActive(false);
    }


    public void ShowPressToRebindKey()
    {
        presstoRebindKeyTranform.gameObject.SetActive(true);
    }
    public void HidePressToRebindKey()
    {
        presstoRebindKeyTranform.gameObject.SetActive(false);
    }
}
