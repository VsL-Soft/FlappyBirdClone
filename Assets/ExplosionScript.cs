using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

    public float damage;
    public float explosionSize;
    public GameObject explosionAnimation;
    // Use this for initialization
    void Start() {
        this.transform.localScale = new Vector3(explosionSize, explosionSize);
        Vector2 myPos = this.transform.position;
        Quaternion rotation = new Quaternion();
        GameObject explosionObject = (GameObject)Instantiate(explosionAnimation, myPos, rotation);
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Enemy") {
            other.gameObject.SendMessage("getDamage", damage);
        }
    }

    public void setWeaponParameters(float damage, float explosionSize) {
        this.damage = damage;
        this.explosionSize = explosionSize;
    }
}
