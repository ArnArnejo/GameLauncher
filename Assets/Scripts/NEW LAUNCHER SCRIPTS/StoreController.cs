using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class StoreController : MonoBehaviour
{
    private GameManager _gameManager => GameManager.Instance;

    [Header("Store Game List Prefab")]
    public GameObject GamelistPrefab;

    [Header("Parent Object of the List")]
    public Transform Parent;

    public string[] gameData;

    public List<StoreGame> storeGames = new List<StoreGame>();
    public List<GameObject> storeObject = new List<GameObject>();

    private void Start()
    {
        GetGames();
    }

    #region Get Game Data
    private async void GetGames()
    {
        using var www = UnityWebRequest.Get(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.ViewGameURL.ToString()));
        print(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.ViewGameURL.ToString()));
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
                            HelperScript.GetValueData(_gameData[i], "WallpaperPath:"),
                            HelperScript.GetValueData(_gameData[i], "GameURL:"),
                            HelperScript.GetValueData(_gameData[i], "Filename:")
                            ));


        }
        GetImages();
    }
   
    private async void GetImages()
    {
        //ICONS
        for (int i = 0; i < storeGames.Count; i++)
        {
            string iconURL = _gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.MainURL.ToString()) + storeGames[i].IconPath + "/" + storeGames[i].GameIcon;

            WWW www = new WWW(iconURL);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //icon.texture = www.texture;
            storeGames[i].iconTex = www.texture;

        }
        //WALLPAPERS
        for (int i = 0; i < storeGames.Count; i++)
        {
            string iconURL = _gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.MainURL.ToString()) + storeGames[i].WallpaperPath + "/" + storeGames[i].GameWallpaper;
            print(iconURL);
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
    
    private void DisplayGames()
    {
        for (int i = 0; i < storeGames.Count; i++)
        {
            GameObject prefab = Instantiate(GamelistPrefab, Parent.transform.position, Quaternion.identity, Parent.transform);
            StoreGameListItem listItem = prefab.GetComponent<StoreGameListItem>();
            listItem.SetupStoreGameDetails(storeGames[i]);
            storeObject.Add(prefab);
            //DisplayedGames.Add(prefab);
            //await Task.Delay(500);
        }
        
    }
    #endregion
}
