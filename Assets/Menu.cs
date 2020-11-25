using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayMultijoueur()
    {
        SceneManager.LoadScene("1v1 map");
    }

    public void CloseGame()
    {
        PlayerPrefs.DeleteAll(); // Pour être sur que les paramètres de touches redeviennent normaux en fermant l'application
        Application.Quit();
    }

    public void param()
    {
        SceneManager.LoadScene("parametres");
    }
    
    public void PlaySolo()
    {
        SceneManager.LoadScene("PvAi map");
    }
}
