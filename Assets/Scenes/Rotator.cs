using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 45f;

    void Update()
    {
        // Rotate the coin around its own center on the Z axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}