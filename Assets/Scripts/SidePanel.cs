using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SidePanel : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public RawImage Wallpaper;


    public void SetUpPanel(PurchasedGame _details) {
        Title.text = _details.GameTitle;
        Description.text = _details.GameDesc;
        Wallpaper.texture = _details.wallpaperTex;
    }
}   
