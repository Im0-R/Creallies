using PokeDatas;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static RandomAllie;

public class CaptureManager : MonoBehaviour
{
    public enum ZoneName
    {
        //Graph
        CREA_FRONT1,
        HALL1,
        CAFETERIA1,
        PIZZA_TRUCK1,
        SPACE1,

        //Prog
        CREA_FRONT2,
        HALL2,
        CAFETERIA2,
        PIZZA_TRUCK2,
        SPACE2,

        TOTAL_ZONE_NB
    }

    private enum Allies
    {
        CTRLZ,
        KANGLOUGLOU,
        SEAPLUSH,
        SEASHARP,
        PENSOUL,
        BRUSHSHADOW,
        TOUCOMPIL,
    }

    private struct Zone
    {
        public ZoneName name;
        public (Allies, float)[] alliesSpawnChance;
    }

    private Zone[] zoneList;

    [SerializeField]
    RawImage background;
    [SerializeField]
    RawImage imageAllie;

    [SerializeField]
    ParticleSystem shinyParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zoneList = new Zone[(int)ZoneName.TOTAL_ZONE_NB];

        //Loading of all the zone
        for (int i = 0; i < (int)ZoneName.TOTAL_ZONE_NB; i++)
        {
            if (Enum.IsDefined(typeof(ZoneName), i))
            {
                zoneList[i].name = (ZoneName)i;
            }
            else
            {
                Debug.LogError("Can't set a name to a zone with an index bigger than the number of zone");
            }
        }

        //Set the current background 
        if (GameData.Instance != null)
        {
            if (GameData.Instance.QRCodeStr != null)
            {
                SetBackground();
                SetAllie();
                GameData.Instance.QRCodeStr = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {

    }

    private void SetBackground()
    {
        switch (GameData.Instance.QRCodeStr)
        {
            case nameof(ZoneName.CREA_FRONT1):
            case nameof(ZoneName.CREA_FRONT2):

                background.texture = GameData.Instance.backGroundList[(int)ZoneName.CREA_FRONT1].texture;
                break;
            case nameof(ZoneName.HALL1):
            case nameof(ZoneName.HALL2):
                background.texture = GameData.Instance.backGroundList[(int)ZoneName.HALL1].texture;
                break;
            case nameof(ZoneName.CAFETERIA1):
            case nameof(ZoneName.CAFETERIA2):
                background.texture = GameData.Instance.backGroundList[(int)ZoneName.CAFETERIA1].texture;
                break;
            case nameof(ZoneName.PIZZA_TRUCK1):
            case nameof(ZoneName.PIZZA_TRUCK2):
                background.texture = GameData.Instance.backGroundList[(int)ZoneName.PIZZA_TRUCK1].texture;
                break;
            case nameof(ZoneName.SPACE1):
            case nameof(ZoneName.SPACE2):
                background.texture = GameData.Instance.backGroundList[(int)ZoneName.SPACE1].texture;
                break;
            default:
                Debug.LogError("GameData QR code scanned not in list of availble name");
                SceneManager.LoadScene("QRScanner");
                break;
        }
    }

    private void SetAllie()
    {
        GameData.AllieIndividual allie = GameData.Instance.stockedIndividualAllie;

        if (allie != null)
        {
            if (allie.isShiny)
            {
                imageAllie.texture = allie.spriteShiny.texture;
                shinyParticle.Play();
                SoundManager.PlaySound(SoundType.SHINY_POP);
            }
            else
            {
                imageAllie.texture = allie.sprite.texture;
            }
        }
    }

    public void Capture()
    {
        GameData.Instance.AddCreamon();
        SoundManager.PlaySound(SoundType.CATCH);
    }
}
