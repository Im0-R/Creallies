using UnityEngine;
using UnityEngine.SceneManagement;
using PokeDatas;
public class ChangeScene : MonoBehaviour
{
    public static string ButtonPressed;
    public void MoveToScene(int _scene)
    {
        SceneManager.LoadScene(_scene);
    }
    public void MoveToScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }
    public void MoveToSceneButton(string _scene)
    {
        if (SceneManager.GetActiveScene().name == "Title Screen" && GameData.Instance.infoplayer.firstTime == true)
        {
            LoadingManager.instance.LoadSceneAsync("PseudoPlayer");
            SoundManager.PlaySound(SoundType.BUTTON_PRESSED);
        }
        else if (SceneManager.GetActiveScene().name == "PseudoPlayer" && GameData.Instance.infoplayer.firstTime == true)
        {
            LoadingManager.instance.LoadSceneAsync("StarterChoose");
            SoundManager.PlaySound(SoundType.BUTTON_PRESSED);
        }
        else if (SceneManager.GetActiveScene().name == "StarterChoose" && GameData.Instance.infoplayer.firstTime == true)
        {
            LoadingManager.instance.LoadSceneAsync("Replace Profile Picture");
            SoundManager.PlaySound(SoundType.BUTTON_PRESSED);
        }
        else
        {
            LoadingManager.instance.LoadSceneAsync(_scene);
            SoundManager.PlaySound(SoundType.BUTTON_PRESSED);
        }
    }
}
