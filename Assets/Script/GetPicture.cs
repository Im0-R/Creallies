using PokeDatas;
using UnityEngine;
using UnityEngine.UI;
public class GetPicture : MonoBehaviour
{
    public Sprite[] sprites;
    public void GetProfilPicture(int _index)
    {
        GameData.Instance.infoplayer.selectedPictures = _index;
    }
    private void Start()
    {
        if (sprites.Length > 0)
        {
            gameObject.GetComponent<Image>().sprite = sprites[GameData.Instance.infoplayer.selectedPictures];
        }
    }
}