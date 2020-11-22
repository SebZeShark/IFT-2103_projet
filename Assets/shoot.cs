using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shoot : MonoBehaviour
{
    InputAction m_tirer;
    public InputActionAsset settings;
    public GameObject m_boulet;
    public string player;

    // Start is called before the first frame update
    void Start()
    {
        m_tirer = settings.FindActionMap(player).FindAction("Fire");
        m_tirer.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_tirer.triggered)
        {
            m_boulet.GetComponent<physics>().speed = transform.forward*20f;
            Instantiate(m_boulet, 
                transform.position + transform.forward, 
                transform.rotation);
        }
    }
    
    public void OnDisable()
    {
        m_tirer.Disable();
    }
}
