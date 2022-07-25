using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor;
using SFB;
using TMPro;
using System.Threading.Tasks;

public class StoreHandler : MonoBehaviour
{

    private GameManager _gameManager => GameManager.Instance;
    [Header("Store Game List Prefab")]
    public GameObject GamelistPrefab;

    [Header("Parent Object of the List")]
    public Transform Parent;

    [Header("URLS")]
    public string ViewGamesURL;
    public string MainURL;
    public string InsertGameURL;

    [Header("Game Information")]
    public string GameTitle;
    public string IconName;
    public string GameDesc;
    public string WallpaperName;
    public string price;

    [Header("Game Fields")]
    public TMP_InputField GameTitleField;
    public TMP_InputField GameDescField;
    public TMP_InputField GamePriceField;
    public Button UploadImageBtn;
    public Button UploadwallpaperBtn;
    public TextMeshProUGUI iconText;
    public TextMeshProUGUI wallpaperText;
    public Texture2D texIcon;
    public Texture2D textWallpaper;
    public Button SubmitBtn;


    [Header("Utilities")]
    public string[] gameData;
    public List<StoreGame> storeGames = new List<StoreGame>();
    public ExtensionFilter[] filter;

    private void Start()
    {
        GetGames();

        UploadImageBtn.onClick.AddListener(UploadImageIcon);
        UploadwallpaperBtn.onClick.AddListener(UploadImageWallpaper);
        SubmitBtn.onClick.AddListener(SubmitGame);
    }

    #region Get Game Data
    //[ContextMenu("Get Games")]
    private async void GetGames()
    {
        using var www = UnityWebRequest.Get(ViewGamesURL);
        www.SetRequestHeader("Content-Type", "application/json");
        var operation = www.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            print($"Success: {www.downloadHandler.text}");
            string gameDataString = www.downloadHandler.text;
            gameData = gameDataString.Split(';');
            ListGames(gameData);
        }
        else
        {
            print($"Failed: {www.error}");
        }
    }
    public void ListGames(string[] _gameData)
    {


        for (int i = 0; i < _gameData.Length - 1; i++)
        {
            storeGames.Add(new StoreGame(
                            HelperScript.GetValueData(_gameData[i], "ID:"),
                            HelperScript.GetValueData(_gameData[i], "GameTitle:"),
                            HelperScript.GetValueData(_gameData[i], "GameDesc:"),
                            float.Parse(HelperScript.GetValueData(_gameData[i], "GamePrice:")),
                            HelperScript.GetValueData(_gameData[i], "GameIcon:"),
                            HelperScript.GetValueData(_gameData[i], "IconPath:"),
                            HelperScript.GetValueData(_gameData[i], "GameWallpaper:"),
                            HelperScript.GetValueData(_gameData[i], "WallpaperPath:")
                            ));

        }
        GetImageIcon();
        GetImageWallPaper();
        
    }
    //[ContextMenu("Get Icon")]
    private async void GetImageIcon()
    {
        for (int i = 0; i < storeGames.Count; i++)
        {
            string iconURL = MainURL + storeGames[i].IconPath + "/" + storeGames[i].GameIcon;

            WWW www = new WWW(iconURL);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //icon.texture = www.texture;
            storeGames[i].iconTex = www.texture;

        }

    }
    //[ContextMenu("Get Wallpaper")]
    private async void GetImageWallPaper()
    {
        for (int i = 0; i < storeGames.Count; i++)
        {
            string iconURL = MainURL + storeGames[i].WallpaperPath + "/" + storeGames[i].GameWallpaper;

            WWW www = new WWW(iconURL);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //wallpaper.texture = www.texture;
            storeGames[i].wallpaperTex = www.texture;
        }
        DisplayGames();
    }
    private async void DisplayGames() {

        for (int i = 0; i < storeGames.Count; i++)
        {
            GameObject prefab = Instantiate(GamelistPrefab, Parent.transform.position, Quaternion.identity, Parent.transform);
            StoreGameList storeList = prefab.GetComponent<StoreGameList>();
            storeList.SetupList(storeGames[i]);
            await Task.Delay(500);
        }
    }
    #endregion

    #region Submit Game
    private async void SubmitGame()
    {

        //StartCoroutine(UploadGame());
        //Debug.LogError(HelperScript.ImageToBase64(texIcon));
        //Debug.LogError(HelperScript.ImageToBase64(textWallpaper));

        GameTitle = GameTitleField.text;
        GameDesc = GameDescField.text;
        price = GamePriceField.text;

        WWWForm form = new WWWForm();
        form.AddField("addTitle", GameTitle);
        form.AddField("addDesc", GameDesc);
        form.AddField("addPrice", price);
        form.AddField("addIcon", IconName);
        form.AddField("addWallpaper", WallpaperName);
        form.AddBinaryData("IconFile", HelperScript.ImageToByte(texIcon), IconName, "Pictures/Icons");
        form.AddBinaryData("WallpaperFile", HelperScript.ImageToByte(textWallpaper), WallpaperName, "Pictures/Wallpaper");
        form.AddField("iconPath", "Pictures/Icons");
        form.AddField("wallpaperPath", "Pictures/Wallpaper");



        UnityWebRequest webRequest = UnityWebRequest.Post(InsertGameURL, form);
        //yield return webRequest.SendWebRequest();
        //print(webRequest.downloadHandler.text);
        //string[] result = webRequest.downloadHandler.text.Split(';');

        //using var www = UnityWebRequest.Get(ViewGamesURL);
        //www.SetRequestHeader("Content-Type", "application/json");
        var operation = webRequest.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print($"Success: {webRequest.downloadHandler.text}");
        }
        else
        {
            print($"Failed: {webRequest.error}");
        }

    }
    #endregion

    #region Icon Upload
    private async void UploadImageIcon()
    {

        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", filter, false);

        string[] name = paths[0].Split('\\');

        //Debug.LogError(name[name.Length-1].ToString());
        IconName = name[name.Length - 1].ToString();
        iconText.text = IconName;

        if (paths.Length > 0)
        {
            //StartCoroutine(OutputRoutineIcon(new System.Uri(paths[0]).AbsoluteUri));
            WWW www = new WWW(new System.Uri(paths[0]).AbsoluteUri);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            texIcon = www.texture;
        }


    }
    #endregion

    #region Wallpaper Upload
    private async void UploadImageWallpaper()
    {

        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", filter, false);

        string[] name = paths[0].Split('\\');

        //Debug.LogError(name[name.Length - 1].ToString());
        WallpaperName = name[name.Length - 1].ToString();
        wallpaperText.text = WallpaperName;
        if (paths.Length > 0)
        {
            //StartCoroutine(OutputRoutineWallpaper(new System.Uri(paths[0]).AbsoluteUri));
            if (paths.Length > 0)
            {
                //StartCoroutine(OutputRoutineIcon(new System.Uri(paths[0]).AbsoluteUri));
                WWW www = new WWW(new System.Uri(paths[0]).AbsoluteUri);
                var operation = www;
                while (!operation.isDone)
                {
                    await Task.Yield();
                }
                textWallpaper = www.texture;
            }
        }
    }
    #endregion

}
