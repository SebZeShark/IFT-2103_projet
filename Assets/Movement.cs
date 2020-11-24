using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    InputAction m_movement;
    InputAction m_rotation;
    public InputActionAsset settings;
    public string player;

    // Start is called before the first frame update
    void Start()
    {
        m_movement = settings.FindActionMap(player).FindAction("Avancer");
        m_movement.Enable();
        m_rotation = settings.FindActionMap(player).FindAction("Tourner");
        m_rotation.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // Deplacement vers l'avant

        Vector3 direction = transform.forward * m_movement.ReadValue<float>();
        Vector3 velocity = direction * 5;
        transform.position += velocity * Time.deltaTime;

        transform.rotation *= new Quaternion(0f, 2f * Time.deltaTime * m_rotation.ReadValue<float>(), 0f, 1f);

    }

    public void OnDisable()
    {
        m_movement.Disable();
        m_rotation.Disable();
    }

}
