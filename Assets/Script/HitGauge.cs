using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HitGauge : MonoBehaviour
{
    private float power;
    private bool cooldown = false;
    [SerializeField] FightManager fightManager;
    private Image sprite;
    [SerializeField] Image outline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        power = 0;
        sprite = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown)
        {
            power -= fightManager.loader.PlayerAlly.stats.speed;
            if (power <= 0)
            {
                power = 0;
                if (fightManager.fightType == 0)
                {
                    cooldown = false;
                }
            }
        }
        else
        {
            if (power > 0 && fightManager.fightType == 0)
            {
                power--;
            }

            if (Input.GetMouseButtonUp(0) && fightManager.HP > 0)
            {
                power += 5 + fightManager.loader.PlayerAlly.stats.speed * 2; //Quand on fait monter la jauge
                SoundManager.PlaySound(SoundType.CLICK_BAR);
            }

            if (power > 100)
            {
                power = 100;
                cooldown = true;

                fightManager.fightType = UnityEngine.Random.Range(1, 3); //Quand on lance un QTE
            }
        }

        sprite.fillAmount = power / 100;
        sprite.color = new Color(0.8f, 1 - power / 100, 0, 1);
        outline.color = new Color(0.6f, (1 - power / 100) - 0.4f, 0, 1);
    }
}