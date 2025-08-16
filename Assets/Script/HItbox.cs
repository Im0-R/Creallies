using PokeDatas;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{
    public PV health;
    [SerializeField] FightManager fightManager;

    void Update()
    {
        if (health.displayedHP <= 0 && transform.GetComponent<Image>().color.a >= 0 && fightManager.fightType == 0)
        {
            GetComponent<Animator>().SetTrigger("Faint");
            SoundManager.PlaySound(SoundType.ALLIE_HIT);
        //music.Stop();
        }
    }

    void EndFight()
    {
        fightManager.EndFight();
    }
}
