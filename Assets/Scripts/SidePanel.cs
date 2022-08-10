using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;
using System.ComponentModel;
using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Humanizer.Bytes;

public class SidePanel : MonoBehaviour, ISynchronizeInvoke
{
   // private PlayGameHandler _playGameHandler => PlayGameHandler.Instance;

    public bool InvokeRequired => throw new NotImplementedException();

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public RawImage Wallpaper;
    public string GameURL;
    public Button PlayBtn;
    public TextMeshProUGUI PlayBtnText;
    public Slider progreesBar;
    public TextMeshProUGUI Percentage;

    public string Name;
    public string InternalName;
    public string URL;

    private WebClient webCLient;
    public string zipLoc;
    public string fileLoc;
    public string exeLoc;

    string fileName;

    public void Init(string _name, string _internalName, string _url)
    {

        Name = _name;
        InternalName = _internalName;
        URL = _url;

        webCLient = new WebClient();
        string N = Name.Replace(' ', '_');
        fileLoc = "Downloads\\Games\\" + N; //Application.persistentDataPath + "\\" + N; //"Assets\\Games\\" + N;
        zipLoc = fileLoc + ".zip";
        exeLoc = fileLoc + "\\" + InternalName + ".exe";
        fileName = InternalName + ".exe";


        webCLient.OpenRead(URL);
        Int64 bytes_total = Convert.ToInt64(webCLient.ResponseHeaders["Content-Length"]);
        var maxFileSize = ByteSize.FromKilobytes(bytes_total);

        print(maxFileSize.Gigabytes.ToString("0.00") + " " + bytes_total.ToString());

        //var maxFileSize = ByteSize.FromKilobytes(10);
        //maxFileSize.Bytes;
        //maxFileSize.MegaBytes;
        //maxFileSize.GigaBytes;

        //print(maxFileSize.Bytes + " " + maxFileSize.Megabytes + " " + maxFileSize.Gigabytes);

        CheckGameState();

        //CheckAvailability();
    }

    public void SetUpPanel(PurchasedGame _details) {
        Title.text = _details.GameTitle;
        Description.text = _details.GameDesc;
        Wallpaper.texture = _details.wallpaperTex;
        GameURL = _details.GameURL;

        //CheckAvailability();
        CheckGameState();
    }

    //public void Demo()
    //{
    //    //Process.Start("C:\\Users\\Arn\\Desktop\\The Punisher\\updatelauncher.exe");

    //    Process p = new Process();
    //    p.StartInfo.FileName = "punisher.exe";
    //    p.StartInfo.WorkingDirectory = "C:\\Users\\Arn\\Desktop\\The Punisher\\";
    //    p.SynchronizingObject = this;
    //    p.EnableRaisingEvents = true;
    //    p.Start();
    //}

    public async void InstallGame()
    {


        webCLient.DownloadProgressChanged += (s, e) =>
        {
            //progressBar.Value = e.ProgressPercentage;
            print(e.ProgressPercentage);
            Percentage.text = e.ProgressPercentage.ToString("0.00") + " %";
            progreesBar.value = e.ProgressPercentage / 100f;
        };

        Task dl = webCLient.DownloadFileTaskAsync(URL, zipLoc);

        while (!dl.IsCompleted)
        {

            PlayBtnText.text = "Downloading";
            progreesBar.gameObject.SetActive(true);
            Percentage.gameObject.SetActive(true);
            await Task.Yield();
        }


        webCLient.DownloadFileCompleted += (s, e) =>
        {
            progreesBar.gameObject.SetActive(false);
            Percentage.gameObject.SetActive(false);
            // any other code to process the file
        };

        UnzipGame();

    }

    private bool isInstalled()
    {
        return File.Exists(exeLoc);
    }
    public void PlayGame()
    {
        //Process.Start(exeLoc);

        Process p = new Process();
        p.StartInfo.FileName = fileName;
        p.StartInfo.WorkingDirectory = fileLoc;
        p.SynchronizingObject = this;
        p.EnableRaisingEvents = true;
        p.Start();
    }
    private void UnzipGame()
    {

        //await Task.Run(() => ZipFile.ExtractToDirectory(zipLoc, fileLoc));

        // zipfilePath = Application.temporaryCachePath + "/args.zip"
        //string exportLocation = Application.temporaryCachePath + "/dir"

        //ZipUtil.Unzip(zipLoc, fileLoc);
        ZipFile.ExtractToDirectory(zipLoc, fileLoc);

        Clean();
        Refresh();
    }
    private void Clean()
    {
        File.Delete(zipLoc);
    }
    private void Refresh()
    {
        PlayBtn.onClick.RemoveAllListeners();
        progreesBar.gameObject.SetActive(false);
        Percentage.gameObject.SetActive(false);
        print("Refresh: " + isInstalled());
        if (isInstalled())
        {
            PlayBtnText.text = "Play Game";
            PlayBtn.onClick.AddListener(PlayGame);
        }
        else
        {
            PlayBtnText.text = "Download";
            PlayBtn.onClick.AddListener(InstallGame);
        }
    }

    public void CheckGameState()
    {
        //text = _text;
        //btn = _playButton;
        //s_panel = _sidePanel;
        PlayBtn.onClick.RemoveAllListeners();
        progreesBar.gameObject.SetActive(false);
        Percentage.gameObject.SetActive(false);
        print("CheckGameState: " + isInstalled());
        if (isInstalled())
        {
            PlayBtnText.text = "Play Game";
            PlayBtn.onClick.AddListener(PlayGame);
        }
        else
        {
            PlayBtnText.text = "Download";
            PlayBtn.onClick.AddListener(InstallGame);
        }


    }


    public IAsyncResult BeginInvoke(Delegate method, object[] args)
    {
        throw new NotImplementedException();
    }

    public object EndInvoke(IAsyncResult result)
    {
        throw new NotImplementedException();
    }

    public object Invoke(Delegate method, object[] args)
    {
        throw new NotImplementedException();
    }
}   
