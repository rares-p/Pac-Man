using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wait : MonoBehaviour {

    public Text press;

    private void Awake()
    {
        this.GetComponent<AudioSource>().Play();
        Time.timeScale = 0f;
    }

    void Start ()
    {

	}
	
	void Update ()
    {
        if (Input.anyKey && !this.GetComponent<AudioSource>().isPlaying)
        {
            press.gameObject.SetActive(false);
            Time.timeScale = 1f;
            this.gameObject.SetActive(false);
        }
	}
}
