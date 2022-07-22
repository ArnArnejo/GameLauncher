using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;

    public List<GameDetails> GameTitles = new List<GameDetails>();
    [SerializeField]
    private GameDetail gameElement;
    [SerializeField]
    private Transform gameListParent;
    [SerializeField]
    private Transform sidePanelParent;
    private SidePanel s_panel;
    public GameObject SidePanelPrefab;
    public bool isSidePanelExists;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void Start()
    {
        SetUpGameList();
    }


    private void SetUpGameList() {
        for (int i = 0; i < GameTitles.Count; i++)
        {
            GameDetail prefab = Instantiate(gameElement, gameListParent.position, Quaternion.identity, gameListParent);
            prefab.SetupDetails(GameTitles[i]);

            if (i == 0) {
                prefab.IsSelected = true;
                
            }
        }
        

    }

    public void SpawnSidePanel(GameDetails _details) {
        GameObject sidePanel = Instantiate(SidePanelPrefab, sidePanelParent.position, Quaternion.identity, sidePanelParent);
        s_panel = sidePanel.GetComponent<SidePanel>();
        s_panel.SetUpPanel(_details);
        isSidePanelExists = true;
    }

    public void UpdateSidePanel(GameDetails _details) {
        s_panel.SetUpPanel(_details);
    }
}

[System.Serializable]
public class GameDetails {
    public string GameName;
    public Sprite GameImage;
    public Sprite GameIcon;
    public string GameDesciption;
}
