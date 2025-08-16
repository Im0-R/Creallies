using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class JustSoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void Start()
    {
        audioSource.clip = audioClip;
    }
    public void SoundEffectButton()
    {
        audioSource.Play();
    }
}
