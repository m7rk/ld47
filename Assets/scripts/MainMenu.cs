using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject blackTransition;

    public void PlayGame ()
	{
        blackTransition.SetActive(true);
        blackTransition.GetComponent<Animator>().SetTrigger("Start");
        Invoke("loadgame", 3f);
        foreach(Button b in FindObjectsOfType<Button>())
        {
            b.interactable = false;
        }
	}

    public void loadgame()
    {
        Utility.level = 1;
        SceneManager.LoadScene("Main");
    }
}
