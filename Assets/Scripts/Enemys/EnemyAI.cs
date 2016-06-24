using UnityEngine;
using System.Collections;

public enum AIType{
    CHASE,DISTANCEANDSHOOT
}
public class EnemyAI : MonoBehaviour {
    public AIType ai;
    private GameObject player;
    public GameObject attackSpeedBuff;
    public GameObject onDeathParticles;
    public AudioClip onDeathSound;
    public AudioClip[] idleSound;
    public bool isPause;
    public GameObject[] dropableItems;
    [Tooltip("The array size always has to be the same as the dropableItems array size ")]
    [Range(0f, 1f)]
    public float[] dropChance; //the dropChances for the dropableItems
    public float speed = 1.5f;
    public float live = 20;
    public int pointsWorth = 1;
    public float soundTimer;

    //DISTANCEANDSHOOT variablen
    public float distanceToPlayer = 2f;
    public float fireRate = 0.5f; //per second
    private float fireRateTimer = 0;
    private bool moveUp = true;
    public GameObject shot;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        live = live + (live * player.GetComponent<PlayerControlls>().getPointCounter()/100f);
        isPause = false;
        soundTimer = Random.Range(3f, 8f);
    }

    // Update is called once per frame
    void Update() {
        if (!isPause) {
            soundTimer -= Time.deltaTime;

            switch (ai) {
                case AIType.CHASE:
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                    neutralizeVelocity();
                    break;


                case AIType.DISTANCEANDSHOOT:
                    fireRateTimer -= Time.deltaTime;
                    if (transform.position.x <= player.transform.position.x + distanceToPlayer) {
                        transform.position = new Vector3(player.transform.position.x + distanceToPlayer,transform.position.y,transform.position.z);
                    }
                    if (moveUp) {
                        if (transform.position.y <= 2) {
                            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
                        } else {
                            moveUp = !moveUp;
                        }
                    } else {
                        if (transform.position.y >= -2) {
                            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
                        } else {
                            moveUp = !moveUp;
                        }
                    }
                    if (fireRateTimer <= 0) {
                        fireRateTimer = fireRate;
                        Instantiate(shot, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0, 0, 0, 0));
                    }
                    break;


                default:
                    Debug.LogWarning("Unknown AI Type");
                    break;
            }

            if (soundTimer <= 0) {
                soundTimer = Random.Range(3f, 8f);
                int r = Random.Range(0, idleSound.Length);
                AudioSource.PlayClipAtPoint(idleSound[r], transform.position);
            }
        }
    }


     
    public void getDamage(float dmg) {
        this.live -= dmg;
        if (live <= 0) {
            Instantiate(onDeathParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            AudioSource.PlayClipAtPoint(onDeathSound, transform.position);
            int i = 0;
            foreach(GameObject o in dropableItems) {
                if(Random.value < dropChance[i]) {
                    Instantiate(o, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0, 0, 0, 0));
                }
                i++;
            }
            player.SendMessage("increasePointCounter", pointsWorth);
            Destroy(this.gameObject);
        }
    }

    public void neutralizeVelocity() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.MoveTowards(GetComponent<Rigidbody2D>().velocity.x, 0, 1 * Time.deltaTime), Mathf.MoveTowards(GetComponent<Rigidbody2D>().velocity.y, 0, 1 * Time.deltaTime));
    }

    public void pause() {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        isPause = true;
    }

    public void unPause() {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        isPause = false;
    }

    public int getWeaponPowerupCounter() {


        return 0;
    }

    void OnCollisionStay2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(150, 10.5f));
            player.SendMessage("reduceLive", 1);
        }
    }
}
