using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {

    public AudioClip eating;

    public AudioClip powerPellet;

    public AudioClip eatingGhost;

    public AudioClip beginning;

    public AudioClip fruit;

    private AudioSource audioSource;

	void Start ()
    {
        audioSource = this.GetComponent<AudioSource>();
	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dot")
        {
            //if (!audioSource.isPlaying)
            audioSource.PlayOneShot(eating);
        }
        else if (col.gameObject.tag == "Power Pellet")
        {
            audioSource.PlayOneShot(powerPellet);
        }
        else if (col.gameObject.tag == "Cherry")
            audioSource.PlayOneShot(fruit);

    }

    public void ghostEatingSound()
    {
        audioSource.PlayOneShot(eatingGhost);
    }

    public void startSound()
    {
        audioSource.PlayOneShot(beginning);
    }
}
