using UnityEngine;

public class Caller : MonoBehaviour
{
    [SerializeField] private Receiver receiver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello Friend");
        receiver.OnCalled();
    }

    // Update is called once per frame
    void Update()
    {

    }
}


