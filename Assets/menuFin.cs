using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuFin : MonoBehaviour
{
    public Text textFin;

    public void onClicOui()
    {
        SceneManager.LoadScene("Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        textFin.text = "Le joueur gagnant est " + PlayerPrefs.GetString("Gagnant") + "!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
