using System.Threading;
using TMPro;
using UnityEngine;

public class DotBehaviour : MonoBehaviour
{
    private int id;
    private bool clicked = false;
    private DotsManager dotsManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dotsManager = GetComponentInParent<DotsManager>();
        GetComponentInChildren<TextMeshProUGUI>().text = id.ToString();
    }

    public void SetId(int _id)
    {
        id = _id;
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (!clicked) //Quand on clique dessus (ça me parait assez clair mais bon)
        {
            ++dotsManager.streak;
            clicked = true;
            dotsManager.Strike();
            dotsManager.TriggerParticle(gameObject.transform.position);
            GetComponent<Animator>().SetTrigger("Checked");
            SoundManager.PlaySound(SoundType.QTE_CIRCLE);
        }
    }
}
