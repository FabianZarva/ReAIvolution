using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class PICO_ButtonInputs : MonoBehaviour
{
    [Header("InputAction Components")]
    public InputActionReference incrementButton;
    public InputActionReference decrementButton;
    public InputActionReference menuButton;
    public InputActionReference Move;

    [Header("Game Components")]
    public GameObject menuWindow;
    public GameObject T1, T2;
    public XRRayInteractor XRRayInteractor1, XRRayInteractor2;


    // Start is called before the first frame update
    void Start()
    {
        incrementButton.action.started += DoIncrement;
        decrementButton.action.started += DoDecrement;
        menuButton.action.started += MenuToggle;
        Move.action.started += DisText;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        incrementButton.action.started -= DoIncrement;
        decrementButton.action.started -= DoDecrement;
        menuButton.action.started -= MenuToggle;
        Move.action.started -= DisText;
    }

    private void DoIncrement(InputAction.CallbackContext obj)
    {
        // B to do something
    }

    private void DoDecrement(InputAction.CallbackContext obj)
    {
        Skipping = !Skipping;
        AtoSkip.SetActive(!AtoSkip.activeSelf);
        AtoCancel.SetActive(!AtoCancel.activeSelf);
    }

    private void MenuToggle(InputAction.CallbackContext obj)
    {
        if (menuWindow.activeSelf == false)
        {
            menuWindow.SetActive(true);
            XRRayInteractor1.enabled = true;
            XRRayInteractor2.enabled = true;
            AudioListener.pause = true;
            Time.timeScale = 0f;
        }
        else
        {
            menuWindow.SetActive(false);
            XRRayInteractor1.enabled = false;
            XRRayInteractor2.enabled = false;
            AudioListener.pause = false;
            Time.timeScale = 1f;
        }
    }
    public void MenuToggle2()
    {
        if (menuWindow.activeSelf == false)
        {
            menuWindow.SetActive(true);
            AudioListener.pause = true;
            XRRayInteractor1.enabled = true;
            XRRayInteractor2.enabled = true;
            Time.timeScale = 0f;
        }
        else
        {
            menuWindow.SetActive(false);
            AudioListener.pause = false;
            XRRayInteractor1.enabled = false;
            XRRayInteractor2.enabled = false;
            Time.timeScale = 1f;
        }
    }

    private void DisText(InputAction.CallbackContext obj)
    {
        T1.SetActive(false);
        T2.SetActive(false);
    }

    public bool Skipping;
    public Image fillImage;
    public float fillSpeed = 0.5f;

    private bool isFilling = false;
    private float fillAmount = 0f;
    public GameObject TutorialOver;
    public GameObject AtoSkip;
    public GameObject AtoCancel;

    public void Update()
    {
        if (Skipping)
        {
            isFilling = true;
            fillAmount += Time.deltaTime * fillSpeed;

            if (fillAmount >= 1f)
            {
                fillAmount = 0f;
                TutorialOver.SetActive(false);
            }
        }
        else
        {
            isFilling = false;
            fillAmount = 0f;
        }

        fillImage.fillAmount = isFilling ? fillAmount : 0f;
    }
}
