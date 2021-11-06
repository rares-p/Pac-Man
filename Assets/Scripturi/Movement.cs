using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    public Rigidbody2D rb;

    public float speed = 5f;

    public char dirCurr;

    public char dirNext;

    public GameObject inter;

    public bool canMove;

    public bool allow_Up;

    public bool allow_Left;

    public bool allow_Down;

    public bool allow_Right;

    public bool sta;

    public float rotation;

    public Sprite frame;

    private Animator anim;

    public int score;

    public Text scoreText;

    public GameObject fruits;

    public int index;

    public bool invulnerable;

    public float time;

    public GameObject Inky;

    public GameObject Blinky;

    public GameObject Pinky;

    public GameObject Clyde;

    private float ghostPozY = 0.08f;

    private float ghostPozX = 0f;

    public GameObject waitForKey;

    private int timer;

    public int dots;

    private bool setTime;

    public GameObject mazeAnim;

    public GameObject Lives;

    public GameObject cherry;

    private bool Cherry = true;

    private int eatenGhosts = 1;

    [SerializeField]
    int lives = 3;

    [SerializeField]
    Vector2 move;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dirCurr = 'd';
        dirNext = 'd';
        move = Vector2.right;
        canMove = true;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (dots != 244)
        {
            if (Input.GetKeyDown("l") && Input.GetKeyDown("[9]"))
                dots = 244;
            if (dots > 100 & Cherry)
            {
                cherry.SetActive(true);
                Cherry = false;
            }
            if (!invulnerable)
            {
                if (Vector3.Distance(Inky.transform.position, transform.position) < 0.12f || Vector3.Distance(Pinky.transform.position, transform.position) < 0.12f || Vector3.Distance(Blinky.transform.position, transform.position) < 0.12f || Vector3.Distance(Clyde.transform.position, transform.position) < 0.12f)
                {
                    lives--;
                    if(lives == 2)
                        Lives.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    else if(lives == 1)
                        Lives.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    if (lives > 0)
                    {
                        Restart();
                    }
                    else
                        SceneManager.LoadScene(2);
                }
            }
            else
            {
                if (Vector3.Distance(Inky.transform.position, transform.position) < 0.12f)
                {
                    Inky.transform.position = new Vector3(ghostPozX, ghostPozY);
                    Inky.gameObject.GetComponent<GhostMove>().move = Vector2.up;
                    score = score + 200 * eatenGhosts;
                    scoreText.text = score.ToString();
                    this.GetComponent<Sounds>().ghostEatingSound();
                }
                if (Vector3.Distance(Pinky.transform.position, transform.position) < 0.12f)
                {

                    Pinky.transform.position = new Vector3(ghostPozX, ghostPozY);
                    Pinky.gameObject.GetComponent<GhostMove>().move = Vector2.up;
                    score = score + 200 * eatenGhosts;
                    scoreText.text = score.ToString();
                    this.GetComponent<Sounds>().ghostEatingSound();
                }
                if (Vector3.Distance(Blinky.transform.position, transform.position) < 0.12f)
                {

                    Blinky.transform.position = new Vector3(ghostPozX, ghostPozY);
                    Blinky.gameObject.GetComponent<GhostMove>().move = Vector2.up;
                    score = score + 200 * eatenGhosts;
                    scoreText.text = score.ToString();
                    this.GetComponent<Sounds>().ghostEatingSound();
                }
                if (Vector3.Distance(Clyde.transform.position, transform.position) < 0.12f)
                {

                    Clyde.transform.position = new Vector3(ghostPozX, ghostPozY);
                    Clyde.gameObject.GetComponent<GhostMove>().move = Vector2.up;
                    score = score + 200 * eatenGhosts;
                    scoreText.text = score.ToString();
                    this.GetComponent<Sounds>().ghostEatingSound();
                }
            }

            if (sta)
            {
                anim.enabled = !sta;
                GetComponent<SpriteRenderer>().sprite = frame;
            }
            else
                anim.enabled = true;

            switch (dirCurr)
            {
                case 'w': rotation = 90; break;
                case 'a': rotation = 180; break;
                case 's': rotation = 270; break;
                default: rotation = 0; break;
            }

            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = rotation;
            this.transform.rotation = Quaternion.Euler(rotationVector);

            if (canMove == true)
                rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);

            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                if (dirCurr == 's' || sta && allow_Up)
                {
                    move = Vector2.up;
                    canMove = true;
                    dirCurr = 'w';
                    sta = false;
                }
                dirNext = 'w';
            }

            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                if (dirCurr == 'd' || sta && allow_Left)
                {
                    move = Vector2.left;
                    canMove = true;
                    dirCurr = 'a';
                    sta = false;
                }
                dirNext = 'a';
            }

            if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                if (dirCurr == 'w' || sta && allow_Down)
                {
                    move = Vector2.down;
                    canMove = true;
                    dirCurr = 's';
                    sta = false;
                }
                dirNext = 's';
            }

            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                if (dirCurr == 'a' || sta && allow_Right)
                {
                    move = Vector2.right;
                    canMove = true;
                    dirCurr = 'd';
                    sta = false;
                }
                dirNext = 'd';
            }

            if (this.transform.position.x > 1.08f)
            {
                this.transform.position = new Vector3(-1f, this.transform.position.y, 0f);
            }
            else if (this.transform.position.x < -1.08f)
            {
                this.transform.position = new Vector3(1f, this.transform.position.y, 0f);
            }

            if (invulnerable)
            {
                if (time == 0f)
                    time = Time.time;
                if (Time.time - time >= 6f)
                {
                    invulnerable = false;
                    time = 0f;
                    eatenGhosts = 1;
                }
            }
        }
        else
        {
            Inky.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Pinky.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Blinky.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Clyde.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (!setTime)
            {
                time = Time.time;
                setTime = true;
            }
            mazeAnim.SetActive(true);
            if (Time.time - time > 3f)
                SceneManager.LoadScene(3);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Intersection")
        {
            sta = false;

            var intersection = col.gameObject.GetComponent<Intersection>();

            allow_Up = intersection.allowUp;
            allow_Left = intersection.allowLeft;
            allow_Down = intersection.allowDown;
            allow_Right = intersection.allowRight;

            canMove = false;

            this.transform.position = col.gameObject.transform.position;

            if (dirCurr == dirNext)
            {
                if (dirCurr == 'w' && intersection.allowUp)
                    canMove = true;
                else if (dirCurr == 'a' && intersection.allowLeft)
                    canMove = true;
                else if (dirCurr == 's' && intersection.allowDown)
                    canMove = true;
                else if (dirCurr == 'd' && intersection.allowRight)
                    canMove = true;
                else
                    sta = true;
            }
            else
            {
                if (dirNext == 'w' && intersection.allowUp)
                {
                    dirCurr = dirNext;
                    move = Vector2.up;
                    canMove = true;
                }
                else if (dirNext == 'a' && intersection.allowLeft)
                {
                    dirCurr = dirNext;
                    move = Vector2.left;
                    canMove = true;
                }
                else if (dirNext == 's' && intersection.allowDown)
                {
                    dirCurr = dirNext;
                    move = Vector2.down;
                    canMove = true;
                }
                else if (dirNext == 'd' && intersection.allowRight)
                {
                    dirCurr = dirNext;
                    move = Vector2.right;
                    canMove = true;
                }
                else if (dirCurr == 'w' && intersection.allowUp)
                    canMove = true;
                else if (dirCurr == 'a' && intersection.allowLeft)
                    canMove = true;
                else if (dirCurr == 's' && intersection.allowDown)
                    canMove = true;
                else if (dirCurr == 'd' && intersection.allowRight)
                    canMove = true;
                else
                    sta = true;
            }
        }
        else if (col.gameObject.tag == "Dot")
        {
            dots++;
            col.gameObject.SetActive(false);
            score += 10;
            scoreText.text = score.ToString();
        }
        else if (col.gameObject.tag == "Power Pellet")
        {
            col.gameObject.SetActive(false);
            score += 50;
            scoreText.text = score.ToString();

            invulnerable = true;
        }
        /*
        else if (col.gameObject.tag == "Fruit")
        {
            col.gameObject.SetActive(false);

            switch (index)
            {
                case 0: score += 100; break;
                case 1: score += 300; break;
                case 2: score += 500; break;
                case 3: score += 700; break;
                case 4: score += 1000; break;
                case 5: score += 3000; break;
                case 6: score += 5000; break;
                default: break;
            }
            scoreText.text = score.ToString();
            index++;
        }*/
        else if(col.gameObject.tag == "Cherry")
        {
            col.gameObject.SetActive(false);
            score += 200;
            scoreText.text = score.ToString();
        }
    }

    void Restart()
    {
        this.transform.position = new Vector3(0f, -0.64f);
        move = Vector2.right;
        dirCurr = 'd';

        Inky.transform.position = new Vector3(-0.16f, 0.08f);
        Inky.gameObject.GetComponent<GhostMove>().move = Vector2.right;

        Pinky.transform.position = new Vector3(0f, 0.08f);
        Pinky.gameObject.GetComponent<GhostMove>().move = Vector2.up;

        Blinky.transform.position = new Vector3(0f, 0.32f);

        Clyde.transform.position = new Vector3(0.16f, 0.08f);
        Clyde.gameObject.GetComponent<GhostMove>().move = Vector2.left;
    }
}
