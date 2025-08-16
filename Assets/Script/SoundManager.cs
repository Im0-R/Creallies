using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using PokeDatas;
using UnityEngine.SceneManagement;
using TMPro.Examples;
using Unity.VisualScripting;
public enum SoundType
{
    BUTTON_PRESSED,
    ALLIE_HIT,
    ALLIE_ATTACK,
    CLICK_BAR,
    QTE_CIRCLE,
    SHINY_POP,
    CATCH,
    VICTORY,

    BATTLE_MUSIC,
    MAIN_MENU_MUSIC,
    TOTAL_SOUNDS_TYPE
}
public enum MusicType
{
    MAIN,
    BATTLE,
}
public class SoundManager : MonoBehaviour
{

    public enum SoundInfo
    {
        MASTER,
        SOUNDS,
        FX,
        TOTAL_SOUNDS_MIXER
    }
    private class SaveSound
    {
        public List<float> saveSounds = new List<float>();
    }

    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    private AudioSource[] audioSources;
    [SerializeField] public AudioSource FXSource;
    [SerializeField] private AudioMixerGroup[] audioMixers;

    public List<float> volumesPrefs;
    //[SerializeField]
    //AudioMixer masterMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(gameObject.GetComponent<AudioResource>());

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumesPrefs = new List<float>(audioMixers.Length);
        audioSources = new AudioSource[(int)SoundType.TOTAL_SOUNDS_TYPE];

        //for (int i = 1; i < (int)SoundType.TOTAL_SOUNDS_TYPE; i++)
        //{
        //    if (instance.soundList[i - 1].Sounds)
        //    {
        //        audioSources[i].clip = instance.soundList[i].Sounds;
        //    }
        //}
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            instance.audioMixers[0].audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            instance.audioMixers[1].audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            instance.audioMixers[2].audioMixer.SetFloat("FXVolume", PlayerPrefs.GetFloat("FXVolume"));
        }
        FXSource.volume = 0.2f;
        audioSource.volume = 0.2f;
    }
    private void Update()
    {
    }
    public static void PlaySound(SoundType sound, float volume = 0.2f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound].Sounds, volume);
        instance.audioSource.clip = null;
    }
    public static void PlayMusic(SoundType music, float volume = 0.2f)
    {
        instance.FXSource.clip = instance.soundList[(int)music].Sounds;
        instance.FXSource.Play();
    }
    public static void PauseAllSound()
    {
        instance.audioSource.Pause();
    }
    public static void UnPauseAllSound()
    {
        instance.audioSource.UnPause();
    }
    public static void PlaySoundSpecificTime(float timeWanted)
    {
        instance.audioSource.time = timeWanted;
        instance.audioSource.Play();
    }
    public static void UnPauseSoundSpecificTime(float timeWanted)
    {
        instance.audioSource.time = timeWanted;
        instance.audioSource.UnPause();
    }
    public static void SetSoundVolume(int _i, float _volume)
    {

        if (_i == (int)SoundInfo.MASTER)
        {
            instance.audioMixers[_i].audioMixer.SetFloat("MasterVolume", _volume);
            PlayerPrefs.SetFloat("MasterVolume", _volume);

        }
        else if (_i == (int)SoundInfo.SOUNDS)
        {
            instance.audioMixers[_i].audioMixer.SetFloat("MusicVolume", _volume);
            PlayerPrefs.SetFloat("MusicVolume", _volume);

        }
        else if (_i == (int)SoundInfo.FX)
        {
            instance.audioMixers[_i].audioMixer.SetFloat("FXVolume", _volume);
            PlayerPrefs.SetFloat("FXVolume", _volume);
        }
        PlayerPrefs.Save();
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public string name;
    public AudioClip Sounds { get => sounds; }
    public AudioMixerGroup mixer;
    [SerializeField] private AudioClip sounds;
}
