using PokeDatas;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class specificInfoManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Sprite shinySprite;
    public Sprite NotShinySprite;
    public Sprite[] captureLevelSprites;
    public Sprite[] raritySprites;

    public ParticleSystem shinyParticle;
    public Material controlZShader;

    public TMP_InputField inputField;
    void Start()
    {
        int id = GameData.Instance.GetStockedAllie().id;
        ChangeUIToSpecificAllie(id - 1);
    }

    // Update is called once per frame
    void Update()
    {
    }



    private void ChangeUIToSpecificAllie(int _id)
    {
        ChangeNumberText(_id);
        ChangeSprite(_id);
        ChangeCaptureZone(_id);
        ChangeName(_id);
        ChangeDimension(_id);
        ChangeBackGround(_id);
        ChangeRarity();
        ChangeCaptureLevel(_id);
        ChangeQRField(_id);
    }
    public void ChangeShinyIcon()
    {
        if (GameData.Instance.Pokedex[GameData.Instance.GetStockedAllie().id - 1].isShinyObtained)
        {
            if (GameObject.Find("ImgShinyButton").GetComponent<Image>().sprite == shinySprite)
            {
                GameObject.Find("ImgShinyButton").GetComponent<Image>().sprite = NotShinySprite;
                GameObject.Find("ImgPokemon").GetComponent<Image>().sprite = GameData.Instance.GetStockedAllie().sprite;
            }
            else if (GameObject.Find("ImgShinyButton").GetComponent<Image>().sprite == NotShinySprite)
            {
                GameObject.Find("ImgShinyButton").GetComponent<Image>().sprite = shinySprite;
                GameObject.Find("ImgPokemon").GetComponent<Image>().sprite = GameData.Instance.GetStockedAllie().spriteShiny;
                shinyParticle.Play();
            }
        }
    }
    private void ChangeRarity()
    {
        GameObject.Find("ImgRarity").GetComponent<SpriteRenderer>().sprite = raritySprites[GameData.Instance.GetStockedAllie().rarity - 1];
    }
    private void ChangeQRField(int _id)
    {
        inputField.text = GameData.Instance.GetStockedAllie().name;
        if (GameData.Instance.Pokedex[_id].captureLevel == 5)
        {
            GameObject[] onlyInactive = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(sr => !sr.gameObject.activeInHierarchy).ToArray();
            foreach (GameObject obj in onlyInactive)
            {
                if (obj != null)
                {
                    if (obj.name == "QRCodeButton")
                    {
                        obj.SetActive(true);
                    }
                }
            }

        }
    }
    private void ChangeCaptureLevel(int _id)
    {
        GameObject.Find("ImgCaptureLevel").GetComponent<SpriteRenderer>().sprite = captureLevelSprites[GameData.Instance.Pokedex[_id].captureLevel];
    }
    private void ChangeSprite(int _id)
    {
        GameObject.Find("ImgPokemon").GetComponent<Image>().sprite = GameData.Instance.GetStockedAllie().sprite;
        //if (_id == (int)Pokemons.CONTROL_Z -1)
        //{
        //    GameObject.Find("ImgPokemon").GetComponent<Image>().material = controlZShader;
        //}
    }

    private void ChangeNumberText(int _id)
    {
        string stringId = _id.ToString();
        _id++;
        if (_id >= 0 && _id < 10)
        {
            GameObject.Find("TextNumber").GetComponent<TextMeshProUGUI>().text = "00" + _id;
        }
        else if (_id > 9 && _id < 100)
        {
            GameObject.Find("TextNumber").GetComponent<TextMeshProUGUI>().text = "0" + _id;
        }
    }
    private void ChangeDimension(int _id)
    {
        GameObject.Find("TextHeight").GetComponent<TextMeshProUGUI>().text = GameData.Instance.GetPokedexInfo(_id).height.ToString() + "m";
        GameObject.Find("TextWeight").GetComponent<TextMeshProUGUI>().text = GameData.Instance.GetPokedexInfo(_id).weight.ToString() + "kg";
    }
    private void ChangeName(int _id)
    {
        GameObject.Find("TextName").GetComponent<TextMeshProUGUI>().text = GameData.Instance.GetStockedAllie().name;
    }
    private void ChangeCaptureZone(int _id)
    {
        string str = "null";
        switch (GameData.Instance.GetPokedexInfo(_id).zoneName)
        {
            case CaptureManager.ZoneName.CREA_FRONT1:
            case CaptureManager.ZoneName.CREA_FRONT2:
                str = "Crea Front";
                break;
            case CaptureManager.ZoneName.HALL1:
            case CaptureManager.ZoneName.HALL2:
                str = "Hall";
                break;
            case CaptureManager.ZoneName.CAFETERIA1:
            case CaptureManager.ZoneName.CAFETERIA2:
                str = "Cafeteria";
                break;
            case CaptureManager.ZoneName.PIZZA_TRUCK1:
            case CaptureManager.ZoneName.PIZZA_TRUCK2:
                str = "Pizza Truck";
                break;
            case CaptureManager.ZoneName.SPACE1:
            case CaptureManager.ZoneName.SPACE2:
                str = "Space";
                break;
            default:
                Debug.LogError("No zone detected");
                break;
        }
        GameObject.Find("TextCaptureZone").GetComponent<TextMeshProUGUI>().text = str;


    }
    private void ChangeBackGround(int _id)
    {
        GameObject.Find("Canvas").GetComponent<Image>().sprite = GameData.Instance.backGroundBlurredList[(int)GameData.Instance.GetPokedexInfo(_id).zoneName];
    }
}