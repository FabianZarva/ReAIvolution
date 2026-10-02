using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class FloorMechanic : MonoBehaviour
{
    public bool OnOffB;
    public bool GameStarted;

    public int position;
    public int sec;

    public void PressedButton()
    {
        OnOffB = false;
        if(position == 1)
        {
            sec = 1;
        }
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameStarted == true);
        sec = Random.Range(10, 40);
        while (true)
        {
            yield return new WaitUntil(() => GameStarted == true);
            while (sec > 0)
            {
                yield return new WaitForSeconds(1f);
                sec = sec - 1;
            }
            if (GameStarted == true)
            {
                position = position + 1;
            }
            if (position == 1)
            {
                sec = 25;
            }
            else
            {
                sec = Random.Range(10, 50);
            }
        }
    }

    public bool x, y, z;
    public GameObject GreenLight, RedLight;
    void Update()
    {
        if (GameStarted == false)
        {
            FindObjectOfType<AudioManager>().Stop("FloorCharging");
        }

        if (position == 0)
        {
            FindObjectOfType<AudioManager>().Stop("FloorCharging");
            x = true;
        }

        if (position == 1)
        {
            if (x)
            {
                x = false;
                OnOffB = true;
                FindObjectOfType<AudioManager>().Play("FloorCharging");
            }
        }

        if (position == 2)
        {
            FindObjectOfType<AudioManager>().Stop("FloorCharging");
            if (!OnOffB)
            {
                position = 0;
            }
            else
            {
                position = 0;
                StartCoroutine(Death());
            }
        }

        if (!OnOffB)
        {
            RedLight.SetActive(true);
            GreenLight.SetActive(false);
            FindObjectOfType<AudioManager>().Stop("FloorCharging");
        }
        else
        {
            GreenLight.SetActive(true);
            RedLight.SetActive(false);
        }
    }

    public GameObject Black;
    IEnumerator Death()
    {
        FindObjectOfType<AudioManager>().Play("EL");
        Black.SetActive(true);
        yield return new WaitForSeconds(2.6f);
        SceneManager.LoadScene("GameOver");
    }
}
