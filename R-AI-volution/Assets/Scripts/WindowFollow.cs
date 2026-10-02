using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowFollow : MonoBehaviour
{
    public Transform lerpPoint;
    public float lerpSpeed = 10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, lerpPoint.position, Time.unscaledDeltaTime * lerpSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, lerpPoint.rotation, Time.unscaledDeltaTime * lerpSpeed);
    }
}
