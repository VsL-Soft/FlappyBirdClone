using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : MonoBehaviour {
    private GameObject player;
    public float speed = 1;
    public bool isPause = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (isPause == false) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime );
        }
	}

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.SendMessage("reduceLive", 1);
            Destroy(this.gameObject);
        } else if (other.tag != "Enemy") {
            Destroy(this.gameObject);
        }
    }

    public void pause() {
        isPause = true;
    }

    public void unPause() {
        isPause = false;
    }
}
