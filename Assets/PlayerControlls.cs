using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlls : MonoBehaviour {

    public bool autoMove = true;
    public bool moveRight = true;
    public bool isDead = false;
    public bool isHurtable = true;

    public float timeNotHurtable = 2; // how long the player is invincible after a hit in seconds
    public float autoscrollSpeed = 0.5f; //units per second
    public float jumpPower = 45000;
    public float maxYSpeed = 5;
    public float projectileSpeed = 10;
    public float firerate = 0.8f; // Firerate in seconds
    public int lives;

    private float fireRateTimer;
    public GameObject bullet;
    public Button PlayAgainButton;

    public AudioClip jumpingSound;

    // Use this for initialization
    void Start () {
        PlayAgainButton.gameObject.SetActive(false);
        Invoke("unfreezeAllAxis", 5);
        lives = 3;
        fireRateTimer = firerate;
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

        if (Input.GetMouseButton(0) && fireRateTimer <= 0) {
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
        } else if (autoMove && !moveRight) {
            transform.Translate(Vector3.left * autoscrollSpeed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.y, -1, transform.localScale.z);
        }
    }

    void unfreezeAllAxis() {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    void jump(float power) {
        GetComponent<Rigidbody2D>().velocity = new Vector3( 0, 1 * maxYSpeed, 0);
        AudioSource.PlayClipAtPoint(jumpingSound, this.transform.position);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "FlappyObsticle" || other.tag == "Enemy") {
            reduceLive(1);
        }
    }

    void reduceLive(int amount) {
        if (isHurtable) {
            lives -= amount;
            GetComponent<Animator>().SetTrigger("hurt");
            GetComponent<Animator>().SetBool("isHurtable", false);
            if (lives <= 0) {
                GetComponent<Animator>().SetBool("dead", true);
                isDead = true;
                PlayAgainButton.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("GM").GetComponent<GameMasterScript>().pauseTheGame();
            }
            timeNotHurtable = 2;
            isHurtable = false;
        }
    }

    void pause() {
        isHurtable = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        autoMove = false;
    }

    void unPause() {
        isHurtable = true;
        unfreezeAllAxis();
        autoMove = true;
    }
}
