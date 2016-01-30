using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    GameObject player;

    public bool isPause;

    public float speed = 1.5f;
    public float live = 20;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        isPause = false;
    }

    // Update is called once per frame
    void Update() {
        if (!isPause) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (live <= 0) {
                Destroy(this.gameObject);
            }
            neutralizeVelocity();
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(8, 1.5f));
            player.SendMessage("reduceLive", 1);
        }
    }
     
    public void getDamage(float dmg) {
        this.live -= dmg;
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
}
