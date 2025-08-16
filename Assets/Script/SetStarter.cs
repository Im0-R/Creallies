using PokeDatas;
using System.Linq;
using UnityEngine;
using static PokeDatas.GameData;

public class SetStarter : MonoBehaviour
{
    public void SetStarterGraph()
    {
        GameData.Instance.infoplayer.starterChoose = 1;
    }

    public void SetStarterProg()
    {
        GameData.Instance.infoplayer.starterChoose = 2;
    }
    public void ConfirmButton()
    {

        GameData.AllieIndividual allieSpawned;
        if (GameData.Instance.infoplayer.starterChoose == 1)
        {
            allieSpawned = new GameData.AllieIndividual(GameData.Instance.Allies[(int)AlliesId.PENSOUL - 1]);
            GameData.Instance.stockedIndividualAllie = allieSpawned;
        }
        else /*(GameData.Instance.infoplayer.starterChoose == 2)*/
        {
            allieSpawned = new GameData.AllieIndividual(GameData.Instance.Allies[(int)AlliesId.SEAPLUSH - 1]);
            GameData.Instance.stockedIndividualAllie = allieSpawned;
        }
        allieSpawned.stats.natures = (Natures)UnityEngine.Random.Range((int)Natures.SHY, (int)Natures.ATTENTIVE + 1);
        allieSpawned.stats.hp = 80 * allieSpawned.rarity;
        allieSpawned.stats.speed = 3 * allieSpawned.rarity;
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


        GameData.Instance.stockedIndividualAllie = allieSpawned;
        GameData.Instance.AddCreamon();
        GameData.Instance.infoplayer.firstTime = false;
        GameData.Instance.SaveInfoToJson();
    }
}
