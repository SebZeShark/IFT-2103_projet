using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class scriptAcceuil : MonoBehaviour
{
    public GameObject p_chargement;
    public Text textChargement;
    InputAction keys;
    float chargement;
    bool charging;

    private void Awake()
    {
        p_chargement.SetActive(false);
        chargement = 0;
        keys = new InputAction(binding: "/*/<button>");
        keys.Enable();
        charging = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keys.triggered)
        {
            charging = true;
            p_chargement.SetActive(true);
        }
        if (charging)
        {
            textChargement.text = "Chargement: " 
                + ((int)chargement).ToString() 
                + "%";
            chargement += 0.3141592f;
        }
        if(chargement >= 100f)
            SceneManager.LoadScene("Menu");
    }
}
