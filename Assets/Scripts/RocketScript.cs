using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

    public float dmg = 10;
    public float timer = 10;
    public float explosionSize;
    public AudioClip sound;
    public GameObject explosion;
    // Use this for initialization
    void Start() {
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "FlappyObsticle" || other.tag == "Enemy") {
            Vector2 myPos = this.transform.position;
            Quaternion rotation = new Quaternion();
            GameObject explosionObject = (GameObject)Instantiate(explosion, myPos, rotation);
            explosionObject.GetComponent<ExplosionScript>().setWeaponParameters(dmg, explosionSize);
            Destroy(this.gameObject);
        }        
        
    }

    public void setWeaponParameters(float damage, float explosionSize) {
        dmg = damage;
        this.explosionSize = explosionSize;
    }
}
