using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Text selectedText;
    [SerializeField] private GameObject itemPanel;



    private static HUDManager instance;
    public static HUDManager Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetSelectedText(string newText)
    {
        selectedText.text = newText;
    }

    public static void EnableItem(int childIndex)
    {
        instance
            .itemPanel
                .transform
                    .GetChild(childIndex)
                        .GetChild(0).GetComponent<Image>().color = Color.white;
    }
}
