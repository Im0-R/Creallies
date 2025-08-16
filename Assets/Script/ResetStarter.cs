using PokeDatas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetStarter : MonoBehaviour
{
    public void Reset_Starter()
    {
        GameData.Instance.infoplayer.starterChoose = 0;
        SceneManager.LoadScene("StarterChoose");
    }
}
