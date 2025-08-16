using UnityEngine;
using UnityEngine.UIElements;

public class RotateObject : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0, 20 * Time.deltaTime);
    }
}
