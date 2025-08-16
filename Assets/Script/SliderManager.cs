using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SliderManager : MonoBehaviour
{
    [SerializeField] private GameObject[] sliderList;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSoundVolume(int _id)
    {
        SoundManager.SetSoundVolume(_id, sliderList[_id].GetComponent<Slider>().value);
    }
}
