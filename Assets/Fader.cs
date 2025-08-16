using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    void QuitFight()
    {
        LoadingManager.instance.LoadSceneAsync("Menu");
        SoundManager.PlayMusic(SoundType.MAIN_MENU_MUSIC);
    }
}
