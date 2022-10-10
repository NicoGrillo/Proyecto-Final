using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text selectedText;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider fearBar;
    [SerializeField] private Slider flBar;
    [SerializeField] private TMP_Text rocksText;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject textPanel;

    private float count;
    private string temporalText;

    private static HUDManager instance;
    public static HUDManager Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PlayerEvents.OnWin += WinUI;
            PlayerEvents.OnLose += LoseUI;
            PlayerEvents.OnTutorial += TutorialSettings;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        selectedText.text = "";
    }

    private void Update()
    {
        ResetText();
    }

    public static void EnableItem(int childIndex, int selectColor)
    {
        if (selectColor == 0) instance
            .itemPanel
                .transform
                    .GetChild(childIndex).GetComponent<Image>().color = Color.white;

        if (selectColor == 1) instance
            .itemPanel
                .transform
                    .GetChild(childIndex).GetComponent<Image>().color = Color.green;

        if (selectColor == 2) instance
            .itemPanel
                .transform
                    .GetChild(childIndex).GetComponent<Image>().color = Color.red;
    }


    private void ResetText()
    {
        if (selectedText.text == temporalText)
        {
            count += Time.deltaTime;
            if (count >= 5)
            {
                selectedText.text = "";
                count = 0;
            }
        }
        else count = 0;

        temporalText = selectedText.text;
    }

    public void SetSelectedText(string newText)
    {
        selectedText.text = newText;
    }

    public static void SetHPBar(int newValue)
    {
        instance.hpBar.value = newValue;
    }

    public static void SetFearBar(float newValue)
    {
        instance.fearBar.value = newValue;
    }

    public static void SetFLBar(int newValue)
    {
        instance.flBar.value = newValue;
    }

    public void ThrowRocksText(string newText)
    {
        rocksText.text = newText;
    }

    public void enableTextPanel(string note)
    {
        PlayerEvents.OnCantMoveCall(true);
        textPanel.SetActive(true);
        if (note == "StartNote") noteText.text = "There is no time for explanations, you have to run away and hide from those things before dawn.";
    }

    public void onCancelTextPanelButton()
    {
        textPanel.SetActive(false);
        PlayerEvents.OnCantMoveCall(false);
    }

    private void WinUI()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        winPanel.SetActive(true);
        PlayerEvents.OnCantMoveCall(true);
    }

    private void LoseUI()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        losePanel.SetActive(true);
        PlayerEvents.OnCantMoveCall(true);
    }

    private void TutorialSettings()
    {
        textPanel.SetActive(true);
        PlayerEvents.OnCantMoveCall(true);
        noteText.text = TextHUDManager.TutorialMoveText;

        TutorialEvents.OnFLPick += FLPick;
        TutorialEvents.OnRocksFirstPick += RocksPick;
    }

    private void FLPick()
    {
        textPanel.SetActive(true);
        PlayerEvents.OnCantMoveCall(true);
        noteText.text = TextHUDManager.TutorialFLText;
    }

    private void RocksPick()
    {
        textPanel.SetActive(true);
        PlayerEvents.OnCantMoveCall(true);
        noteText.text = TextHUDManager.TutorialRocksText;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWin -= WinUI;
        PlayerEvents.OnLose -= LoseUI;
        PlayerEvents.OnTutorial -= TutorialSettings;
        TutorialEvents.OnFLPick -= FLPick;
        TutorialEvents.OnRocksFirstPick -= RocksPick;
    }
}
