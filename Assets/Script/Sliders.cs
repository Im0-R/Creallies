using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        if (gameObject.name == "MasterVolume" && PlayerPrefs.HasKey("MasterVolume"))
        {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");
        }
        else if (gameObject.name == "FXVolume" && PlayerPrefs.HasKey("FXVolume"))
        {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("FXVolume");
        }
        else if (gameObject.name == "MusicVolume" && PlayerPrefs.HasKey("MusicVolume"))
        {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeVolumeOnValueChanged(int _id)
    {
        SoundManager.SetSoundVolume(_id, gameObject.GetComponent<Slider>().value);
    }
}
