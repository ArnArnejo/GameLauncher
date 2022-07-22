using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public static class HelperScript
{
    public static string GetValueData(string data, string index)
    {

        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }

        return value;
    }

    public static string ImageToBase64(Texture2D tex)
    {

        Texture2D mytexture = tex;
        byte[] bytes;
        bytes = mytexture.EncodeToPNG();
        //using (MemoryStream ms = new MemoryStream())
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    bf.Serialize(ms, mytexture);
        //    bytes = ms.ToArray();
        //}
        string enc = Convert.ToBase64String(bytes);
        return enc;
    }

    public static byte[] ImageToByte(Texture2D tex)
    {

        Texture2D mytexture = tex;
        byte[] bytes;
        bytes = mytexture.EncodeToPNG();
        //using (MemoryStream ms = new MemoryStream())
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    bf.Serialize(ms, mytexture);
        //    bytes = ms.ToArray();
        //}
        
        return bytes;
    }
}
