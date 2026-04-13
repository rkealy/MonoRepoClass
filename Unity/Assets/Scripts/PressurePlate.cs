using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pressure Plate Activated");
    }
}
