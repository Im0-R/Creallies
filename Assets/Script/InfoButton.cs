using JetBrains.Annotations;
using PokeDatas;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class InfoButton : MonoBehaviour
{

    public void ShowImageOnTrigger(string _string)
    {
        if (GameObject.Find(_string) != null)
        {
            if (GameObject.Find(_string).activeInHierarchy)
            {
                GameObject.Find(_string).SetActive(false);
                return;
            }
        }
        GameObject[] onlyInactive = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(sr => !sr.gameObject.activeInHierarchy).ToArray();
        foreach (GameObject obj in onlyInactive) 
        {
            if (obj != null)
            {
                if (obj.name == _string)
                {
                    obj.SetActive(true);
                }
            }
        }

    }
}
