using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject PlayInOneSittingWarning;
    public GameObject CameraBlocker;
    public float fadeSpeed = 1f;

    private Material cameraFadeMat;
    private bool StartFading = false;

    private void Awake() => cameraFadeMat = CameraBlocker.GetComponent<Renderer>().material;

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        PlayInOneSittingWarning.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(Intro());
    }
    void Update()
    {
        if (StartFading)
        {
            var fadeValue = Mathf.MoveTowards(cameraFadeMat.GetFloat("_Alpha_Value"), 1f, Time.deltaTime * fadeSpeed);
            cameraFadeMat.SetFloat("_Alpha_Value", fadeValue);
        }
    }
    IEnumerator Intro()
    {
        StartFading = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }

    public void MM()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
