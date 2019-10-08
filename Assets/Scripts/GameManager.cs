using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip bgAudio;
    [SerializeField] AudioClip runAudio;
    [SerializeField] GameObject Wolf;
    public string PhotoFilePath { get; set; }
    public static GameManager instance;
    AudioSource mAudioSourse;
    IntefaceLogic intLogic;
    NativeShare myNativeShare;
    void Start()
    {
        mAudioSourse = GetComponent<AudioSource>();
        SetAudio(bgAudio);
        intLogic = FindObjectOfType<IntefaceLogic>();
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAudio(AudioClip clip)
    {
        if (mAudioSourse.clip != clip)
        {
            mAudioSourse.clip = clip;
            if(mAudioSourse.isActiveAndEnabled)
                mAudioSourse.Play();
        }
    }

    public void WolfMouseClic(Animator anim, bool run)
    {
        Debug.Log(run);
        anim.SetBool("Move", run);
        Debug.LogError(anim.GetBool("Move"));
        SetAudio(run?runAudio:bgAudio);
                 
    }

    public void TrackingLostAction()
    {
        intLogic.ShowFindingImage();
        intLogic.SetActivePhotoButton(false);
        SetAudio(bgAudio);
        WolfMouseClic(Wolf.GetComponent<Animator>(), false);


    }

    public void TrackingFoundAction()
    {
        intLogic.HideFindingImage();
        intLogic.SetActivePhotoButton(true);

        Wolf.transform.LookAt(new Vector3(0,0,0));

        Vector3 quaternion = Wolf.transform.localRotation.eulerAngles;
        quaternion.x = 0;
        quaternion.z = 0;
        Wolf.transform.localRotation = Quaternion.Euler(quaternion);
    }

    public void SetShareCanvas(Texture2D shareTexture)
    {
        intLogic.ShowShareView(shareTexture);
    }

    public void Share()
    {
        NativeGallery.SaveImageToGallery(PhotoFilePath,"TestAR", "Screenshot.png");
        myNativeShare = new NativeShare();
        myNativeShare.AddFile(PhotoFilePath);
        myNativeShare.Share();
    }
}
