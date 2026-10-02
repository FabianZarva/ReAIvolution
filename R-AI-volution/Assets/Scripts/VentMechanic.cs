using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class VentMechanic : MonoBehaviour
{
    public bool VentState = true;
    public bool GameStarted;

    public int position;
    public int sec;

    public Animator RobotA;
    public EnemyAI enemyAI;
    public NavMeshAgent agent;

    public AudioSource WalkingVentAud;
    public AudioSource Thud;

    public HingeJoint VentDoor;
    public Material cameraFadeMat;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameStarted == true);
        sec = Random.Range(20, 60);
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
            if(position == 1)
            {
                sec = 23;
            }
            else
            {
                sec = Random.Range(30, 60);
            }
        }
    }

    // Update is called once per frame
    private bool x,y,z,m;
    public float O2 = 100f;

    public AudioSource ClosingVent, OpeningVent, OpenedVent;
    void Update()
    {
        if (GameStarted == false)
        {
            WalkingVentAud.Stop();
        }

        cameraFadeMat.SetFloat("_ApertureSize", O2 / 100);

        if (O2 <= 0f)
        {
            if (!z)
            {
                z = true;
                StartCoroutine(DeathFromO2());
            }
        }

        if (O2 > 100f)
        {
            O2 = 100f;
        }
        if (O2 < 30f)
        {
            if (!m)
            {
                m = true;
                FindObjectOfType<AudioManager>().Play("HB");
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().Stop("HB");
            m = false;
        }

        //Missing O2 Feedback Visuals
        if (!VentState)
        {
            if (y)
            {
                y = false;
                JointSpring spring = VentDoor.spring;

                // Change the target position
                spring.targetPosition = 5.0f;

                VentDoor.spring = spring;
                ClosingVent.Play();
                OpenedVent.Stop();
            }
            O2 -= Time.deltaTime * 3f;
        }
        else
        {
            if (!y)
            {
                y = true;
                JointSpring spring = VentDoor.spring;

                // Change the target position
                spring.targetPosition = -95.0f;

                VentDoor.spring = spring;
                OpeningVent.Play();
                OpenedVent.Play();
            }
            O2 += Time.deltaTime * 6f;
        }
            
        if (position == 0)
        {
            WalkingVentAud.Stop();
            x = true;
        }

        if (position == 1)
        {
            if (x)
            {
                x = false;
                WalkingVentAud.Play();
            }
        }

        if (position == 2)
        {
            WalkingVentAud.Stop();
            if (!VentState)
            {
                position = 0;
                Thud.Play();
            }
            else
            {
                StartCoroutine(Death());
            }
        }
    }

    IEnumerator Death()
    {
        RobotA.SetBool("Attack", true);
        yield return new WaitForSeconds(2.6f);
        enemyAI.enabled = true;
        agent.speed = 5;
        agent.angularSpeed = 1200;
        agent.acceleration = 40;
    }
    public GameObject o2blacken, o2Visuals;
    IEnumerator DeathFromO2()
    {
        FindObjectOfType<AudioManager>().Play("o2d");
        o2blacken.SetActive(true);
        o2Visuals.SetActive(false);
        yield return new WaitForSeconds(2.6f);
        SceneManager.LoadScene("GameOver");
    }
}