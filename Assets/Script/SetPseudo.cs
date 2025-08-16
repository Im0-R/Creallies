using PokeDatas;
using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI pseudoUpdate;
    void Start()
    {
        pseudoUpdate.text = GameData.Instance.infoplayer.PseudoPlayer;
    }
}
