using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gestionVie : MonoBehaviour
{
    public int life;
    public string color;
    public GameObject joueur2;
    public Text t_vie;

    // Start is called before the first frame update
    void Start()
    {
        t_vie.text = life.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
            PlayerPrefs.SetString("Perdant", color);
            PlayerPrefs.SetString("Gagnant", joueur2.GetComponent<gestionVie>().color);
            Debug.Log("Le joueur " + joueur2.GetComponent<gestionVie>().color + " a gagné!");
            SceneManager.LoadScene("FinDePartie");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("projectile"))
        {
            life -= 1;
            t_vie.text = life.ToString();
        }
    }
}
