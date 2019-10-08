using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IntefaceLogic : MonoBehaviour
{
    const string SOUND_KEY = "Sound";

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject sharePanel;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject photoButton;
    [SerializeField] GameObject findingImage;
    [SerializeField] GameObject screenCanvas;
    [SerializeField] GameObject infoPanel;

    [SerializeField] Button menuButton;
    [SerializeField] Button soundButton;
    [SerializeField] Sprite menuSprite;
    [SerializeField] Sprite closeSprite;

    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite loudeSprite;


    bool menuShowing = false;
    AudioSource sound;

    public void OpenMenuPanel()
    {
        if(!menuShowing)
        {
            menuButton.GetComponent<Image>().sprite = closeSprite;
            menuShowing = true;
            menuPanel.SetActive(true);
        }
        else
        {
            menuButton.GetComponent<Image>().sprite = menuSprite;
            menuShowing = false;
            menuPanel.SetActive(false);
        }
    }

    public void ChangedSound()
    {
        sound.enabled = !sound.enabled;
        PlayerPrefs.SetInt(SOUND_KEY, sound.enabled ? 1 : 0);

        SetSoundSprite();

    }

    private void SetSoundSprite()
    {
        if (sound.enabled)
        {
            soundButton.GetComponent<Image>().sprite = loudeSprite;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = muteSprite;
        }
    }

    void StartSetting()
    {
        if (PlayerPrefs.HasKey(SOUND_KEY))
        {
            sound.enabled = PlayerPrefs.GetInt(SOUND_KEY) == 1;
        }
        else
        {
            sound.enabled = true;
            PlayerPrefs.SetInt(SOUND_KEY, 1);
        }
        SetSoundSprite();

    }

    private void Start()
    {
        sound = GameManager.instance.GetComponent<AudioSource>();
        StartSetting();
    }

    public void ShowShareView(Texture2D screenTexture)
    {
        mainPanel.SetActive(false);
        screenCanvas.SetActive(true);
        screenCanvas.GetComponentInChildren<RawImage>().texture = screenTexture;
    }

    public void HideShareView()
    {
        mainPanel.SetActive(true);
        screenCanvas.SetActive(false);
    }

    public void SetActivePhotoButton(bool active)
    {
        photoButton.SetActive(active);
    }

    public void HideFindingImage()
    {
        findingImage.SetActive(false);
    }

    public void ShowFindingImage()
    {
        findingImage.SetActive(true);
    }

    public void OpenHowToPlay(bool open)
    {
        infoPanel.SetActive(open);
        //Application.OpenURL("file:///" + Application.streamingAssetsPath + "/readme.html");
    }
    
}
