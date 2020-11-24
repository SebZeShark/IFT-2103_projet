using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    private float rotationSpeed = 4f;
    public GameObject player;
    
    private Quaternion _lookRotation;
    private Vector3 _direction;
    
    public GameObject m_boulet;

    private float shootTimer;
    public float shootColldown = 2f;
    private bool shotReady = true;
    void Start()
    {
        player = GameObject.Find("Joueur 1");
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
        TravelToPoint(player.transform.position);
        
    }

    private void TravelToPoint(Vector3 travelPoint)
    {
        _direction = (travelPoint - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
        transform.position += transform.forward * 5 * Time.deltaTime;
    }

    private void RotateToPoint(Vector3 travelPoint)
    {
        _direction = (travelPoint - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void Shoot()
    {
        m_boulet.GetComponent<physics>().speed = transform.forward*20f;
        Instantiate(m_boulet, 
            transform.position + transform.forward, 
            transform.rotation);
        shotReady = false;
        shootTimer = 0;
    }
}
