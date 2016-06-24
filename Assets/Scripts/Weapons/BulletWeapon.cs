using UnityEngine;
using System.Collections;
using System;

public class BulletWeapon: IWeapon {

    public float basicFirerate = 0.5f; // Firerate in seconds
    static float firerate;
    public float maxFirerate = 0.1f;
    public float projectileSpeed = 18f;
    static int firerateCounter = 1;
    static float fireRateTimer;
    public GameObject bullet;
    static GameObject player;
    static BulletType bulletType;




    // Use this for initialization
    void Start () {
        bulletType = BulletType.CLASSICBULLET;
        player = GameObject.FindGameObjectWithTag("Player");
        firerate = basicFirerate;
        fireRateTimer = firerate;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPause) {
            fireRateTimer -= Time.deltaTime;
        }
}

    public override void fire() {

        if (!isPause) {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos =  new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                if (fireRateTimer <= 0) {
                    GameObject classicProjectile = (GameObject)Instantiate(bullet, myPos, rotation);
                    classicProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                    fireRateTimer = firerate;
                }

            }
    }

    public override void upgrade(float power) {
        if (firerate >= maxFirerate) {
            firerate = firerate * power;
            firerateCounter++;
        }
    }

    public override int getPowerUpCounts() {
        return firerateCounter;
    }

    public override BulletType getBulletType() {
        return bulletType;
    }
}
