using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public void LoadMap()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
