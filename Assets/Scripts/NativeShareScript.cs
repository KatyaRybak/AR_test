using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class NativeShareScript : MonoBehaviour
{
    //public GameObject canvasShareObject;
    private bool isProcessing = false;
    private bool isFocus= false;

    public void ShareButtonPress()
    {
        string message = "";
        if (!isProcessing)
        {
            StartCoroutine(ShareScreenShot("com.DefaultCompany.VuforiaTest2", message));
        }
    }

    IEnumerator ShareScreenShot(string AUTHORITY, string shareText)
    {
        if (!Application.isEditor)
        {
            string destination = Path.Combine(Application.persistentDataPath, "photo.png");
            yield return new WaitForEndOfFrame();

                AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
                AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
                intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
                AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
                AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
                    uriObject);
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
                    "Can you beat my score?");
                intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
                    intentObject, "Share your new score");
                currentActivity.Call("startActivity", chooser);

                yield return new WaitForSecondsRealtime(1);
           
        }

        //yield return new WaitUntil(() => isFocus);
        //canvasShareObject.SetActive(false);
        isProcessing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

}

   
