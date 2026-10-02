using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopDoor : MonoBehaviour
{
    public HingeJoint Doorjoint;
    private JointMotor motor;

    void Start()
    {
        motor = Doorjoint.motor;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            motor.force = 20;
            Doorjoint.motor = motor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            motor.force = 0.5f;
            Doorjoint.motor = motor;
        }
    }
}
