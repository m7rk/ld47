using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
	{
        Invoke("loadgame", 4f);
	}

    public void loadgame()
    {
        Utility.level = 1;
        SceneManager.LoadScene("Main");
    }
}
