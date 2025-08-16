using PokeDatas;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RandomSpawnManager : MonoBehaviour
{
    private enum ZoneName
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

    private struct Zone
    {
        public ZoneName name;
        public (AlliesId, float)[] alliesSpawnChance;
    }

    private Zone[] zoneList;

    [SerializeField]
    RawImage background;
    [SerializeField]
    RawImage imageAllie;
    [SerializeField]
    TextMeshProUGUI textFoundAllie;

    [SerializeField]
    ParticleSystem shinyParticle;

    bool isShiny = false;
    private bool isSharedAllieFound = false;
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

        //Setting all zone allies drop rate
        SetSpawnRate();

        //Set the current background 
        if (GameData.Instance != null)
        {
             SetBackground();
             if (!isSharedAllieFound)
             {
                 SetRandomAllie();
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
    private void SetTextFoundAllie(string _name)
    {
        textFoundAllie.text = " You just found a " + _name + "!";
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
                SetSharedAllie();
                break;
        }
    }
    private void SetSharedAllie()
    {
        int idAllie = 25;
        isSharedAllieFound = false;
        Debug.Log(GameData.Instance.QRCodeStr);
        for (int i = 0; i < GameData.Instance.Allies.Count; i++)
        {
            Debug.Log(GameData.Instance.Allies[i].name);
            if (GameData.Instance.Allies[i].name + "1" == GameData.Instance.QRCodeStr ||
                GameData.Instance.Allies[i].name + "2" == GameData.Instance.QRCodeStr)
            {
                idAllie = i;
                isSharedAllieFound = true;
                break;
            }
        }
        if (isSharedAllieFound)
        {
            if (imageAllie != null)
            {
                GameData.AllieIndividual allieSpawned = new GameData.AllieIndividual(GameData.Instance.Allies[idAllie]);

                allieSpawned.isShiny = RollForShiny();
                allieSpawned.exp = 0;
                RollRandomStat(allieSpawned);

                GameData.Instance.stockedIndividualAllie = allieSpawned;
                GameData.Instance.QRCodeStr = "SPACE1";

                if (GameData.Instance.stockedIndividualAllie.isShiny)
                {
                    imageAllie.texture = GameData.Instance.Allies[allieSpawned.id - 1].spriteShiny.texture;
                }
                else
                {
                    imageAllie.texture = GameData.Instance.Allies[allieSpawned.id - 1].sprite.texture;
                }

                background.texture = GameData.Instance.backGroundList[(int)ZoneName.SPACE1].texture;
            }
        }
        else
        {
            LoadingManager.instance.LoadSceneAsync("QRScanner");
            Debug.LogError("GameData QR code scanned not in list of availble name");
        }
    }
    private void SetRandomAllie()
    {
        //GameData.Allie allieSpawned;
        int zoneIn = 0;
        for (int i = 0; i < (int)ZoneName.TOTAL_ZONE_NB; i++)
        {
            if (GameData.Instance.QRCodeStr == Enum.GetName(typeof(ZoneName), i))
            {
                zoneIn = i;
            }
        }

        //Spawn an random allie in the zone
        float minRange = 0.0f;
        float randomFloat = UnityEngine.Random.Range(0.0f, 100.0f);
        Debug.Log(randomFloat);

        for (int i = 0; i < zoneList[zoneIn].alliesSpawnChance.Length; i++)
        {
            Debug.Log("Min Range: " + minRange);

            if (minRange <= randomFloat && minRange + zoneList[zoneIn].alliesSpawnChance[i].Item2 > randomFloat)
            {
                if (imageAllie != null)
                {
                    GameData.AllieIndividual allieSpawned = new GameData.AllieIndividual(GameData.Instance.Allies[(int)zoneList[zoneIn].alliesSpawnChance[i].Item1 - 1]);

                    allieSpawned.isShiny = RollForShiny();
                    allieSpawned.exp = 0;
                    RollRandomStat(allieSpawned);

                    Debug.Log("Spawned Allie:");
                    Debug.Log("Id allie: " + allieSpawned.id);
                    Debug.Log("Name: " + allieSpawned.name);
                    Debug.Log("Is shiny: " + allieSpawned.isShiny);

                    //Setter for the text on the scene
                    SetTextFoundAllie(allieSpawned.name);
                    GameData.Instance.stockedIndividualAllie = allieSpawned;

                    if (GameData.Instance.stockedIndividualAllie.isShiny)
                    {
                        imageAllie.texture = GameData.Instance.Allies[allieSpawned.id - 1].spriteShiny.texture;
                    }
                    else
                    {
                        imageAllie.texture = GameData.Instance.Allies[allieSpawned.id - 1].sprite.texture;
                    }

                    Debug.Log("Allies Spawned Debug log:");
                    Debug.Log("Id used: " + GameData.Instance.Allies[(int)zoneList[zoneIn].alliesSpawnChance[i].Item1 - 1].id);
                }
                break;
            }
            minRange += zoneList[zoneIn].alliesSpawnChance[i].Item2;

            Debug.Log("Max Range: " + minRange);
        }
    }

    private void SetSpawnRate()
    {
        //Be cautious that the total spawn chance add up to 100%
        zoneList[(int)ZoneName.CREA_FRONT1].alliesSpawnChance = new[]
        {
            (AlliesId.PENSOUL, 30.0f),
            (AlliesId.CIGARGOYLE, 30.0f),
            (AlliesId.YEK, 17.5f),
            (AlliesId.POLTERGHEISLER, 17.5f),
            (AlliesId.SHRIECKEN, 5.0f)
        };

        zoneList[(int)ZoneName.HALL1].alliesSpawnChance = new[]
        {
            (AlliesId.SNAEKY, 30.0f),
            (AlliesId.CINTY, 30.0f),
            (AlliesId.SQUEAK, 20.0f),
            (AlliesId.CLEANERMIT, 20.0f),
        };

        zoneList[(int)ZoneName.CAFETERIA1].alliesSpawnChance = new[]
        {
            (AlliesId.KANGLOUGLOU, 30.0f),
            (AlliesId.JAVERMIN, 30.0f),
            (AlliesId.MILESTUN, 20.0f),
            (AlliesId.BRAWNNY, 20.0f),
        };

        zoneList[(int)ZoneName.PIZZA_TRUCK1].alliesSpawnChance = new[]
        {
            (AlliesId.SHRIECKEN, 20.0f),
            (AlliesId.YEK, 20.0f),
            (AlliesId.ROBONO, 30.0f),
            (AlliesId.CARTIPUS, 20.0f),
            (AlliesId.MILESTUN, 10.0f)
        };

        zoneList[(int)ZoneName.SPACE1].alliesSpawnChance = new[]
        {
            (AlliesId.ZENNOGA, 85.0f),
            (AlliesId.CONTROL_Z, 15.0f),
        };


        zoneList[(int)ZoneName.CREA_FRONT2].alliesSpawnChance = new[]
        {
            (AlliesId.SEAPLUSH, 30.0f),
            (AlliesId.CIGARGOYLE, 30.0f),
            (AlliesId.YEK, 17.5f),
            (AlliesId.POLTERGHEISLER, 17.5f),
            (AlliesId.SHRIECKEN, 5.0f)
        };

        zoneList[(int)ZoneName.HALL2].alliesSpawnChance = new[]
        {
            (AlliesId.SNAEKY, 30.0f),
            (AlliesId.CINTY, 30.0f),
            (AlliesId.TOUCOMPIL, 20.0f),
            (AlliesId.CLEANERMIT, 20.0f),
        };

        zoneList[(int)ZoneName.CAFETERIA2].alliesSpawnChance = new[]
        {
            (AlliesId.KANGLOUGLOU, 30.0f),
            (AlliesId.JAVERMIN, 30.0f),
            (AlliesId.MILESTUN, 20.0f),
            (AlliesId.BRAWNNY, 20.0f),
        };

        zoneList[(int)ZoneName.PIZZA_TRUCK2].alliesSpawnChance = new[]
        {
            (AlliesId.SHRIECKEN, 20.0f),
            (AlliesId.YEK, 20.0f),
            (AlliesId.ROBONO, 30.0f),
            (AlliesId.CARTIPUS, 20.0f),
            (AlliesId.MILESTUN, 10.0f)
        };

        zoneList[(int)ZoneName.SPACE2].alliesSpawnChance = new[]
        {
            (AlliesId.ZENNCHIDE, 85.0f),
            (AlliesId.CONTROL_Z, 15.0f),
        };
    }

    private bool RollForShiny()
    {
        if (UnityEngine.Random.Range(0, GameData.Instance.shinyRate) == 0)
        {
            shinyParticle.Play();
            SoundManager.PlaySound(SoundType.SHINY_POP);
            return true;
        }
        return false;
    }

    private void RollRandomStat(GameData.AllieIndividual allieSpawned)
    {
        // Set stat allie
        allieSpawned.stats.natures = (Natures)UnityEngine.Random.Range((int)Natures.SHY, (int)Natures.ATTENTIVE + 1);
        allieSpawned.stats.hp = 80 * allieSpawned.rarity;
        allieSpawned.stats.speed = 3 * allieSpawned.rarity ;
        allieSpawned.stats.atk = 4 * allieSpawned.rarity;
        switch (allieSpawned.stats.natures)
        {
            case Natures.SHY:
                allieSpawned.stats.hp *= 1.2f;
                allieSpawned.stats.atk *= 0.8f;
                break;
            case Natures.NERVOUS:
                allieSpawned.stats.speed *= 1.2f;
                allieSpawned.stats.atk *= 0.8f;
                break;
            case Natures.IMPULSIVE:
                allieSpawned.stats.atk *= 1.2f;
                allieSpawned.stats.hp *= 0.8f;
                break;
            case Natures.HASTY:
                allieSpawned.stats.speed *= 1.2f;
                allieSpawned.stats.hp *= 0.8f;
                break;
            case Natures.CONFIDENT:
                allieSpawned.stats.atk *= 1.2f;
                allieSpawned.stats.speed *= 0.8f;
                break;
            case Natures.ATTENTIVE:
                allieSpawned.stats.hp *= 1.2f;
                allieSpawned.stats.speed *= 0.8f;
                break;
            default:
                break;
        }

        Debug.Log("Stats:");
        Debug.Log("Hp: " + allieSpawned.stats.hp + "\tAtk: " + allieSpawned.stats.atk + "\tSpeed: " + allieSpawned.stats.speed);
    }
}