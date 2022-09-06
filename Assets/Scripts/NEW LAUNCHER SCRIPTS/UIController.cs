using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("UI PANELS")]
    public List<GameObject> PanelsList = new List<GameObject>();
    public GameObject StorePanel;
    public GameObject CartPanel;
    public GameObject UserGamesPanel;


    [Header("Button")]
    public Button CartBtn;
    public Button StoreBtn;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    private void Start()
    {
        StoreBtn.onClick.AddListener(EnableStore);
        CartBtn.onClick.AddListener(EnableCart);
    }

    public void DisablePanels() {
        
        for (int i = 0; i < PanelsList.Count; i++)
        {
            PanelsList[i].SetActive(false);
        }
    }

    public void EnableStore() {
        DisablePanels();
        StorePanel.SetActive(true);
    }

    public void EnableCart()
    {
        DisablePanels();
        CartPanel.SetActive(true);

    }
}
