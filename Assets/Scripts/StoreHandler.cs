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
    [Header("Store Game List Prefab")]
    public GameObject GamelistPrefab;

    [Header("Parent Object of the List")]
    public Transform Parent;

    [Header("URLS")]
    public string ViewGamesURL;
    public string MainURL;


    [Header("Utilities")]
    public string[] gameData;
    public List<StoreGame> storeGames = new List<StoreGame>();


    private void Start()
    {
        GetGames();
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
    #endregion

    private async void DisplayGames() {

        for (int i = 0; i < storeGames.Count; i++)
        {
            GameObject prefab = Instantiate(GamelistPrefab, Parent.transform.position, Quaternion.identity, Parent.transform);
            await Task.Delay(500);
        }
    }
}
