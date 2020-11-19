using System.Collections;
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
            Vector3 v3 = new Vector3(0f, 0f, 40f);
            v3 = Quaternion.Euler(-20f, 0f, 0f) * v3;
            v3 = Quaternion.Euler(transform.rotation.eulerAngles) * v3;
            m_boulet.GetComponent<physics>().speed = v3;
            Instantiate(m_boulet, 
                transform.position + (Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(0, 0.5f, 0.5f)), 
                transform.rotation);

            Debug.Log("y " + transform.rotation.y);
            Debug.Log("w " + transform.rotation.w);
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
