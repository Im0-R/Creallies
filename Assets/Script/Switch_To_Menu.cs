using PokeDatas;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Switch_To_Menu : MonoBehaviour, IPointerDownHandler//, IPointerUpHandler
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private float timer = 0.0f;
    private bool isActivated = false;
    public string sceneName = "";

    public void Start()
    {
        //audioSource.clip = audioClip;
    }
    public void SoundEffectButton()
    {
        //audioSource.Play();
        SoundManager.PlaySound(SoundType.BUTTON_PRESSED);
    }
    public void Update()
    {
        if (isActivated == true)
        {
            //    if (SceneManager.GetActiveScene().name == "Title Screen" && GameData.Instance.infoplayer.firstTime == true)
            //    {
            //        SceneManager.LoadScene("PseudoPlayer");
            //    }
            //    else if (SceneManager.GetActiveScene().name == "PseudoPlayer" && GameData.Instance.infoplayer.firstTime == true)
            //    {
            //        SceneManager.LoadScene("StarterChoose");
            //    }
            //    else
            //    {
            //        ChangeScene(sceneName);
            //    }
            //    isActivated = false;
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isActivated = true;
    }
}
