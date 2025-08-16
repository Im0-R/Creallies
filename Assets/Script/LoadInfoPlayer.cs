using PokeDatas;
using UnityEngine;

public class LoadInfoPlayer : MonoBehaviour
{
    void Start()
    {
        GameData.Instance.LoadInfoToJson();
    }
}
