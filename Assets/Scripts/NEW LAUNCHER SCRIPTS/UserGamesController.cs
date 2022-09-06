using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class UserGamesController : MonoBehaviour
{
    public static UserGamesController Instance;

    private GameManager _gameManager => GameManager.Instance;

    public List<GameObject> DisplayedGames = new List<GameObject>();

    [Header("Purchased Game Prefab")]
    public PurchasedGameListItem PurchasedGamePrefab;
    public Transform parent;

    public string[] PurchasedGameData;

    public List<UserGameListItem> sidePanels = new List<UserGameListItem>();

    public GameObject UserGamesPanelPrefab;
    public Transform UserGamesPanelParent;
    public UserGameListItem s_Panel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    private void Start()
    {
        GetPurchasedGames();
    }

    public async void GetPurchasedGames()
    {
        WWWForm form = new WWWForm();
        form.AddField("UserID", _gameManager.AccountManager.userID);

        UnityWebRequest webRequest = UnityWebRequest.Post(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.GetPurchasedGameURL.ToString()), form);
        var operation = webRequest.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }
        print(webRequest.downloadHandler.text);
        PurchasedGameData = webRequest.downloadHandler.text.Split(';');
        ListGames(PurchasedGameData);
    }

    public void ListGames(string[] _PurchasedGameDatartData)
    {

        for (int i = 0; i < PurchasedGameData.Length - 1; i++)
        {
            _gameManager.purchasedGame.Add(new StoreGame(
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "ID:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameTitle:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameDesc:"),
                            float.Parse(HelperScript.GetValueData(_PurchasedGameDatartData[i], "GamePrice:")),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameIcon:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "IconPath:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameWallpaper:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "WallpaperPath:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "GameURL:"),
                            HelperScript.GetValueData(_PurchasedGameDatartData[i], "Filename:")
                            ));

        }
        GetImages();

    }

    private async void GetImages()
    {

        //ICONS
        for (int i = 0; i < _gameManager.purchasedGame.Count; i++)
        {
            string iconURL = _gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.MainURL.ToString()) + _gameManager.purchasedGame[i].IconPath + "/" + _gameManager.purchasedGame[i].GameIcon;

            WWW www = new WWW(iconURL);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //icon.texture = www.texture;
            _gameManager.purchasedGame[i].iconTex = www.texture;

        }
        //WALLPAPERS
        for (int i = 0; i < _gameManager.purchasedGame.Count; i++)
        {
            string iconURL = _gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.MainURL.ToString()) + _gameManager.purchasedGame[i].WallpaperPath + "/" + _gameManager.purchasedGame[i].GameWallpaper;

            WWW www = new WWW(iconURL);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //wallpaper.texture = www.texture;
            _gameManager.purchasedGame[i].wallpaperTex = www.texture;
        }
        DisplayGames();
    }
    private void DisplayGames()
    {

        //if (_setup)
        //{
            for (int i = 0; i < _gameManager.purchasedGame.Count; i++)
            {
                PurchasedGameListItem prefab = Instantiate(PurchasedGamePrefab, parent.position, Quaternion.identity, parent);


            if (i == 0)
            {
                prefab.IsSelected = true;

            }
            SpawnUserGamesPanel(_gameManager.purchasedGame[i], prefab);
            prefab.SetupStoreGameDetails(_gameManager.purchasedGame[i]);

                DisplayedGames.Add(prefab.gameObject);
            }
        //}
        //else
        //{
        //    int ctr = _gameManager.purchasedGame.Count - 1;
        //    GameDetail prefab = Instantiate(PurchasedGamePrefab, parent.position, Quaternion.identity, parent.transform);
        //    prefab.IsSelected = true;

        //    //SpawnSidePanel(_gameManager.purchasedGame[ctr], prefab);
        //    prefab.SetupDetails(_gameManager.purchasedGame[ctr]);

        //    DisplayedGames.Add(prefab.gameObject);
        //    //await Task.Delay(500);
        //}

        //if (_gameManager.purchasedGame.Count > 0) _uiHandler.EmptyGameObject.SetActive(false);
        //else _uiHandler.EmptyGameObject.SetActive(true);

    }


    public void SpawnUserGamesPanel(StoreGame _details, PurchasedGameListItem gameDetail)
    {
        GameObject side_Panel = Instantiate(UserGamesPanelPrefab, UserGamesPanelParent.position, Quaternion.identity, UserGamesPanelParent);
        s_Panel = side_Panel.GetComponent<UserGameListItem>();
        gameDetail.UserGameItemPanel = s_Panel;
        s_Panel.SetupStoreGameDetails(_details);
        sidePanels.Add(s_Panel);
        //isSidePanelExists = true;
    }

    public void DisableSidPanels()
    {
        for (int i = 0; i < sidePanels.Count; i++)
        {
            sidePanels[i].gameObject.SetActive(false);
        }
    }
}
