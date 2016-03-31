using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    private GameObject player;
    public GameObject attackSpeedBuff;
    public GameObject onDeathParticles;
    public AudioClip onDeathSound;
    public AudioClip[] idleSound;
    public bool isPause;
    public GameObject[] dropableItems;
    public float speed = 1.5f;
    public float live = 20;
    public float dropChance = 0.3f; // dropchance ( Value between 0.0f - 1.0f)
    public int pointsWorth = 1;
    public float soundTimer;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        live = live + (live * player.GetComponent<PlayerControlls>().getCounter()/100f);
        isPause = false;
        soundTimer = Random.Range(3f, 8f);
    }

    // Update is called once per frame
    void Update() {
        soundTimer -= Time.deltaTime;
        if (!isPause) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            neutralizeVelocity();
        }
        if (soundTimer <= 0) {
            soundTimer = Random.Range(3f, 8f);
            int r = Random.Range(0, idleSound.Length);
            Debug.Log(idleSound.Length);
            AudioSource.PlayClipAtPoint(idleSound[r], transform.position);
        }
    }


     
    public void getDamage(float dmg) {
        this.live -= dmg;
        if (live <= 0) {
            Instantiate(onDeathParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0, 0, 180, 0));
            AudioSource.PlayClipAtPoint(onDeathSound, transform.position);
            foreach(GameObject o in dropableItems) {
                if(Random.value < dropChance) {
                    Instantiate(o, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0, 0, 0, 0));
                }
            }
            player.SendMessage("increaseCounter", pointsWorth);
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

    void OnCollisionStay2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(150, 10.5f));
            player.SendMessage("reduceLive", 1);
        }
    }
}
