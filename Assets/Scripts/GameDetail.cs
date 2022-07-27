using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDetail : MonoBehaviour
{
    private PurchaseHandler _purchaseHandler => PurchaseHandler.Instance;

    private PlayGameHandler _playGameHandler => PlayGameHandler.Instance;

    private PurchasedGame details;
    public TextMeshProUGUI GameTitle;
    public RawImage icon;
    public bool IsSelected;

    public void SetupDetails(PurchasedGame _detail) {
        details = _detail;
    }

    private void Start()
    {
        GameTitle.text = details.GameTitle;
        icon.texture = details.iconTex;

        if (IsSelected) {
            CheckSidePanel();
        }
    }

    public void CheckSidePanel() {

        _playGameHandler.Init(details.GameTitle, details.GameTitle, details.GameURL);

        if (!_purchaseHandler.isSidePanelExists)
        {
            _purchaseHandler.SpawnSidePanel(details);
        }
        else {
            _purchaseHandler.UpdateSidePanel(details);
        }

        
    }
}
