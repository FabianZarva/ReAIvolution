using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreaks : MonoBehaviour
{
    public Rigidbody rb;
    public float extraForce = 1000f;
    public HingeJoint hingeJoint;
    private Vector3 lastConnectedBodyPosition;

    void Update()
    {
        if (hingeJoint.connectedBody != null)
        {
            lastConnectedBodyPosition = hingeJoint.connectedBody.transform.position;
        }
    }
    void OnJointBreak(float breakForce)
    {
        Vector3 forceDirection = lastConnectedBodyPosition - transform.position;
        forceDirection.Normalize();

        // Apply extra force in the direction the GameObject was pushed
        rb.AddForce(forceDirection * extraForce, ForceMode.Impulse);
    }
}
