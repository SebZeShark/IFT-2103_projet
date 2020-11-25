using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuPause : MonoBehaviour
{
    public InputActionAsset settings;
    InputAction a_pause;
    public GameObject p_menuPause;

    // Awake is called first
    void Awake()
    {
        a_pause = settings.FindActionMap("UI").FindAction("Pause");
        a_pause.Enable();
        p_menuPause.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(a_pause.triggered && !p_menuPause.activeInHierarchy)
        {
            Time.timeScale = 0;
            p_menuPause.SetActive(true);
        }
        /*if (a_pause.triggered && p_menuPause.activeInHierarchy)
        {
            onRepprendre();
        }*/
    }

    public void onRepprendre()
    {
        Time.timeScale = 1;
        p_menuPause.SetActive(false);
    }

    public void onQuitter()
    {
        Time.timeScale = 1;
        p_menuPause.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    private void OnDisable()
    {
        a_pause.Disable();
    }
}
