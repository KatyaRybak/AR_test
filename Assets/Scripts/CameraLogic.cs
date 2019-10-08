using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CameraLogic : MonoBehaviour
{
    bool takeScreenshot;
    byte[] bytes;


    public void CreatePhoto()
    {
        takeScreenshot=true;
    }


    private void Start()
    {
        takeScreenshot = false;
    }

    private void OnPostRender()
    {
        if (takeScreenshot)
        {
            Debug.Log(Application.persistentDataPath);
            CreateScreen();
            takeScreenshot = false;
        }

    }

    void CreateScreen()
    {
        Texture2D photo = new Texture2D(Screen.width, Screen.height);
        photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        GameManager.instance.SetShareCanvas(photo);
        photo.Apply();

        //Encode to a PNG
        bytes = photo.EncodeToPNG();

        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "photo.png"), bytes);
        GameManager.instance.PhotoFilePath = Path.Combine(Application.persistentDataPath, "photo.png");
    }

}



