using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDemo : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Stay!");
        if (other.gameObject.tag == "Screw")
        {
            Debug.Log("Screw detected!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
