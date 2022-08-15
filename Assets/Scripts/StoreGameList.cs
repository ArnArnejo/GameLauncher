using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Threading.Tasks;
using TMPro;


public class StoreGameList : MonoBehaviour
{
    private GameManager _gameManager => GameManager.Instance;

    //[Header("URL's")]
    //public string AddGameToCartURL;

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
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        GameManager.OnUpdateStoreDetails += AlreadyPurchased;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateStoreDetails -= AlreadyPurchased;
    }

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

        AlreadyPurchased();
    }

    private void AlreadyPurchased() {
        
        text.text = "Add to Cart";

        for (int i = 0; i < _gameManager.purchasedGame.Count; i++)
        {
            if (ID == _gameManager.purchasedGame[i].ID) {
                text.text = "Already Purchased";
                AddToCartBtn.onClick.RemoveAllListeners();
                return;
            }
        }
        Debug.LogError("Check Purchase " + text.text);
    }

    private async void addToCart() {



        WWWForm form = new WWWForm();
        form.AddField("GameID", ID);
        form.AddField("UserID", _gameManager.AccountManager.userID);

        UnityWebRequest webRequest = UnityWebRequest.Post(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.AddToCartURL.ToString()), form);

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
