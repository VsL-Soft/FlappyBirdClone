using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float dmg = 1;
    public float timer = 5;
    public AudioClip sound;
	// Use this for initialization
	void Start () {
        AudioSource.PlayClipAtPoint(sound, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag  == "FlappyObsticle") {
            Destroy(this.gameObject);
        } else if(other.tag == "Enemy"){
            other.gameObject.SendMessage("getDamage", dmg);
            Destroy(this.gameObject);
        }
    }
}
