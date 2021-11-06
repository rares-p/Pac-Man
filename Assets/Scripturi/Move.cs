using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour {

    private Rigidbody2D rb;

    public GameObject Inky;

    public GameObject Pinky;

    public GameObject Blinky;

    public GameObject Clyde;

    public float time;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        time = Time.time;
	}
	
	void FixedUpdate ()
    {
        if (Time.time - time > 3.5f)
        {
            rb.position = new Vector3(-1f, 0f, 0f);

            Inky.gameObject.SetActive(true);
            Pinky.gameObject.SetActive(true);
            Blinky.gameObject.SetActive(true);
            Clyde.gameObject.SetActive(true);

            time = Time.time;
        }

        rb.velocity = new Vector2(30f, 0f) * Time.fixedDeltaTime;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.SetActive(false);
    }
}
