using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CartHandler : MonoBehaviour
{
    private GameManager _gameManager => GameManager.Instance;
    private UIHandler _uiHandler => UIHandler.Instance;

    //[Header("Get Cart Items URL")]
    //public string GetCartItemURL;
    //public string RemoveCartItemURL;
    //public string MainURL;

    [Header("Cart Item Prefab")]
    public GameObject CartItem;
    public Transform parent;

    [Header("Utilities")]
    public string[] cartData;

    private void OnEnable()
    {
        GameManager.OnAddCartItem += UpdateCart;
        GameManager.OnRemoveCartItem += RemoveCartItem;
    }
    private void OnDisable()
    {
        GameManager.OnAddCartItem -= UpdateCart;
        GameManager.OnRemoveCartItem -= RemoveCartItem;
    }

    private void Start()
    {
        //GetCartItem();
        GetCartItem(true);
    }

    //public async void GetCartItem() {

    //    WWWForm form = new WWWForm();
    //    form.AddField("UserID", _gameManager.AccountManager.userID);

    //    UnityWebRequest webRequest = UnityWebRequest.Post(GetCartItemURL, form);
    //    webRequest.SetRequestHeader("Content-Type", "application/json");
    //    var operation = webRequest.SendWebRequest();
    //    while (!operation.isDone)
    //    {
    //        await Task.Yield();
    //    }

    //    if (webRequest.result == UnityWebRequest.Result.Success)
    //    {
    //        print($"Success: {webRequest.downloadHandler.text}");
    //        //string gameDataString = webRequest.downloadHandler.text;
    //        //cartData = gameDataString.Split(';');
    //        //ListCart(cartData);
    //    }
    //    else
    //    {
    //        print($"Failed: {webRequest.error}");
    //    }
    //}
    public void GetCartItem(bool _setup) {
        StartCoroutine(Cart(_setup));
    }

    public void UpdateCart(string _id) {
        _gameManager.cartGames.Clear();
        StartCoroutine(Cart(false));
    }

    public void RemoveCartItem(string _id) {
        StartCoroutine(RemoveItem(_id));
    }

    IEnumerator RemoveItem(string _id) {

        WWWForm form = new WWWForm();
        form.AddField("GameID", _id);

        UnityWebRequest webRequest = UnityWebRequest.Post(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.RemoveCartItemURL.ToString()), form);
        yield return webRequest.SendWebRequest();
        print(webRequest.downloadHandler.text);
        _gameManager.cartGames.RemoveAll(r => r.ID == _id);
        _gameManager.cartItemList.RemoveAll(item => item == null);
    }

    IEnumerator Cart(bool _setup)
    {
        if (!_setup)
        {
            _uiHandler.Notification.gameObject.SetActive(true);
            _uiHandler.Notification.NotifText.text = "Added to Cart";
            yield return new WaitForSeconds(1f);
            _uiHandler.Notification.gameObject.SetActive(false);
            _uiHandler.Notification.NotifText.text = "";
        }

        WWWForm form = new WWWForm();
        form.AddField("UserID", _gameManager.AccountManager.userID);

        UnityWebRequest webRequest = UnityWebRequest.Post(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.GetCartItemURL.ToString()), form);
        yield return webRequest.SendWebRequest();
        print(webRequest.downloadHandler.text);
        cartData = webRequest.downloadHandler.text.Split(';');
        ListCart(cartData, _setup);
    }

    public void ListCart(string[] _cartData, bool _setup)
    {


        for (int i = 0; i < _cartData.Length - 1; i++)
        {
            _gameManager.cartGames.Add(new CartGames(
                            HelperScript.GetValueData(_cartData[i], "GameTitle:"),
                            HelperScript.GetValueData(_cartData[i], "ID:"),
                            float.Parse(HelperScript.GetValueData(_cartData[i], "GamePrice:")),
                            HelperScript.GetValueData(_cartData[i], "GameIcon:"),
                            HelperScript.GetValueData(_cartData[i], "IconPath:")
                            ));

        }
        GetImageIcon(_setup);
        
    }

    private async void GetImageIcon(bool _setup)
    {
        for (int i = 0; i < _gameManager.cartGames.Count; i++)
        {
            string iconURL = _gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.MainURL.ToString()) + _gameManager.cartGames[i].IconPath + "/" + _gameManager.cartGames[i].GameIcon;

            WWW www = new WWW(iconURL);
            var operation = www;
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //icon.texture = www.texture;
            _gameManager.cartGames[i].iconTex = www.texture;

        }
        DisplayCart(_setup);
    }

    private async void DisplayCart(bool _setup)
    {
        if (_setup)
        {
            for (int i = 0; i < _gameManager.cartGames.Count; i++)
            {
                GameObject prefab = Instantiate(CartItem, parent.position, Quaternion.identity, parent.transform);
                CartItem item = prefab.GetComponent<CartItem>();
                _gameManager.cartItemList.Add(item);
                item.SetupList(_gameManager.cartGames[i]);
                await Task.Delay(500);
            }
        }
        else {
                GameObject prefab = Instantiate(CartItem, parent.position, Quaternion.identity, parent.transform);
                CartItem item = prefab.GetComponent<CartItem>();
                _gameManager.cartItemList.Add(item);
                item.SetupList(_gameManager.cartGames[_gameManager.cartGames.Count - 1]);
                await Task.Delay(500);
        }
    }

}

[System.Serializable]
public class CartGames {
    public string GameTitle;
    public string ID;
    public float GamePrice;
    public string GameIcon;
    public string IconPath;
    public Texture2D iconTex;

    public CartGames(string _gameTitle, string _id, float _gamePrice, string _gameIcon, string _iconPath) {
        GameTitle = _gameTitle;
        ID = _id;
        GamePrice = _gamePrice;
        GameIcon = _gameIcon;
        IconPath = _iconPath;
    }

}
