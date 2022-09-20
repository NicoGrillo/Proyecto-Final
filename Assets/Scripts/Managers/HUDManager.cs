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
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] public GameObject textPanel;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        selectedText.text = "";
        textPanel.SetActive(false);
    }

    private void Update()
    {
        ResetText();
    }

    public static void EnableItem(int childIndex, int selectColor)
    {
        //DisableAllItemsIcons();
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

    public static void DisableAllItemsIcons()
    {
        foreach (Transform panel in instance.itemPanel.transform)
        {
            panel.GetChild(0).GetComponent<Image>().color = Color.black;
        }
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

    public void enableTextPanel(bool value)
    {
        textPanel.SetActive(value);
        PlayerEvents.OnCantMoveCall(value);
        Cursor.lockState = CursorLockMode.Confined;
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
        Debug.Log(gameObject.name + " recibe al evento OnLose");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        losePanel.SetActive(true);
        PlayerEvents.OnCantMoveCall(true);
        Debug.Log(gameObject.name + " llamó al evento OnCantMove");
    }

    private void OnDisable()
    {
        PlayerEvents.OnWin -= WinUI;
        PlayerEvents.OnLose -= LoseUI;
    }
}
