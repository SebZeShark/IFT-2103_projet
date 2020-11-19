using System;
using UnityEngine;

public class physics : MonoBehaviour
{
    Vector3 a;
    public Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        a.Set(0f, -9.81f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Change la position selon la vitesse et le vecteur d'accélération
        transform.position = transform.position + speed * (float)Time.deltaTime + (0.5f * (a) * (float)Math.Pow(Time.deltaTime, 2));
        // Met à jour la nouvelle vitesse
        speed = speed + ((a) * Time.deltaTime);
    }
}
