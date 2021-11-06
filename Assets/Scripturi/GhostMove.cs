using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 5f;

    public char dirCurr;

    public char dirNext;

    public GameObject inter;

    public bool allow_Up;

    public bool allow_Left;

    public bool allow_Down;

    public bool allow_Right;

    public int randDir;

    public int directions;

    public int[] dirVector;

    public Sprite[] rotatii;

    public GameObject player;

    public bool invulnerable;

    public float time;

    private Animator anim;

    public Vector2 move;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (this.gameObject.name == "Inky")
            move = Vector2.right;
        else if (this.gameObject.name == "Clyde")
            move = Vector2.left;
        else
            move = Vector2.up;
    }

    void FixedUpdate()
    {
        invulnerable = player.GetComponent<Movement>().invulnerable;
        time = player.GetComponent<Movement>().time;

        if (invulnerable && (Time.time - time >= 5f && Time.time - time < 6f))
        {
            anim.enabled = true;
        }

        if (invulnerable)
        {
            this.GetComponent<SpriteRenderer>().sprite = rotatii[4];
            speed = 0.2f;
        }
        else
        {
            speed = 0.5f;

            anim.enabled = false;

            switch (dirCurr)
            {
                case 'w': this.GetComponent<SpriteRenderer>().sprite = rotatii[0]; break;
                case 'a': this.GetComponent<SpriteRenderer>().sprite = rotatii[1]; break;
                case 's': this.GetComponent<SpriteRenderer>().sprite = rotatii[2]; break;
                case 'd': this.GetComponent<SpriteRenderer>().sprite = rotatii[3]; break;
            }
        }
        
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);

        if (this.transform.position.x > 1.08f)
        {
            this.transform.position = new Vector3(-1f, this.transform.position.y, 0f);
        }
        else if (this.transform.position.x < -1.08f)
        {
            this.transform.position = new Vector3(1f, this.transform.position.y, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Intersection")
        {
            var intersection = col.gameObject.GetComponent<Intersection>();

            allow_Up = intersection.allowUp;
            allow_Left = intersection.allowLeft;
            allow_Down = intersection.allowDown;
            allow_Right = intersection.allowRight;

            for (int i = 1; i < 5; i++)
                dirVector[i] = 0;

            directions = 1;

            if (allow_Up)
            {
                directions++;
                dirVector[1] = 1;
            }
            if (allow_Left)
            {
                directions++;
                dirVector[2] = 1;
            }
            if (allow_Down)
            {
                directions++;
                dirVector[3] = 1;
            }
            if (allow_Right)
            {
                directions++;
                dirVector[4] = 1;
            }

            randDir = Random.Range(1, directions);

            for (int index = 0, i = 1; index < randDir; i++)
            {
                if (dirVector[i] == 1)
                    index++;
                if (index == randDir)
                {
                    switch (i)
                    {
                        case 1: dirNext = 'w'; break;
                        case 2: dirNext = 'a'; break;
                        case 3: dirNext = 's'; break;
                        case 4: dirNext = 'd'; break;
                        default: break;
                    }
                }
            }

            this.transform.position = col.gameObject.transform.position;

            if (dirCurr != dirNext)
            {
                if (dirNext == 'w' && intersection.allowUp)
                {
                    dirCurr = dirNext;
                    move = Vector2.up;
                }
                else if (dirNext == 'a' && intersection.allowLeft)
                {
                    dirCurr = dirNext;
                    move = Vector2.left;
                }
                else if (dirNext == 's' && intersection.allowDown)
                {
                    dirCurr = dirNext;
                    move = Vector2.down;
                }
                else if (dirNext == 'd' && intersection.allowRight)
                {
                    dirCurr = dirNext;
                    move = Vector2.right;
                }
            }
        }
        else if (col.gameObject.tag == "IntersectionGhost")
        {
            this.transform.position = col.gameObject.transform.position;

            // left or right
            int a = Random.Range(1, 3);
            if (a == 1)
            { move = Vector2.left; dirCurr = 'a'; }
            else
            { move = Vector2.right; dirCurr = 'd'; }
        }
    }
}
