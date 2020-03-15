using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustColliders : MonoBehaviour
{
    [SerializeField]
    private GameObject gravityZone;

    [SerializeField]
    private GameObject planetCollider;

    private ShapeSettings shapeSettings;

    void Start()
    {
        shapeSettings = GetComponent<Planet>().shapeSettings;

        AdjustSizes();
    }

    public void AdjustSizes()
    {
        if (!shapeSettings) {
            shapeSettings = GetComponent<Planet>().shapeSettings;
        }

        planetCollider.transform.localScale = new Vector3(shapeSettings.planetRadius, shapeSettings.planetRadius, shapeSettings.planetRadius);
        gravityZone.transform.localScale = new Vector3(shapeSettings.planetRadius, shapeSettings.planetRadius, shapeSettings.planetRadius) + (1.5f * Vector3.one);
    }

}
