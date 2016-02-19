using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlls : MonoBehaviour {

    public bool autoMove = true;
    public bool moveRight = true;
    public bool isDead = false;
    public bool isHurtable = true;
    private bool isPaused;

    public float timeNotHurtable = 2; // how long the player is invincible after a hit in seconds
    public float autoscrollSpeed = 0.5f; //units per second
    public float jumpPower = 45000;
    public float maxYSpeed = 5;
    public float projectileSpeed = 10;
    public float basicFirerate = 0.5f; // Firerate in seconds
    public float firerate;
    public float maxFirerate = 0.1f;

    public int lives;
    public int maxLives = 10;

    private float fireRateTimer;
    public GameObject bullet;
    public Button PlayAgainButton;
    public Button MainManuButton;

    public AudioClip jumpingSound;
    //Score
    public int counter = 0;

    // Use this for initialization
    void Start () {
        PlayAgainButton.gameObject.SetActive(false);
        MainManuButton.gameObject.SetActive(false);
        Invoke("unfreezeAllAxis", 3);
        lives = 3;
        basicFirerate = firerate;
        fireRateTimer = firerate;
        isPaused = false;

    }
	// Put Physics related shit here
    void FixedUpdate() {
        handleAutoMove();

        if(GetComponent<Rigidbody2D>().velocity.y > maxYSpeed) {
            GetComponent<Rigidbody2D>().velocity = Vector3.up * maxYSpeed;
        }else if(GetComponent<Rigidbody2D>().velocity.y < -1.5*maxYSpeed) {
            GetComponent<Rigidbody2D>().velocity = Vector3.up * -1.5f*maxYSpeed;
        }
    }
    // Update is called once per frame
    void Update() {
        if (!isDead) {
            handleInput();
        }
        timeNotHurtable -= Time.deltaTime;
        fireRateTimer -= Time.deltaTime;

        if (timeNotHurtable <= 0) {
            isHurtable = true;
            GetComponent<Animator>().SetBool("isHurtable", true);
        }

        
    }

    //handles all input
    void handleInput() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)) {
            jump(jumpPower);
        }

        if (Input.GetMouseButton(0) && fireRateTimer <= 0 && !isPaused) {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            GameObject projectile = (GameObject)Instantiate(bullet, myPos, rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            fireRateTimer = firerate;
        }
    }
    // handles if there is autoscrolling and how it works
    void handleAutoMove() {
        if (autoMove && moveRight) {
            transform.Translate(Vector3.right * autoscrollSpeed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.y, 1, transform.localScale.z);
            GetComponent<Rigidbody2D>().velocity = new Vector2(1, GetComponent<Rigidbody2D>().velocity.y);
        } else if (autoMove && !moveRight) {
            transform.Translate(Vector3.left * autoscrollSpeed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.y, -1, transform.localScale.z);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void unfreezeAllAxis() {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    void jump(float power) {
        GetComponent<Rigidbody2D>().velocity = new Vector2( 0, 1 * maxYSpeed);
        AudioSource.PlayClipAtPoint(jumpingSound, this.transform.position);
    }


    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "FlappyObsticle" || other.tag == "Enemy") {
            reduceLive(1);
        }
        if (other.tag == "Counter" && !isDead) {
            counter++;
        }

    }

    void reduceLive(int amount) {
        if (isHurtable) {
            lives -= amount;
            GetComponent<Animator>().SetTrigger("hurt");
            GetComponent<Animator>().SetBool("isHurtable", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            if (lives <= 0) {
                GetComponent<Animator>().SetBool("dead", true);
                isDead = true;
                PlayAgainButton.gameObject.SetActive(true);
                MainManuButton.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("GM").GetComponent<GameMasterScript>().pauseTheGame();
            }
            timeNotHurtable = 2;
            isHurtable = false;
        }
    }

    public void increaseLive(int amount) {
        if(lives < maxLives) {
            lives += amount;
        }
    }

    void pause() {
        isPaused = true;
        isHurtable = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        autoMove = false;
    }

    void unPause() {
        isPaused = false;
        isHurtable = true;
        unfreezeAllAxis();
        autoMove = true;
    }

    public void increaseFirerate(float f) {
        if (firerate >= maxFirerate) { 
            firerate = firerate * f;
        }
    }
}
