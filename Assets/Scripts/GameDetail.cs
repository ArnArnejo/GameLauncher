using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDetail : MonoBehaviour
{
    private PurchaseHandler _purchaseHandler => PurchaseHandler.Instance;

    //private PlayGameHandler _playGameHandler => PlayGameHandler.Instance;

    private StoreGame details;
    public TextMeshProUGUI GameTitle;
    public RawImage icon;
    public bool IsSelected;
    public SidePanel sidePanel;
    public Button btn;

    public void SetupDetails(StoreGame _detail) {
        details = _detail;


        btn.onClick.AddListener(CheckSIdePanel);
        GameTitle.text = details.GameTitle;
        icon.texture = details.iconTex;
        

        if (IsSelected)
        {
            initGameHandler();
            sidePanel.gameObject.SetActive(true);
        }
        else
        {
            sidePanel.gameObject.SetActive(false);
        }
    }

    public void CheckSIdePanel() {
        _purchaseHandler.DisableSidPanels();
        sidePanel.gameObject.SetActive(true);
        initGameHandler();
    }

    private void initGameHandler() {
        //_playGameHandler.Init(details.GameTitle, details.GameTitle, details.GameURL);
        sidePanel.Init(details.GameTitle, details.GameTitle, details.GameURL, details.Filename);
    }

    
}
