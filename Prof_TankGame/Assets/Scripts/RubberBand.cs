using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBand : MonoBehaviour
{
    public Transform target;
    public float rubberStrength;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, target.position,
        //    rubberStrength * Time.deltaTime);
    }

    // Update is called once per physics frame 50 per second
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position,
            rubberStrength * Time.fixedDeltaTime);
    }


}
