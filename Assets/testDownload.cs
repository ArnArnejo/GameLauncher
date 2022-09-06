using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System;

public class testDownload : MonoBehaviour
{

    WebClient webCLient = new WebClient();
    // Start is called before the first frame update
    void Start()
    {
        install();
    }

    public async void install() {

        //webCLient.DownloadProgressChanged += (s, e) =>
        //{
        //    //progressBar.Value = e.ProgressPercentage;
        //    print(e.ProgressPercentage);

        //    print(e.ProgressPercentage.ToString("0.00") + " %");

        //    print(e.ProgressPercentage);
        //    //Percentage.text = e.ProgressPercentage.ToString("0.00") + " %";
        //    //progreesBar.value = e.ProgressPercentage / 100f;
        //};

        string url = "http://downloads.amanomamu.com/full_client/Amanoma%20MU%20Client%20Installer.exe"; //"https://www.dropbox.com/s/h7r38w9xxysc01n/Flyff.zip?dl=1";//
        //string fileLoc = "Downloads\\Games\\"; //Application.persistentDataPath + "\\" + N; //"Assets\\Games\\" + N;
        //string zipLoc = fileLoc + ".exe";

        //Task dl = webCLient.DownloadFileTaskAsync(url, zipLoc);

        //while (!dl.IsCompleted)
        //{

        //    //PlayBtnText.text = "Downloading";
        //    //progreesBar.gameObject.SetActive(true);
        //    //Percentage.gameObject.SetActive(true);
        //    await Task.Yield();
        //}

        webCLient.OpenRead(url);
        //string file = webCLient.ResponseHeaders["Content-Type"].ToString();
        string fileName;
        string header = webCLient.ResponseHeaders["Content-Disposition"] ?? string.Empty;
        const string filename = "filename=";
        int index = header.LastIndexOf(filename, StringComparison.OrdinalIgnoreCase);
        if (index > -1)
        {
            fileName = header.Substring(index + filename.Length);
            print(fileName + "___FILE");
        }
        print("Done");

    }

    
}
