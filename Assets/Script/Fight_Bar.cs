using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Fight_Bar : MonoBehaviour
{
    private int phase = 0;
    private Vector3 norm;
    private int damage;
    private float time;

    public int id;
    public FightManager fightManager;
    public Transform cursor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        norm = transform.rotation * Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (fightManager.fightPhase == id && fightManager.fightType == 1)
        {
            switch (phase)
            {
                case 0: //La jauge apparait
                    transform.localScale += new Vector3(0, 2.0f * Time.deltaTime, 0);
                    if (transform.localScale.y >= 1)
                    {
                        if (fightManager.fightPhase == 0)
                        {
                            time += Time.deltaTime;
                            transform.localScale = new Vector3(1, 1, 1.0f);
                            if (time >= 1.5f)
                            {
                                phase = 1;
                            }
                        }
                        else
                        {
                            transform.localScale = new Vector3(1, 1, 1.0f);
                            phase = 1;
                        }
                    }
                    break;
                case 1: //La barre a fini d'aparaitre et le curseur commence à bouger
                    cursor.position += norm * 3 * Time.deltaTime;
                    //Debug.Log(cursor.localPosition.magnitude);
                    if (Input.GetMouseButtonDown(0))
                    {
                        damage = (int)(fightManager.loader.PlayerAlly.stats.atk * 5 / (cursor.localPosition.magnitude / 200 + 1));
                        fightManager.TakeDamage(damage);

                        phase = 2;
                        cursor.localPosition = new Vector3(-700, 0, 0);
                    }
                    if (cursor.localPosition.x > 600)
                    {
                        phase = 2;
                    }
                    break;
                case 2: //La barre disparait
                    transform.localScale += new Vector3(0, -2.0f * Time.deltaTime, 0);
                    if (transform.localScale.y < 0)
                    {
                        transform.localScale = new Vector3(1, 0.0f, 1.0f);
                        cursor.localPosition = new Vector3(-600, 0, 0);
                        phase = 0;
                        if (fightManager.fightPhase >= 3)
                        {
                            fightManager.Reset();
                        }
                        else
                        {
                            ++fightManager.fightPhase;
                        }
                        fightManager.CheckTargetAlive();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
