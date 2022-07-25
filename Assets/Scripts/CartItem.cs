using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartItem : MonoBehaviour
{
    private GameManager _gameManager => GameManager.Instance;
    [Header("Game Details")]
    public string ID;
    public TextMeshProUGUI GameTitle;
    public TextMeshProUGUI Price;
    public RawImage Icon;
    public Button RemoveBtn;
    public Button BuyButton;

    public string currency;


    private void Start()
    {
        RemoveBtn.onClick.AddListener(RemoveItem);
        BuyButton.onClick.AddListener(BuyGame);
    }

    public void SetupList(CartGames _cartGames)
    {
        ID = _cartGames.ID;
        GameTitle.text = _cartGames.GameTitle;
        Price.text = currency + " " + _cartGames.GamePrice.ToString();
        Icon.texture = _cartGames.iconTex;
    }

    public void RemoveItem() {
        _gameManager.ItemRemovedFromCart(ID);
        gameObject.SetActive(false);
    }
    public void BuyGame() {
        _gameManager.PurchaseGame(ID);
        _gameManager.ItemRemovedFromCart(ID);
        gameObject.SetActive(false);
    }

}
