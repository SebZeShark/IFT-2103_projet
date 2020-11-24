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
    private float shootTimer;
    public float shootColldown = 2f;
    private bool shotReady = true;

    // Start is called before the first frame update
    void Start()
    {
        m_tirer = settings.FindActionMap(player).FindAction("Fire");
        if (PlayerPrefs.GetString("Fire" + player) != "")
            m_tirer.ApplyBindingOverride(PlayerPrefs.GetString("Fire" + player));
        m_tirer.Enable();
        shootTimer = shootColldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shotReady)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootColldown)
            {
                shotReady = true;
            }
        }
        if (m_tirer.triggered && shotReady)
        {
            m_boulet.GetComponent<physics>().speed = transform.forward*20f;
            Instantiate(m_boulet, 
                transform.position + transform.forward, 
                transform.rotation);
            shootTimer = 0f;
            shotReady = false;
        }
    }
    
    public void OnDisable()
    {
        m_tirer.Disable();
    }
}
