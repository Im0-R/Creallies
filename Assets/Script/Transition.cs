using PokeDatas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public GameObject center;
    public GameObject grp;
    public GameObject buttonprog;
    public GameObject buttongraph;

    public GameObject Sure;
    public GameObject Yes;
    public GameObject No;

    void Update()
    {
        if (GameData.Instance.infoplayer.starterChoose == 0)
        {
            Sure.SetActive(false);
            Yes.SetActive(false);
            No.SetActive(false);
        }

        if (GameData.Instance.infoplayer.starterChoose == 1)
        {
            Vector3 distance = center.transform.position - buttongraph.transform.position;
            buttongraph.transform.Translate(distance * Time.deltaTime * 2.5f);
            buttonprog.transform.Translate(new Vector3(50, -50, 0));
            if (grp.transform.position.x <= 550)
            {
                grp.transform.Translate(new Vector3(50, 0, 0));
            }

            if (distance.x < 2.0f && distance.y < 2.0f)
            {
                Sure.SetActive(true);
                Yes.SetActive(true);
                No.SetActive(true);
            }
        }

        if (GameData.Instance.infoplayer.starterChoose == 2)
        {
            Vector3 distance = center.transform.position - buttonprog.transform.position;
            buttonprog.transform.Translate(distance * Time.deltaTime * 2.5f);
            buttongraph.transform.Translate(new Vector3(-50, 50, 0));
            if (grp.transform.position.x >= -1900)
            {
                grp.transform.Translate(new Vector3(-50, 0, 0));
            }

            if (distance.x < 2.0f && distance.y < 2.0f)
            {
                Sure.SetActive(true);
                Yes.SetActive(true);
                No.SetActive(true);
            }
        }
    }
}
