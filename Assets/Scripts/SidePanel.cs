using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SidePanel : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public Image Wallpaper;


    public void SetUpPanel(GameDetails _details) {
        Title.text = _details.GameName;
        Description.text = _details.GameDesciption;
        Wallpaper.sprite = _details.GameImage;
    }
}   
