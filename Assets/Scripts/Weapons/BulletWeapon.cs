using UnityEngine;
using System.Collections;
using System;

public class BulletWeapon: IWeapon {

    
    public float basicFirerate = 0.5f; // Firerate in seconds
    static float firerate;
    public float maxFirerate = 0.1f;
    public float projectileSpeed = 18f;
    static int firerateCounter = 1;
    public int startupFirerateCounter = 1;
    static float fireRateTimer;
    public GameObject bullet;
    static GameObject player;
    static BulletType bulletType;
    public float firerateUpgradePower = 0.915f;




    // Use this for initialization
    void Start () {
        firerate = basicFirerate;
        fireRateTimer = firerate;
        setFirerateUpgrades(startupFirerateCounter);
        bulletType = BulletType.CLASSICBULLET;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPause) {
            fireRateTimer -= Time.deltaTime;
        }
}

    public override void fire(float extraDmg) {

        if (!isPause) {
            if (fireRateTimer <= 0) {
                    Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                    Vector2 myPos =  new Vector2(player.transform.position.x, player.transform.position.y);
                    Vector2 direction = target - myPos;
                    direction.Normalize();
                    Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                    GameObject classicProjectile = (GameObject)Instantiate(bullet, myPos, rotation);
                    classicProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                    classicProjectile.GetComponent<BulletScript>().dmg *= (1 + extraDmg);
                    fireRateTimer = firerate;
                }

            }
    }

    private void setFirerateUpgrades(int n) {
        firerateCounter = 1;
        firerate = basicFirerate;
        for (int i = 1; i < n; i++) {
            upgrade();
        }
    }

    public override void upgrade() {
        if (firerate >= maxFirerate) {
            firerate = firerate * firerateUpgradePower;
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
