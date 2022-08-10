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
using System;
using System.ComponentModel;
using UnityEngine.Networking;
using Humanizer.Bytes;

public class PlayGameHandler : MonoBehaviour//, ISynchronizeInvoke
{
    //public static PlayGameHandler Instance;

    //public bool InvokeRequired => throw new NotImplementedException();

    //public string Name;
    //public string InternalName;
    //public string URL;

    //private WebClient webCLient;
    //public string zipLoc;
    //public string fileLoc;
    //public string exeLoc;

    //private TextMeshProUGUI text;
    //private Button btn;
    //private SidePanel s_panel;

    //string fileName;

    //private void Start()
    //{
    //    UnityEngine.Debug.Log(Application.persistentDataPath);
        
    //}

    //private void Awake()
    //{
    //    if (Instance == null) Instance = this;
    //}

    //public void Init(string _name , string _internalName, string _url) {

    //    Name = _name;
    //    InternalName = _internalName;
    //    URL = _url;

    //    webCLient = new WebClient();
    //    string N = Name.Replace(' ', '_');
    //    fileLoc = "Downloads\\Games\\" + N; //Application.persistentDataPath + "\\" + N; //"Assets\\Games\\" + N;
    //    zipLoc = fileLoc + ".zip";
    //    exeLoc = fileLoc + "\\" + InternalName + ".exe";
    //    fileName = InternalName + ".exe";


    //    webCLient.OpenRead(URL);
    //    Int64 bytes_total = Convert.ToInt64(webCLient.ResponseHeaders["Content-Length"]);
    //    var maxFileSize = ByteSize.FromKilobytes(bytes_total);

    //    print(maxFileSize.Gigabytes.ToString("0.00") + " " + bytes_total.ToString());

    //    //var maxFileSize = ByteSize.FromKilobytes(10);
    //    //maxFileSize.Bytes;
    //    //maxFileSize.MegaBytes;
    //    //maxFileSize.GigaBytes;

    //    //print(maxFileSize.Bytes + " " + maxFileSize.Megabytes + " " + maxFileSize.Gigabytes);
    //}

    //public async void InstallGame() {


    //    webCLient.DownloadProgressChanged += (s, e) =>
    //    {
    //        //progressBar.Value = e.ProgressPercentage;
    //        print(e.ProgressPercentage);
    //        s_panel.progreesBar.value = e.ProgressPercentage / 100f;
    //    };

    //    Task dl = webCLient.DownloadFileTaskAsync(URL, zipLoc);
       
    //    while (!dl.IsCompleted)
    //    {
            
    //        text.text = "Downloading";
    //        await Task.Yield();
    //    }

        
    //    //webCLient.DownloadFileCompleted += (s, e) =>
    //    //{
    //    //    //progressBar.Visible = false;
    //    //    // any other code to process the file
    //    //};

    //    UnzipGame();
        
    //}

    //IEnumerator downloadFile() {
    //    UnityWebRequest webRequest = UnityWebRequest.Head(URL);
    //    webRequest.Send();
    //    while (!webRequest.isDone)
    //    {
    //        yield return null;
    //    }
    //    print("File size: " + webRequest.GetResponseHeader("Content-Length"));
    //}


    //private bool isInstalled() {
    //    return File.Exists(exeLoc);
    //}
    //public void PlayGame() {
    //    //Process.Start(exeLoc);

    //    Process p = new Process();
    //    p.StartInfo.FileName = fileName;
    //    p.StartInfo.WorkingDirectory = fileLoc;
    //    p.SynchronizingObject = this;
    //    p.EnableRaisingEvents = true;
    //    p.Start();
    //}
    //private void UnzipGame() {

    //    //await Task.Run(() => ZipFile.ExtractToDirectory(zipLoc, fileLoc));

    //    // zipfilePath = Application.temporaryCachePath + "/args.zip"
    //    //string exportLocation = Application.temporaryCachePath + "/dir"

    //    //ZipUtil.Unzip(zipLoc, fileLoc);
    //    ZipFile.ExtractToDirectory(zipLoc, fileLoc);
        
    //    Clean();
    //    Refresh();
    //}
    //private void Clean() {
    //    File.Delete(zipLoc);
    //}
    //private void Refresh() {
    //    btn.onClick.RemoveAllListeners();

    //    print("Refresh: " + isInstalled());
    //    if (isInstalled())
    //    {
    //        text.text = "Play Game";
    //        btn.onClick.AddListener(PlayGame);
    //    }
    //    else {
    //        text.text = "Download";
    //        btn.onClick.AddListener(InstallGame);
    //    }
    //}

    //public void CheckGameState(Button _playButton, TextMeshProUGUI _text, SidePanel _sidePanel) {
    //    text = _text;
    //    btn = _playButton;
    //    s_panel = _sidePanel;
    //    btn.onClick.RemoveAllListeners();

    //    print("CheckGameState: " + isInstalled());
    //    if (isInstalled())
    //    {
    //        text.text = "Play Game";
    //        btn.onClick.AddListener(PlayGame);
    //    }
    //    else
    //    {
    //        text.text = "Download";
    //        btn.onClick.AddListener(InstallGame);
    //    }

        
    //}

    //public IAsyncResult BeginInvoke(Delegate method, object[] args)
    //{
    //    throw new NotImplementedException();
    //}

    //public object EndInvoke(IAsyncResult result)
    //{
    //    throw new NotImplementedException();
    //}

    //public object Invoke(Delegate method, object[] args)
    //{
    //    throw new NotImplementedException();
    //}

}
