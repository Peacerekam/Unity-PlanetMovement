using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTestMovement : MonoBehaviour
{
    private Vector3 anchorPoint;
    private int dir;

    private void Start()
    {
        dir = 1;
        anchorPoint = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        anchorPoint.y = anchorPoint.y + Time.deltaTime * dir;

        if (Mathf.Abs(anchorPoint.y) > 2f) {
            dir *= -1;
        }

        transform.Translate(anchorPoint * Time.deltaTime);
    }
}
