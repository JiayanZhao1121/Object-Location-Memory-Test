using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class WebDownloadHelper : MonoBehaviour
{

   

    [DllImport("__Internal")]
    private static extern void uploadString(string categoryName, string fileName, string link);


    // Upload String
    public void UploadStringLocal(string category, string fileName, string content)
    {

        StartCoroutine(InitiateUploadString(category, fileName, content));
    }


    IEnumerator InitiateUploadString(string textCategory, string textFileName, string textContent)
    {
        yield return new WaitForEndOfFrame();
        string aData = textContent;
        string encodedText = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(aData));
        string txturl = "data:application/octet-stream;base64," + encodedText;


#if !UNITY_EDITOR
         uploadString(textCategory, textFileName, txturl);
#endif
#if UNITY_EDITOR
        Debug.Log(txturl);

#endif

    }


}
