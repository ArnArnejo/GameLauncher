using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    

    public static GameManager Instance;
    public AccountManager AccountManager;

    [Header("URL's")]
    public URL[] Links;

    //Events
    public delegate void UpdateCart(string _id);
    public static event UpdateCart OnAddCartItem;
    public static event UpdateCart OnRemoveCartItem;

    public delegate void Purchase(string _id);
    public static event Purchase OnPurchase;

    //General Lists
    [Header("Utilities")]
    public List<CartGames> cartGames = new List<CartGames>();

    [Header("Cart Item List")]
    public List<CartItem> cartItemList = new List<CartItem>();

    [Header("PurchasedGames")]
    public List<PurchasedGame> purchasedGame = new List<PurchasedGame>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null) Instance = this;
    }
    public void ItemAddedToCart(string _id) {
        OnAddCartItem?.Invoke(_id);
    }
    public void ItemRemovedFromCart(string _id) {
        OnRemoveCartItem?.Invoke(_id);
    }
    public void PurchaseGame(string _id) {
        OnPurchase?.Invoke(_id);
    }
    public CartItem Item(string _id) {
        for (int i = 0; i < cartItemList.Count; i++)
        {
            if (cartItemList[i].ID == _id) {
                return cartItemList[i];
            }
        }
        return null;
    }


    public string GetURL(string name) {
        URL url = Array.Find(Links, link => link.Name == name);

        return url.UrlLink;
    }
}

[Serializable]
public class URL {
    public string Name;
    public string UrlLink;
}

public enum eURLS {
    SignupURL,
    LoginURL,
    MainURL,
    ViewGameURL,
    InsertGameURL,
    AddToCartURL,
    GetCartItemURL,
    RemoveCartItemURL,
    GetPurchasedGameURL,
    PurchaseGameURL
}
