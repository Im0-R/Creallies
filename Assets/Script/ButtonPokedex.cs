using PokeDatas;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPokedex : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    Sprite UnobtainedSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if (GetComponent<Button>().interactable)
        {
            int id = int.Parse(gameObject.name) - 1;
            GameData.Instance.SetStockedAllie(id);
        }
    }
}
