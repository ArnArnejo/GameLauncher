using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDetail : MonoBehaviour
{
    private GameHandler _gameHandler => GameHandler.Instance;
    private GameDetails details;
    public TextMeshProUGUI GameTitle;
    public Image icon;
    public bool IsSelected;

    public void SetupDetails(GameDetails _detail) {
        details = _detail;
    }

    private void Start()
    {
        GameTitle.text = details.GameName;
        icon.sprite = details.GameIcon;

        if (IsSelected) {
            CheckSidePanel();
        }
    }

    public void CheckSidePanel() {
        if (!_gameHandler.isSidePanelExists)
        {
            _gameHandler.SpawnSidePanel(details);
        }
        else {
            _gameHandler.UpdateSidePanel(details);
        }
    }
}
