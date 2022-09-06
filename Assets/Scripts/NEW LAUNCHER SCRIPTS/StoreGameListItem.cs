using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreGameListItem : Game
{
    [Header("Game Data")]
    public TextMeshProUGUI GameTitle;
    public TextMeshProUGUI GamePrice;
    public RawImage Wallpaper;
    string currency = "$";

    public override void SetupStoreGameDetails(StoreGame storeGame)
    {
        base.SetupStoreGameDetails(storeGame);
        GameTitle.text = gameDetails.GameTitle;
        GamePrice.text = currency + " " + gameDetails.GamePrice.ToString();
        Wallpaper.texture = gameDetails.wallpaperTex;

    }
}
