using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SceneManager.UnloadSceneAsync("Start");
        SceneManager.LoadSceneAsync("Title Screen");
        GameObject.Find("AudioSource").gameObject.GetComponent<SoundManager>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
