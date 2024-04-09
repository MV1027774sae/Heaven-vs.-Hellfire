using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Text _sliderText;

    private bool isFullscreen = true;

    // Start is called before the first frame update
    void Start()
    {
        //audioSlider.onValueChanged.AddListener((v) => { _sliderText.text = v.ToString("100"); });
        if (!PlayerPrefs.HasKey("VolumeValue"))
        {
            PlayerPrefs.SetFloat("VolumeValue", 1);
            LoadValue();
        }
        else
        {
            LoadValue();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = audioSlider.value;
        SaveVolume();
    }

    void SaveVolume()
    {
        PlayerPrefs.SetFloat("VolumeValue", audioSlider.value);
        LoadValue();
    }

    void LoadValue()
    {
        audioSlider.value = PlayerPrefs.GetFloat("VolumeValue");
    }

    public void SetFullScreen()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
}
