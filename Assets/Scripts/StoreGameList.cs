using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Threading.Tasks;


public class StoreGameList : MonoBehaviour
{
    private GameManager _gameManager => GameManager.Instance;

    [Header("URL's")]
    public string AddGameToCartURL;

    [Header("Game Data")]
    public string ID;
    public TextMeshProUGUI GameTitle;
    public TextMeshProUGUI GameDesc;
    public TextMeshProUGUI GamePrice;
    public RawImage Icon;
    public RawImage Wallpaper;

    string currency = "$";

    [Header("Add to cart Button")]
    public Button AddToCartBtn;

    private void Start()
    {
        AddToCartBtn.onClick.AddListener(addToCart);
    }

    public void SetupList(StoreGame storeGame) {
        ID = storeGame.ID;
        GameTitle.text = storeGame.GameTitle;
        GameDesc.text = storeGame.GameDesc;
        GamePrice.text = currency +" "+ storeGame.GamePrice.ToString();
        Icon.texture = storeGame.iconTex;
        Wallpaper.texture = storeGame.wallpaperTex;
    }

    private async void addToCart() {

        WWWForm form = new WWWForm();
        form.AddField("GameID", ID);
        form.AddField("UserID", _gameManager.AccountManager.userID);

        UnityWebRequest webRequest = UnityWebRequest.Post(AddGameToCartURL, form);

        var operation = webRequest.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print($"Success: {webRequest.downloadHandler.text}");
            _gameManager.ItemAddedToCart(null);
        }
        else
        {
            print($"Failed: {webRequest.error}");
        }

    }
}
