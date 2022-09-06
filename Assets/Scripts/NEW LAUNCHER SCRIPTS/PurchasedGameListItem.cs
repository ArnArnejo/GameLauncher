using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasedGameListItem : Game
{

    private UIController _uiController => UIController.Instance;
    private UserGamesController _userGamesCOntroller => UserGamesController.Instance;

    public UserGameListItem UserGameItemPanel;
    public RawImage Icon;
    public Button Btn;

    public bool IsSelected;

    private void Start()
    {
        Btn.onClick.AddListener(CheckSIdePanel);
    }

    public override void SetupStoreGameDetails(StoreGame storeGame)
    {
        base.SetupStoreGameDetails(storeGame);
        Icon.texture = gameDetails.iconTex;

        if (IsSelected)
        {
            UserGameItemPanel.gameObject.SetActive(true);
        }
        else UserGameItemPanel.gameObject.SetActive(false);


    }

    public void CheckSIdePanel()
    {
        _uiController.DisablePanels();
        _uiController.UserGamesPanel.SetActive(true);
        _userGamesCOntroller.DisableSidPanels();
        UserGameItemPanel.gameObject.SetActive(true);
        //InitGameDetails();
    }

    public void InitGameDetails() {
        UserGameItemPanel.SetupStoreGameDetails(gameDetails);
    }



}
