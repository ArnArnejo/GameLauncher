using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SidePanel : MonoBehaviour
{
    private PlayGameHandler _playGameHandler => PlayGameHandler.Instance;

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public RawImage Wallpaper;
    public string GameURL;
    public Button PlayBtn;
    public TextMeshProUGUI PlayBtnText;

    public void SetUpPanel(PurchasedGame _details) {
        Title.text = _details.GameTitle;
        Description.text = _details.GameDesc;
        Wallpaper.texture = _details.wallpaperTex;
        GameURL = _details.GameURL;

        _playGameHandler.CheckGameState(PlayBtn, PlayBtnText);
    }
}   
