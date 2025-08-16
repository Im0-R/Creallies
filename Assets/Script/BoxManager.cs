using PokeDatas;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField]
    private GameObject boxPrefab;

    [SerializeField]
    private GameObject boxOrderer;

    public void Start()
    {
        ShowBoxes();
    }

    public void ShowBoxes()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameData.Instance.PlayerBox.Count; i++)
        {
            GameObject newBoxButton = Instantiate(boxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newBoxButton.GetComponent<BoxButton>().Init(GameData.Instance.PlayerBox[i]);
            newBoxButton.transform.SetParent(boxOrderer.transform);
            newBoxButton.transform.localScale = Vector3.one;
            newBoxButton.GetComponent<BoxButton>().indexInList = i;
        }
    }

    
}