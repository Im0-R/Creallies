using PokeDatas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FightManager : MonoBehaviour
{
    public int fightPhase;
    public int fightType;
    public float HP;
    public float maxHP;
    public int zone;
    public PV healthBar;
    public Animator target;
    public Animator blackScreen;

    [SerializeField] TextMeshProUGUI beatenText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] public FightSceneLoader loader;
    //[SerializeField] FMODUnity.StudioEventEmitter hitSound;
    [SerializeField] ParticleSystem HitParicle;
    [SerializeField] ParticleSystem DotParticle;
    public void TakeDamage(int damage)
    {
        target.SetTrigger("Hurt");
        SoundManager.PlaySound(SoundType.ALLIE_ATTACK); //<-- C'est ici qu'il faut mettre le son quand on met un coup
        if (healthBar.IsAnimDone())
        {
            HP -= damage;
            HitParicle.Play();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fightPhase = 1;
        fightType = 0;

        enemyName.text = loader.EnemyAlly.name;
    }

    // Update is called once per frame
    public void CheckTargetAlive()
    {
        if (HP == 0)
        {
            Reset();
            EndFight();
        }
    }

    public void Reset()
    {
        fightPhase = 1;
        fightType = 0;
    }

    public void EndFight()
    {
        beatenText.text = loader.EnemyAlly.name + "\ndefeated !";
        SoundManager.PlaySound (SoundType.VICTORY);
        switch (loader.EnemyAlly.rarity - 1)
        {
            case 0:
                loader.PlayerAlly.exp += 10;
                expText.text = "+ 10 Exp";
                break;
            case 1:
                loader.PlayerAlly.exp += 30;
                expText.text = "+ 30 Exp";
                break;
            case 2:
                loader.PlayerAlly.exp += 50;
                expText.text = "+ 50 Exp";
                break;
            case 3:
                loader.PlayerAlly.exp += 100;
                expText.text = "+ 100 Exp";
                break;
            default:
                break;
        }
        blackScreen.SetTrigger("Fade");
    }
}
