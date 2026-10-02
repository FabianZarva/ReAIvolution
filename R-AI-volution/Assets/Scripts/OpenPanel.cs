using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public Animator PanelA;
    public Collider PanelC;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Screw")
        {
            PanelA.SetBool("Open", true);
            PanelC.enabled = true;
        }
    }
}
