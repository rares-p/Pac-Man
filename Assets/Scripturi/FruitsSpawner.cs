using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsSpawner : MonoBehaviour {

    public Sprite[] fruits;

    public int index;

    void Start ()
    {

	}
	
	void Update ()
    {
        var boxCollider2D = GetComponent<BoxCollider2D>() as BoxCollider2D;
        if (Input.GetKeyDown("g"))
        {
            index++;
            if (index > 6)
                index = 0;
            if (index == 0)
            {
                this.transform.localScale = new Vector3(0.02f, 0.02f, 0f);
                boxCollider2D.size = new Vector2(6.25f, 5.7f);
            }
            else
            {
                this.transform.localScale = new Vector3(0.07f, 0.07f, 0f);
                boxCollider2D.size = new Vector2(2.5f, 2.5f);
            }
            GetComponent<SpriteRenderer>().sprite = fruits[index];
        }
    }
}
