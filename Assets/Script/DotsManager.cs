using UnityEngine;

public class DotsManager : MonoBehaviour
{
    public int streak;
    private float time;
    private int nbSpawned;
    private GameObject[] clone;
    [SerializeField] FightManager fightManager;
    [SerializeField] GameObject prefabDot;
    [SerializeField] ParticleSystem DotParticle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        streak = 1;
        clone = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
        if (fightManager.fightType == 2)
        {
            time += Time.deltaTime;

            if (time < 5)
            {
                if (Mathf.Ceil(time) > nbSpawned)
                {
                    clone[nbSpawned] = Instantiate(prefabDot, this.transform); //On fait pop un bouton
                    clone[nbSpawned].GetComponent<DotBehaviour>().SetId(nbSpawned + 1);
                    clone[nbSpawned++].GetComponent<Transform>().position =
                        new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-3.0f, 2.5f), 1);
                }
            }

            switch (fightManager.fightPhase)
            {
                case 1:
                    time = 0;
                    fightManager.fightPhase = 2;
                    break;
                case 2:
                    fightManager.fightPhase = 3;
                    foreach (GameObject i in clone)
                    {
                        if (i || time < 5)
                        {
                            fightManager.fightPhase = 2;
                        }
                    }
                    break;
                case 3:
                    fightManager.Reset();
                    streak = 1;
                    nbSpawned = 0;
                    fightManager.CheckTargetAlive();
                    break;
                default:
                    break;
            }
        }
    }
    public void TriggerParticle(Vector3 _pos)
    {
        DotParticle.transform.position = _pos;
        DotParticle.Play();
    }
    public void Strike()
    {
        fightManager.TakeDamage((int)fightManager.loader.PlayerAlly.stats.atk);
    }
}
