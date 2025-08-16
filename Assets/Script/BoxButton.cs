using PokeDatas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BoxButton : MonoBehaviour
{
    [SerializeField] private GameData.AllieIndividual allieBox = new GameData.AllieIndividual();
    [SerializeField] private Image image;
    [SerializeField] private SelectedCreamonManager selectedCreamon;
    [SerializeField] public int indexInList;


    public void Init(GameData.AllieIndividual allie)
    {
        allieBox = allie;
    }

    private void Awake()
    {
        selectedCreamon = FindAnyObjectByType<SelectedCreamonManager>();
    }

    void Start()
    {
        if (!allieBox.isShiny && allieBox.sprite != null)
        {
            image.sprite = allieBox.sprite;
        }
        else if (allieBox.spriteShiny != null)
        {
            image.sprite = allieBox.spriteShiny;
        }
    }

    public void GetCreamonSelected()
    {
        selectedCreamon.allieBox = allieBox;
        selectedCreamon.indexInList = indexInList;
        selectedCreamon.SetDatasOnScreen();
        if (selectedCreamon.allieBox.isShiny)
        {
            selectedCreamon.ShowShinyParticle();
        }
    }
}