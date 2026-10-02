using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    public GameObject JumpScare;
    public AudioSource Ambience;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            JumpScare.SetActive(true);
            Ambience.Stop();
            Destroy(gameObject);
        }
    }
}
