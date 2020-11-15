using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStop : MonoBehaviour
{
	public GameObject cover;
	public GameObject fadein;
	
    // Start is called before the first frame update
    void Start()
    {
        Invoke("hideIntro", 6.5f);
    }

    void hideIntro()
	{
		cover.SetActive(false);
		fadein.SetActive(true);
		Invoke("hideFade", 1f);
	}

	void hideFade()
	{
		fadein.SetActive(false);
	}
}
