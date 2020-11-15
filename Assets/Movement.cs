using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public InputAction m_movement;
    public InputAction m_rotation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Deplacement vers l'avant

        Vector3 direction = transform.forward * m_movement.ReadValue<float>();
        Vector3 velocity = direction * 15;
        transform.position += velocity * Time.deltaTime;

        transform.rotation *= new Quaternion(0f, 2f * Time.deltaTime * m_rotation.ReadValue<float>(), 0f, 1f);

    }

    public void OnEnable()
    {
        m_movement.Enable();
        m_rotation.Enable();
    }
    public void OnDisable()
    {
        m_movement.Disable();
        m_rotation.Disable();
    }

}
