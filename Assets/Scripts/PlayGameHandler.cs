using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine.UI;
using TMPro;

public class PlayGameHandler : MonoBehaviour
{
    public static PlayGameHandler Instance;

    public string Name;
    public string InternalName;
    public string URL;

    private WebClient webCLient;
    public string zipLoc;
    public string fileLoc;
    public string exeLoc;

    private TextMeshProUGUI text;
    private Button btn;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void Init(string _name , string _internalName, string _url) {

        Name = _name;
        InternalName = _internalName;
        URL = _url;

        webCLient = new WebClient();
        string N = Name.Replace(' ', '_');
        fileLoc = "Assets\\Games\\" + N;
        zipLoc = fileLoc + ".zip";
        exeLoc = fileLoc + "\\" + InternalName + ".exe";
    }

    public async void InstallGame() {


        Task dl = webCLient.DownloadFileTaskAsync(URL, zipLoc);

        while (!dl.IsCompleted)
        {
            text.text = "Downloading";
            await Task.Yield();
        }
        
        UnzipGame();
        
    }

    private bool isInstalled() {
        return File.Exists(exeLoc);
    }
    public void PlayGame() {
        Process.Start(exeLoc);
    }
    private void UnzipGame() {
        ZipFile.ExtractToDirectory(zipLoc, fileLoc);
        Clean();
        Refresh();
    }
    private void Clean() {
        File.Delete(zipLoc);
    }
    private void Refresh() {
        btn.onClick.RemoveAllListeners();
        if (isInstalled())
        {
            text.text = "Play Game";
            btn.onClick.AddListener(PlayGame);
        }
        else {
            text.text = "Download";
            btn.onClick.AddListener(InstallGame);
        }
    }

    public void CheckGameState(Button _playButton, TextMeshProUGUI _text) {
        text = _text;
        btn = _playButton;
        if (isInstalled())
        {
            _text.text = "Play Game";
            _playButton.onClick.AddListener(PlayGame);
        }
        else {
            _text.text = "Download";
            _playButton.onClick.AddListener(InstallGame);
        }


    }

}
