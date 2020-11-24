using System;
using UnityEngine;

public class physics : MonoBehaviour
{
    Vector3 a;
    public Vector3 speed;
    private int hitCounter = 3;

    // Update is called once per frame
    void Update()
    {
        // Change la position selon la vitesse et le vecteur d'accélération
        transform.position += speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        speed = Vector3.Reflect(speed, other.contacts[0].normal);
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        hitCounter--;
        if (hitCounter == 0)
        {
            Destroy(gameObject);
        }
    }
}
