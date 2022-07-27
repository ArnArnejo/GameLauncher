using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PurchaseHandler : MonoBehaviour
{

    public static PurchaseHandler Instance;
    private GameManager _gameManager => GameManager.Instance;
    private UIHandler _uiHandler => UIHandler.Instance;

    //[Header("URLS")]
    //public string GetGamesURL;
    //public string PurchaseGameURL;
    //public string MainURL;

    [Header("Utilities")]
    public string[] PurchasedGameData;

    [Header("Purchased Game Prefab")]
    public GameDetail PurchasedGamePrefab;
    public Transform parent;

    [Header("Side Panel")]
    public GameObject SidePanelPrefab;
    public Transform sidePanelParent;
    private SidePanel s_panel;
    public bool isSidePanelExists;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnPurchase += PurchaseGame;
        //GameManager.OnAddPurchase += AddPurchasedGame;
    }
    private void OnDisable()
    {
        GameManager.OnPurchase -= PurchaseGame;
        //GameManager.OnAddPurchase -= AddPurchasedGame;
    }

    private void Start()
    {
        GetPurchasedGames(true);
    }

    public void GetPurchasedGames(bool _setup) {
        StartCoroutine(PurchasedGames(_setup));
    }
    IEnumerator PurchasedGames(bool _setup)
    {
        WWWForm form = new WWWForm();
        form.AddField("UserID", _gameManager.AccountManager.userID);

        UnityWebRequest webRequest = UnityWebRequest.Post(_gameManager.GetURL(eURLS.GetPurchasedGameURL.ToString()), form);
        yield return webRequest.SendWebRequest();
        print(webRequest.downloadHandler.text);
        PurchasedGameData = webRequest.downloadHandler.text.Split(';');
        ListGames(PurchasedGameData, _setup);
    }

    public void ListGames(string[] _PurchasedGameDatartData, bool _setup)
    {


        for (int i = 0; i < PurchasedGameData.Length - 1; i++)
        {
            _gameManager.purchasedGame.Add(new PurchasedGame(
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "ID:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameTitle:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameDesc:"),
                            float.Parse(HelperScript.GetValueData(_PurchasedGameDatartData[i], "GamePrice:")),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameIcon:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "IconPath:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameWallpaper:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "WallpaperPath:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameURL:")
                            ));

        }
        GetImageIcon();
        GetImageWallPaper(_setup);
    }

    private async void GetImageIcon()
    {
        for (int i = 0; i < _gameManager.purchasedGame.Count; i++)
        {
            string iconURL = _gameManager.GetURL(eURLS.MainURL.ToString()) + _gameManager.purchasedGame[i].IconPath + "/" + _gameManager.purchasedGame[i].GameIcon;

            WWW www = new WWW(iconURL);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //icon.texture = www.texture;
            _gameManager.purchasedGame[i].iconTex = www.texture;

        }

    }
    private async void GetImageWallPaper(bool _setup)
    {
        for (int i = 0; i < _gameManager.purchasedGame.Count; i++)
        {
            string iconURL = _gameManager.GetURL(eURLS.MainURL.ToString()) + _gameManager.purchasedGame[i].WallpaperPath + "/" + _gameManager.purchasedGame[i].GameWallpaper;

            WWW www = new WWW(iconURL);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //wallpaper.texture = www.texture;
            _gameManager.purchasedGame[i].wallpaperTex = www.texture;
        }
        DisplayGames(_setup);
    }

    private void DisplayGames(bool _setup)
    {

        if (_setup)
        {
            for (int i = 0; i < _gameManager.purchasedGame.Count; i++)
            {
                GameDetail prefab = Instantiate(PurchasedGamePrefab, parent.position, Quaternion.identity, parent);
                prefab.SetupDetails(_gameManager.purchasedGame[i]);

                if (i == 0)
                {
                    prefab.IsSelected = true;

                }
            }
        }
        else {
            int ctr = _gameManager.purchasedGame.Count - 1;
            GameDetail prefab = Instantiate(PurchasedGamePrefab, parent.position, Quaternion.identity, parent.transform);
            prefab.SetupDetails(_gameManager.purchasedGame[ctr]);
            prefab.IsSelected = true;
            //await Task.Delay(500);
        }
    }

    public void SpawnSidePanel(PurchasedGame _details)
    {
        GameObject sidePanel = Instantiate(SidePanelPrefab, sidePanelParent.position, Quaternion.identity, sidePanelParent);
        s_panel = sidePanel.GetComponent<SidePanel>();
        s_panel.SetUpPanel(_details);
        isSidePanelExists = true;
    }

    public void UpdateSidePanel(PurchasedGame _details)
    {
        s_panel.SetUpPanel(_details);
    }


    #region Purchase Game
    public void PurchaseGame(string _id)
    {
        StartCoroutine(GamePurchase(_id));
    }

    IEnumerator GamePurchase(string _id)
    {
        _uiHandler.Notification.gameObject.SetActive(true);
        _uiHandler.Notification.NotifText.text = "Game Purchased";
        yield return new WaitForSeconds(1f);
        _uiHandler.Notification.gameObject.SetActive(false);
        _uiHandler.Notification.NotifText.text = "";
       
        WWWForm form = new WWWForm();
        form.AddField("GameID", _id);
        form.AddField("UserID", _gameManager.AccountManager.userID);

        UnityWebRequest webRequest = UnityWebRequest.Post(_gameManager.GetURL(eURLS.PurchaseGameURL.ToString()), form);
        yield return webRequest.SendWebRequest();
        print(webRequest.downloadHandler.text);
        AddPurchasedGame(_id);
    }

    public void AddPurchasedGame(string _id) {
        _gameManager.purchasedGame.Clear();
        GetPurchasedGames(false);
    }
    #endregion
}

[System.Serializable]
public class PurchasedGame
{
    public string GameTitle;
    public string ID;
    public string GameDesc;
    public float GamePrice;
    public string GameIcon;
    public string IconPath;
    public string GameWallpaper;
    public string WallpaperPath;
    public string GameURL;
    public Texture2D iconTex;
    public Texture2D wallpaperTex;

    public PurchasedGame(string _id, string _gametitle, string _gamedesc, float _gameprice, string _gameicon, string _iconpath, string _gamewallpaper, string _wallpaperpath, string _gameURL)
    {
        ID = _id;
        GameTitle = _gametitle;
        GameDesc = _gamedesc;
        GamePrice = _gameprice;
        GameIcon = _gameicon;
        IconPath = _iconpath;
        GameWallpaper = _gamewallpaper;
        WallpaperPath = _wallpaperpath;
        GameURL = _gameURL;
    }
}
