using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PV : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [HideInInspector] public float displayedHP = 100;
    public FightManager health;

    public bool IsAnimDone()
    {
        bool tg = Mathf.Approximately(displayedHP, health.HP / health.maxHP * 100); //DIEU MERCI
        return tg;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = (displayedHP * 1.0f) / 100;

        if (health.HP < 0)
        {
            health.HP = 0;
        }
        if (health.HP > health.maxHP)
        {
            health.HP = health.maxHP;
        }

        if (displayedHP > health.HP / health.maxHP * 100 && displayedHP - health.HP / health.maxHP * 100 >= 0.01f)
        {
            if (((displayedHP - health.HP / health.maxHP * 100) % 2) == 1)
            {
                --displayedHP;
            }
            else
            {
                displayedHP -= 2;
            }
        }
        else if (displayedHP < health.HP / health.maxHP * 100)
        {
            displayedHP = health.HP / health.maxHP * 100;
        }
    }
}
