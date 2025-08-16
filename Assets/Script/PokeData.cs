using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.Jobs;
public enum AlliesId
{
    SEAPLUSH = 1,
    SEASHARP,
    PENSOUL,
    BRUSHADOW,
    TOUCOMPIL,
    SQUEAK,    
    YEK,
    JAVERMIN,
    JAVAQUEEN,
    CLEANERMIT,
    CIGARGOYLE,
    BRAWNNY,
    CINTY,
    MINTHIA,
    SHRIECKEN,
    POLTERGHEISLER,
    CARTIPUS,
    SNAEKY,
    KANGLOUGLOU,
    ROBONO,
    MILESTUN,
    HYDREADLINE,
    ZENNCHIDE,
    ZENNOGA,
    CONTROL_Z,
};
public enum Natures
{
    SHY = 1,
    NERVOUS = 2,
    IMPULSIVE = 3,
    HASTY = 4,
    CONFIDENT = 5,
    ATTENTIVE = 6,
}

[System.Serializable]
public struct Stats
{
    public float hp;
    public float atk;
    public float speed;
    public Natures natures;

    public Stats(int _hp, int _atk, int _speed, Natures _nature)
    {
        hp = _hp;
        atk = _atk;
        speed = _speed;
        natures = _nature;
    }
}
public class Pokemon
{
    //Contient les stats et les informations du Pokemon
    public Pokemon(Stats _stats , Sprite _sprite)
    {
        Stats stats = _stats;
        Sprite sprite = _sprite;
    }
    int id;

}
public static class PokeData
{
    //private static readonly object value = 19;
    //public Pokemon pokemonData[value)];

    static Pokemon pokemons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static void Start()
    {
        
    }

    // Update is called once per frame
    static void Update()
    {
        
    }
}
