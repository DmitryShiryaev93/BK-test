using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject POI;
    public float easing = 0.05f;

    void FixedUpdate()
    {
        if (POI == null) return; 
        Vector3 destination = POI.transform.position;
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = transform.position.z;
        transform.position = destination;
    }
}
