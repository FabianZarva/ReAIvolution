using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentController : MonoBehaviour
{
    public VentMechanic ventMechanic;

    private bool x,y,z;
    public float Timer;

    public Animator LoaderA;
    public Image LoaderI;

    public GameObject FaceGreen, FaceRed;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            y = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            y = false;
        }
    }
    public void Gazing()
    {
        x = true;
    }

    public void StopGazing()
    {
        LoaderA.SetBool("FadeIn", false);
        z = false;
        x = false;
    }

    public void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            Timer = 2f;
            LoaderI.fillAmount = 0f;
            if (x)
            {
                if (z)
                {
                    ventMechanic.VentState = !ventMechanic.VentState;
                    if (!x)
                    {
                        LoaderA.SetBool("FadeIn", false);
                    }
                    FaceGreen.SetActive(!FaceGreen.activeSelf);
                    FaceRed.SetActive(!FaceRed.activeSelf);
                }
            }
        }

        if (x)
        {
            LoaderI.fillAmount = LoaderI.fillAmount + Time.deltaTime / 2;

            if (y)
            {
                if (!z)
                {
                    z = true;
                    LoaderI.fillAmount = 0f;
                    Timer = 2f;
                    LoaderA.SetBool("FadeIn", true);
                }
            }
        }
    }
    
}
