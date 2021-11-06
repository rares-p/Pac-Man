using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathSound : MonoBehaviour {
    
	void Start ()
    {
        this.GetComponent<AudioSource>().Play();
	}
	
	void Update ()
    {
		
	}
}
