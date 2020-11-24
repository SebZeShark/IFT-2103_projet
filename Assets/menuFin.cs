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

    public void onClicNon()
    {
        PlayerPrefs.DeleteAll(); // Pour être sur que les paramètres de touches redeviennent normaux en fermant l'application
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        textFin.text = "Fuck you " + PlayerPrefs.GetString("Perdant") + ", t'as perdu parce que t'es mauvais!\nTu veux rejouer ? ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
