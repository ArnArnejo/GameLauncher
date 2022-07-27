using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{

    private GameManager _gameManager => GameManager.Instance;
    //[Header("Top Panel")]

    public static UIHandler Instance;

    [Header("Main Menu Panel")]
    public Button MenuStoreBtn;
    public Button MenuLibraryBtn;
    public Button MenuCommunityBtn;
    public Button MenuAccountBtn;
    public Button AddGameBtn;
    public TextMeshProUGUI AccountNameText;
    public TextMeshProUGUI AccountBalanceText;

    [Header("Main Panel")]
    public GameObject StorePanel;
    public GameObject LibraryPanel;
    public GameObject CommunityPanel;
    public GameObject AccountPanel;

    [Header("Store Panels")]
    public GameObject MainPanel;
    public GameObject AddGamePanel;


    [Header("Store SPecifics")]
    public GameObject CartSection;
    public GameObject RecomendedSection;
    public GameObject SpecialOfferSection;

    [Header("CartButton")]
    public Button CartBtn;
    public Button YourStoreBtn;

    [Header("Notification")]
    public NotificationText Notification;

  

    //[Header("Footer Panel")]


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        EnableLibrary();
        MenuStoreBtn.onClick.AddListener(EnableStore);
        MenuLibraryBtn.onClick.AddListener(EnableLibrary);
        MenuCommunityBtn.onClick.AddListener(EnableCommunity);
        MenuAccountBtn.onClick.AddListener(EnableAccount);
        AddGameBtn.onClick.AddListener(AddGame);
        CartBtn.onClick.AddListener(EnableCart);
        YourStoreBtn.onClick.AddListener(EnableStore);
        AccountNameText.text = _gameManager.AccountManager.username;
        AccountBalanceText.text = "$ " + _gameManager.AccountManager.walletBallance.ToString();
    }


    public void DisablePanels() {
        StorePanel.SetActive(false);
        LibraryPanel.SetActive(false);
        CommunityPanel.SetActive(false);
        AccountPanel.SetActive(false);

        //Store Panels
        MainPanel.SetActive(false);
        AddGamePanel.SetActive(false);

        //Store Specifics
        CartSection.SetActive(false);
        RecomendedSection.SetActive(false);
        SpecialOfferSection.SetActive(false);
    }

    public void DisableStoreSpecifics() {
        
    }
    public void EnableStore() {
        DisablePanels();
        StorePanel.SetActive(true);
        MainPanel.SetActive(true);

        RecomendedSection.SetActive(true);
        SpecialOfferSection.SetActive(true);

    }

    public void EnableLibrary()
    {
        DisablePanels();
        LibraryPanel.SetActive(true);
    }
    public void EnableCommunity()
    {
        DisablePanels();
        CommunityPanel.SetActive(true);
    }
    public void EnableAccount()
    {
        DisablePanels();
        AccountPanel.SetActive(true);
    }

    public void AddGame() {
        DisablePanels();
        StorePanel.SetActive(true);
        AddGamePanel.SetActive(true);
    }


    public void EnableCart() {
        DisablePanels();
        MainPanel.SetActive(true);
        StorePanel.SetActive(true);
        CartSection.SetActive(true);
    }

}
