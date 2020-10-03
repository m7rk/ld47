using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introProgess : MonoBehaviour
{
    public Sprite intro1, intro2, intro3, menu;
	
	// Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        //checking for mouse imput
		if (Input.GetMouseButtonDown(0))
		{
			//somehow change the sprite
			this.GetComponent<SpriteRenderer>().sprite = intro2;
		}
    }
}
