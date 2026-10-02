using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public Animator BotClean, TVScreenA;
    public Animator BotChase;
    public DynamicMoveProvider dynamicMoveProvider;
    public GameObject OfflineT1, OfflineT2, OnlineT1, OnlineT2;
    public GameObject T1, T2;
    public GameObject ti1, ti2, ti3, ti4, tti1, tti2, tti3, tti21, tti22, tti23, WarnT, WarnT2;
    public Light CeilingL1, CeilingL2, CeilingL3;
    public Collider BSlotC, BSlotC2;

    public VentMechanic ventMechanic;
    public FloorMechanic floorMechanic;

    public List<Transform> teleportLocations;
    public List<Transform> teleportLocations2;
    public List<Transform> teleportLocations3;
    public GameObject ScrewDriver;
    public GameObject Battery, BatteryInSlot;
    public GameObject USB, USBinSlot;
    public GameObject Skipping;
    public GameObject Images1, Images2;

    public Animator FadeOut;


    IEnumerator Start()
    {
        Teleport();
        Teleport2();
        Teleport3();
        yield return new WaitForSeconds(15f);
        BotClean.SetBool("Malfunction", true);
        TVScreenA.enabled = true;

        if (Skipping.activeSelf == true)
        {
            ti1.SetActive(true);
            FindObjectOfType<AudioManager>().Play("SB1");
            yield return new WaitForSeconds(9f);
            ti1.SetActive(false);
        }

        FindObjectOfType<AudioManager>().Play("PowerDown");
        OfflineT1.SetActive(true);
        OfflineT2.SetActive(true);
        OnlineT1.SetActive(false);
        OnlineT2.SetActive(false);
        CeilingL1.color = new Color32(70, 0, 0, 255);
        CeilingL2.color = new Color32(70, 0, 0, 255);
        CeilingL3.color = new Color32(70, 0, 0, 255);

        if (Skipping.activeSelf == true)
        {
            ti2.SetActive(true);
            FindObjectOfType<AudioManager>().Play("SB2");
            yield return new WaitForSeconds(4f);
            ti2.SetActive(false);
        }
        if (Skipping.activeSelf == true)
        {
            ti3.SetActive(true);
            FindObjectOfType<AudioManager>().Play("SB3");
            yield return new WaitForSeconds(9f);
            ti3.SetActive(false);
        }
        if (Skipping.activeSelf == true)
        {
            ti4.SetActive(true);
            FindObjectOfType<AudioManager>().Play("SB4");
            yield return new WaitForSeconds(12f);
        }
        T1.SetActive(true);
        T2.SetActive(true);
        dynamicMoveProvider.enabled = true;
        
        yield return new WaitUntil(() => T1.activeSelf == false);

        ti4.SetActive(false);
        if (Skipping.activeSelf == true)
        {
            tti1.SetActive(true);
            FindObjectOfType<AudioManager>().Play("SBT1");
            yield return new WaitForSeconds(10f);
            tti1.SetActive(false);
        }
        if (Skipping.activeSelf == true)
        {
            tti2.SetActive(true);
            FindObjectOfType<AudioManager>().Play("SBT2");
            yield return new WaitForSeconds(16f);
            tti2.SetActive(false);
        }
        if (Skipping.activeSelf == true)
        {
            tti3.SetActive(true);
            FindObjectOfType<AudioManager>().Play("SBT3");
            yield return new WaitForSeconds(7f);
            tti3.SetActive(false);
        }
        ventMechanic.GameStarted = true;
        Skipping.SetActive(false);
        Images1.SetActive(true);


        yield return new WaitUntil(() => BSlotC.enabled == true);

        Battery.SetActive(true);
        WarnT.SetActive(true);
        tti3.SetActive(false);

        Images1.SetActive(false);
        tti21.SetActive(true);
        FindObjectOfType<AudioManager>().Play("SBT21");
        yield return new WaitForSeconds(8f);
        tti21.SetActive(false);

        tti22.SetActive(true);
        FindObjectOfType<AudioManager>().Play("SBT22");
        yield return new WaitForSeconds(11f);
        tti22.SetActive(false);

        tti23.SetActive(true);
        FindObjectOfType<AudioManager>().Play("SBT23");
        yield return new WaitForSeconds(8.5f);
        tti23.SetActive(false);

        floorMechanic.GameStarted = true;
        Images2.SetActive(true);

        yield return new WaitUntil(() => BatteryInSlot.activeSelf == true);

        FindObjectOfType<AudioManager>().Play("GL");
        Images2.SetActive(false);

        yield return new WaitUntil(() => BSlotC2.enabled == true);

        USB.SetActive(true);
        WarnT2.SetActive(true);

        yield return new WaitUntil(() => USBinSlot.activeSelf == true);

        ventMechanic.GameStarted = false;
        floorMechanic.GameStarted = false;

        yield return new WaitUntil(() => EndGame);

        AIcome.Play();
        SText1.SetActive(false);
        SText2.SetActive(false);
        SText3.SetActive(true);
        SText4.SetActive(true);

        yield return new WaitForSeconds(5f);

        BotChase.SetBool("Attack", true);
        yield return new WaitForSeconds(2.6f);
        FindObjectOfType<AudioManager>().Play("Chase");
        ambience.Stop();
        enemyAI.enabled = true;
        agent.speed = 1.7f;
        agent.angularSpeed = 1200;
        yield return new WaitForSeconds(12f);
        BotChase.SetBool("Shut", true);
        agent.speed = 0;
        yield return new WaitForSeconds(5f);
        FadeOut.enabled = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("End");
    }

    public void EEndGame()
    {
        EndGame = true;
    }
    public bool EndGame;
    public EnemyAI enemyAI;
    public NavMeshAgent agent;
    public GameObject SText1, SText2, SText3, SText4;
    public AudioSource AIcome, ambience;

    void Teleport()
    {
        // Check if there are any teleport locations in the list
        if (teleportLocations.Count > 0)
        {
            // Choose a random index from the list
            int randomIndex = Random.Range(0, teleportLocations.Count);

            // Get the chosen teleport location
            Transform chosenLocation = teleportLocations[randomIndex];

            // Teleport the player to that location
            ScrewDriver.transform.position = chosenLocation.position;
        }
    }

    void Teleport2()
    {
        // Check if there are any teleport locations in the list
        if (teleportLocations2.Count > 0)
        {
            // Choose a random index from the list
            int randomIndex = Random.Range(0, teleportLocations2.Count);

            // Get the chosen teleport location
            Transform chosenLocation = teleportLocations2[randomIndex];

            // Teleport the player to that location
            Battery.transform.position = chosenLocation.position;
        }
    }

    void Teleport3()
    {
        // Check if there are any teleport locations in the list
        if (teleportLocations3.Count > 0)
        {
            // Choose a random index from the list
            int randomIndex = Random.Range(0, teleportLocations3.Count);

            // Get the chosen teleport location
            Transform chosenLocation = teleportLocations3[randomIndex];

            // Teleport the player to that location
            USB.transform.position = chosenLocation.position;
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenu");
    }
}
