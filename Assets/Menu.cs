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
        Application.Quit();
    }

    public void param()
    {
        SceneManager.LoadScene("parametres");
    }
}
