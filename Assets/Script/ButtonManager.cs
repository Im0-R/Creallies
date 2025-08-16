using JetBrains.Annotations;
using PokeDatas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Sprite UnobtainedSprites;
    private void Awake()
    {
    }
    void Start()
    {
        for (int i = 0; i < GetComponentsInChildren<Image>().Length; i++)
        {
            if (!GameData.Instance.GetPokedexInfo(i).isPokemonCatched)
            {
                GetComponentsInChildren<Image>()[i].sprite = UnobtainedSprites;
                GetComponentsInChildren<Button>()[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
