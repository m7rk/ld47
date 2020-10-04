using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introProgess : MonoBehaviour
{
	private int counter=1;
    public Sprite[] intro;
	public Sprite menu;
	
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
			if(this.GetComponent<SpriteRenderer>().sprite != menu)
			{
				if(counter==3)
					this.GetComponent<SpriteRenderer>().sprite = menu;
				for(int i=0; i < intro.Length; i++)
				{
					if(i==counter)
						this.GetComponent<SpriteRenderer>().sprite = intro[i];
				}
				counter++;
			}
			else
			{
				this.GetComponent<SpriteRenderer>().sprite = intro[0];
				counter=1;
			}
		}
    }
}
