using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour
{
    Vector3 newRotation;

    void Start()
    {
        newRotation = new Vector3(0f, 0f, 0f);
    }
    void Update()
    {
        newRotation.z -= Time.deltaTime;
        transform.eulerAngles = newRotation;
    }
}
