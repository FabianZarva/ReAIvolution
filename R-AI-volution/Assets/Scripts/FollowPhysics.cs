using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPhysics : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public bool check = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (check)
        {
            rb.MovePosition(target.transform.position);
        }
    }
}
