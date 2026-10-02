using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PutHandleBack : MonoBehaviour
{
    public Transform handler;
    public Rigidbody rb, doorrb,handlerb;
    public XRGrabInteractable grabInteractable;

    public FollowPhysics followPhysics;

    public void OnRelease()
    {
        StartCoroutine(h());
    }

    IEnumerator h()
    {
        followPhysics.check = false;
        // Get the current velocity
        Vector3 currentVelocity = handlerb.velocity * 0.5f;

        // Give rb the opposite velocity
        doorrb.velocity = -currentVelocity;
        rb.velocity = -currentVelocity;
        handlerb.velocity = -currentVelocity;

        yield return new WaitForSeconds(0.2f);
        transform.position = handler.transform.position;
        transform.rotation = handler.transform.rotation;

        yield return new WaitForSeconds(0.2f);
        followPhysics.check = true;
    }

    private void Update()
    {
        if(Vector3.Distance(handler.position,transform.position) > 0.4f)
        {
            grabInteractable.enabled = false;
            grabInteractable.enabled = true;
        }
    }
}
