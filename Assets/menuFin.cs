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
        textFin.text = "Fuck you " + PlayerPrefs.GetString("Perdant") + ", t'as perdu parce que t'es mauvais!\nTu veux rejouer ? ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
