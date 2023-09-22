using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDynamicController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private void LateUpdate()
    {
        transform.Rotate(transform.forward * Time.deltaTime * rotationSpeed);
    }
}
