using PokeDatas;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class FightSceneLoader : MonoBehaviour
{
    public int environment = 0;
    public SpriteRenderer background;
    public Image Player;
    public Image Enemy;
    public FightManager manager;
    public Sprite[] bgTextures;
    public GameData.AllieIndividual PlayerAlly;
    public GameData.AllieIndividual EnemyAlly;

    private GameData data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        data = FindAnyObjectByType<GameData>();
        PlayerAlly = data.PlayerBox[data.mainSelectedAllie];
        EnemyAlly = data.stockedIndividualAllie;

        environment = (int)data.GetPokedexInfo(EnemyAlly.id).zoneName;
        background.sprite = bgTextures[environment];

        if (PlayerAlly.isShiny)
        {
            Player.sprite = PlayerAlly.spriteShiny;
        }
        else
        {
            Player.sprite = PlayerAlly.sprite;
        }

        if (EnemyAlly.isShiny)
        {
            Enemy.sprite = EnemyAlly.spriteShiny;
        }
        else
        {
            Enemy.sprite = EnemyAlly.sprite;
        }

        manager.maxHP = EnemyAlly.stats.hp;
        manager.HP = manager.maxHP;
    }
}