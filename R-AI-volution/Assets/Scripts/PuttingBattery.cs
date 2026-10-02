using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class PuttingBattery : MonoBehaviour
{
    public Animator PanelA;
    public GameObject OfflineIn, OnlineIn, BatteryHolding, BatteryInTheThing, WarnT;
    public XRGrabInteractable xRGrabInteractable;
    public bool USB;
    public GameObject Collder;

    public void OnTriggerEnter(Collider other)
    {
        if (USB)
        {
            if (other.gameObject.tag == "USB")
            {
                WarnT.SetActive(false);
                OfflineIn.SetActive(false);
                OnlineIn.SetActive(true);
                BatteryHolding.SetActive(false);
                BatteryInTheThing.SetActive(true);
                xRGrabInteractable.enabled = true;
                Collder.SetActive(false);
            }
        }
        else
        {
            if (other.gameObject.tag == "Battery")
            {
                PanelA.SetBool("Open", false);
                WarnT.SetActive(false);
                OfflineIn.SetActive(false);
                OnlineIn.SetActive(true);
                BatteryHolding.SetActive(false);
                BatteryInTheThing.SetActive(true);
                xRGrabInteractable.enabled = true;
                Collder.SetActive(false);
            }
        }
    }
}
