using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Color_Change : MonoBehaviour
{
    public Color color;
    public float healthScale;
    private FightManager health;
    private UnityEngine.UI.Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        color = Color.green;
        health = this.transform.GetComponentInParent<PV>().health;
        image = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(health.HP.ToString() + "/" + health.maxHP.ToString());
        if (health.HP < health.maxHP / 4)
        {
            image.color = Color.red;
        }
        else if (health.HP < health.maxHP / 2)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.green;
        }
    }
}
