using UnityEngine;

public class RandomAllie : MonoBehaviour
{
    public struct AllieStat
    {
        public AlliesId name;
        public int rarity;
        public Natures nature;
        public bool isShiny;
    }

    public AllieStat allieStat = new AllieStat();

    public void SetStat(AllieStat _allieStat)
    {
        allieStat = _allieStat;

        Debug.Log("AllieStat: ");
        Debug.Log(allieStat.name);
        Debug.Log(allieStat.rarity);
        Debug.Log(allieStat.nature);
        Debug.Log(allieStat.isShiny);

    }

    public void SetStat(AlliesId _name, int _rarity, Natures _nature, bool _isShiny)
    {
        allieStat.name = _name;
        allieStat.rarity = _rarity;
        allieStat.nature = _nature;
        allieStat.isShiny = _isShiny;
    }
}
