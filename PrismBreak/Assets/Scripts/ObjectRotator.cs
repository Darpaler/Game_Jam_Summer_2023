using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    // Properties
    [SerializeField]
    private float rotationAmount = 15f;
    private void OnMouseDown()
    {
        transform.Rotate(Vector3.up * rotationAmount, Space.World);
    }
}
