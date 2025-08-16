using PokeDatas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecupPseudo : MonoBehaviour
{
    public TMP_InputField field;

    public void SetPseudo()
    {
        GameData.Instance.infoplayer.PseudoPlayer = field.text;
        GameData.Instance.infoplayer.starterChoose = 0;
    }
}