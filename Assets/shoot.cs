﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shoot : MonoBehaviour
{
    public InputAction m_tirer;
    public GameObject m_boulet;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void OnEnable()
    {
        m_tirer.Enable();
    }
    public void OnDisable()
    {
        m_tirer.Disable();
    }
}
