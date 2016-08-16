using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlls : MonoBehaviour {

    public float pointCounter = 0;

    public bool isDead = false;
    public bool autoMove = true;
    public bool moveRight = true;
    public bool isHurtable = true;
    private bool isPaused;

    public float timeNotHurtable = 2; // how long the player is invincible after a hit in seconds
    public float autoscrollSpeed = 0.5f; //units per second
    public float jumpPower = 45000;
    public float maxYSpeed = 5;
    public int upgradeCounts = 0;
    public int maxUpgrades = 20;
    public int lives;
    public int maxLives = 10;
    public int collectedMoney = 0;
    public float extraDamage = 0;

    public GameObject weapon;
    private IWeapon weapScript;

    public AudioClip jumpingSound;
    public AudioClip deathSound;
    public AudioClip hitSound;

    // Use this for initialization
    void Start() {
        isPaused = false;
        weapScript = weapon.GetComponent<IWeapon>();
        extraDamage = PlayerPrefs.GetFloat("increasedPower");
    }

    // Put Physics related shit here
    void FixedUpdate() {
        handleAutoMove();

        if (GetComponent<Rigidbody2D>().velocity.y > maxYSpeed) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, maxYSpeed);
        } else if (GetComponent<Rigidbody2D>().velocity.y < -1.5 * maxYSpeed) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -1.5f * maxYSpeed);
        }
    }

    // Update is called once per frame
    void Update() {

        if (!isDead) {
            handleInput();
        }
        timeNotHurtable -= Time.deltaTime;

        if (timeNotHurtable <= 0) {
            isHurtable = true;
            GetComponent<Animator>().SetBool("isHurtable", true);
        }
        upgradeCounts = weapScript.getPowerUpCounts();
    }

    //handles all input
    void handleInput() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)) {
            jump(jumpPower);
        }
        if (Input.GetMouseButton(0) && !isPaused) {
            weapScript.fire(extraDamage);
        }
    }

    // handles if there is autoscrolling and how it works
    void handleAutoMove() {
        if (autoMove && moveRight) {
            transform.Translate(new Vector3(autoscrollSpeed * Time.deltaTime, 0, 0));
            transform.localScale = new Vector3(transform.localScale.y, 1, transform.localScale.z);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(autoscrollSpeed, GetComponent<Rigidbody2D>().velocity.y);
        } else if (autoMove && !moveRight) {
            transform.Translate(new Vector3(-autoscrollSpeed * Time.deltaTime, 0, 0));
            transform.localScale = new Vector3(transform.localScale.y, -1, transform.localScale.z);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(-autoscrollSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void unfreezeAllAxis() {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    void jump(float power) {
        GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1 * maxYSpeed);
        AudioSource.PlayClipAtPoint(jumpingSound, this.transform.position);
    }

    void reduceLive(int amount) {
        if (isHurtable) {
            lives -= amount;
            GetComponent<Animator>().SetTrigger("hurt");
            GetComponent<Animator>().SetBool("isHurtable", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if (lives <= 0) {
                GetComponent<Animator>().SetBool("dead", true);
                isDead = true;
                GameObject.FindGameObjectWithTag("GM").GetComponent<GameMasterScript>().pauseTheGame();
                GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManagerScript>().safePlayerState();
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            } else {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }
            timeNotHurtable = 2;
            isHurtable = false;
        }
    }

    public void increaseLive(int amount) {
        if (lives < maxLives) {
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

    //increases the points by number x
    public void increasePointCounter(float x) {
        pointCounter += x;
    }

    public float getPointCounter() {
        return pointCounter;
    }

    //returns true if the operation was succesfull and false otherwise
    public bool setWeapon(GameObject w) {
        if (w.GetComponent<IWeapon>() == null) {
            Debug.LogError("The weapon you try to attach does not implement the IWeapon.");
                return false;
        } else {

            weapon = w;
            weapScript = weapon.GetComponent<IWeapon>();
            return true;
        }
    }

    public BulletType getWeaponType() {
        return weapScript.getBulletType();
    }

    public void increaseMoney(int amount){
        collectedMoney += amount;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "FlappyObsticle" || other.tag == "Enemy") {
            reduceLive(1);
        }
        if (other.tag == "Counter" && !isDead) {
            pointCounter++;
        }

    }
}
/*
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
    public float laserPower = 15f;
    public int upgradeCounter = 1;
    public int laserCounter = 1;
    public int lives;
    public int maxLives = 10;

    private float fireRateTimer;
    public GameObject rocket;
    public Button PlayAgainButton;
    public Button MainManuButton;
    public BulletType bulletType;

    Vector2 target;
    Vector2 myPos;
    Vector2 direction;
    RaycastHit2D hit;
    LineRenderer lineRenderer;
    public Material material;

    public AudioClip jumpingSound;
    //Score
    public int counter = 0;

    // Use this for initialization
    void Start() {
        PlayAgainButton.gameObject.SetActive(false);
        MainManuButton.gameObject.SetActive(false);
        basicFirerate = firerate;
        fireRateTimer = firerate;
        isPaused = false;
        target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        myPos = new Vector2(transform.position.x + 1, transform.position.y);
        direction = target - myPos;
        direction.Normalize();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(2);
        
        lineRenderer.SetWidth(0.1f, 0.1f);
        lineRenderer.material = material;
        lineRenderer.sortingOrder = -1;
        


    }
    // Put Physics related shit here
    void FixedUpdate() {
        handleAutoMove();

        if (GetComponent<Rigidbody2D>().velocity.y > maxYSpeed) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, maxYSpeed);
        } else if (GetComponent<Rigidbody2D>().velocity.y < -1.5 * maxYSpeed) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -1.5f * maxYSpeed);
        }

        target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        myPos = new Vector2(transform.position.x + 0.4f, transform.position.y);
        direction = target - myPos;
        direction.Normalize();
        hit = Physics2D.Raycast(myPos, direction, 20f);


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
        if (Input.GetMouseButton(0) && !isPaused) {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            lineRenderer.enabled = false;
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            switch (bulletType) {
                case BulletType.CLASSICBULLET:
                    if (fireRateTimer <= 0) {
                        GameObject classicProjectile = (GameObject)Instantiate(rocket, myPos, rotation);
                        classicProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                        fireRateTimer = firerate;
                    }
                    break;

                case BulletType.LASER:
                    if (hit.collider != null) {
                        if (hit.collider.tag == "Enemy" || hit.collider.tag == "FlappyObsticle" || hit.collider.tag == "Player") {
                            if (hit.collider.tag == "Enemy") {
                                hit.transform.SendMessage("getDamage", laserPower * Time.deltaTime);
                            }
                            Vector3[] points = { this.myPos, hit.point };
                            lineRenderer.SetPositions(points);

                        } else {
                            Vector3[] points = { this.myPos, (this.myPos + (direction * 20)) };
                            lineRenderer.SetPositions(points);
                        }
                    } else {
                        Vector3[] points = { this.myPos, (this.myPos + (direction * 20)) };
                        lineRenderer.SetPositions(points);
                    }
                    lineRenderer.enabled = true;
                    break;
                default:
                    Debug.Log("Not Yet Implemented");
                    break;
            }
        } else {
            lineRenderer.enabled = false;
        }
    }
    // handles if there is autoscrolling and how it works
    void handleAutoMove() {
        if (autoMove && moveRight) {
            transform.Translate(new Vector3(autoscrollSpeed * Time.deltaTime, 0, 0));
            transform.localScale = new Vector3(transform.localScale.y, 1, transform.localScale.z);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(autoscrollSpeed, GetComponent<Rigidbody2D>().velocity.y);
        } else if (autoMove && !moveRight) {
            transform.Translate(new Vector3(-autoscrollSpeed * Time.deltaTime, 0, 0));
            transform.localScale = new Vector3(transform.localScale.y, -1, transform.localScale.z);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(-autoscrollSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void unfreezeAllAxis() {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    void jump(float power) {
        GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1 * maxYSpeed);
        AudioSource.PlayClipAtPoint(jumpingSound, this.transform.position);
    }


    void reduceLive(int amount) {
        if (isHurtable) {
            lives -= amount;
            GetComponent<Animator>().SetTrigger("hurt");
            GetComponent<Animator>().SetBool("isHurtable", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
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
        if (lives < maxLives) {
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
            upgradeCounter++;
        }
    }

    public void increaseLaserPower(float f) {
        if(laserCounter < 20) {
            laserPower = laserPower * f;
            laserCounter++;
        }
    }

    //increases the points by number x
    public void increaseCounter(int x) {
        counter += x;
    }

    public int getCounter() {
        return counter;
    }

    public void setWeapon(BulletType weapon) {
        bulletType = weapon;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "FlappyObsticle" || other.tag == "Enemy") {
            reduceLive(1);
        }
        if (other.tag == "Counter" && !isDead) {
            counter++;
        }

    }
}
*/
