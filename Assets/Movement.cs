using System;
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
    public LayerMask WallMask;

    // Start is called before the first frame update
    void Start()
    {
        m_movement = settings.FindActionMap(player).FindAction("Avancer");
        var negative = m_movement.bindings.IndexOf(b => b.name == "negative");
        var positive = m_movement.bindings.IndexOf(b => b.name == "positive");

        string[] actions = { "Avancer", "Tourner" };
        string[] axes = { "negative", "positive" };

        foreach (string action in actions)
        {
            foreach (string axe in axes)
            {
                string overBind = PlayerPrefs.GetString(player + action + axe);
                if (overBind != "")
                {
                    var act = settings.FindActionMap(player).FindAction(action);
                    act.ApplyBindingOverride(act.bindings.IndexOf(b => b.name == axe), overBind);
                }
            }
        }


        m_rotation = settings.FindActionMap(player).FindAction("Tourner");

        m_movement.Enable();
        m_rotation.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // Deplacement vers l'avant

        Vector3 direction = transform.forward * m_movement.ReadValue<float>();
        Vector3 velocity = direction * 5;
        if (m_rotation.ReadValue<float>() == 0)
        {
            transform.position += velocity * Time.deltaTime;
            if (Physics.CheckSphere(transform.position, 0.75f, WallMask))
            {
                transform.position -= velocity * Time.deltaTime;
            }
        }
        transform.rotation *= new Quaternion(0f, Time.deltaTime * m_rotation.ReadValue<float>(), 0f, 1f);

    }

    public void OnDisable()
    {
        m_movement.Disable();
        m_rotation.Disable();
    }
}
