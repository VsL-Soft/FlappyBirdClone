using UnityEngine;
using System.Collections;
using System;

public class RocketWeapon : IWeapon {

    public float basicFirerate = 1.0f; // Firerate in seconds
    static float firerate;
    public float projectileSpeed = 10f;
    static int upgradeCounter = 1;
    public int startupUpgradeCounter = 1;
    static float fireRateTimer;
    public GameObject rocket;
    static GameObject player;
    static BulletType bulletType;
    public float basicDamage = 20;
    public float basicExplosionSize = 1;
    static float damage;
    static float explosionSize;
    public float rocketUpgradePower = 1.085f;





    // Use this for initialization
    void Start() {
        firerate = basicFirerate;
        fireRateTimer = firerate;
        setUpgrades(startupUpgradeCounter);
        bulletType = BulletType.ROCKET;
        player = GameObject.FindGameObjectWithTag("Player");
        damage = basicDamage;
        explosionSize = basicExplosionSize;
    }

    // Update is called once per frame
    void Update() {
        if (!isPause) {
            fireRateTimer -= Time.deltaTime;
        }
    }

    public override void fire(float extraDmg) {

        if (!isPause) {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            if (fireRateTimer <= 0) {
                GameObject rocketProjectile = (GameObject)Instantiate(rocket, myPos, rotation);
                rocketProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                rocketProjectile.GetComponent<RocketScript>().setWeaponParameters(damage*(1+extraDmg), explosionSize);
                fireRateTimer = firerate;
            }

        }
    }

    private void setUpgrades(int n) {
        upgradeCounter = 1;
        firerate = basicFirerate;
        for (int i = 1; i < n; i++) {
            upgrade();
        }
    }

    public override void upgrade() {
        if (upgradeCounter < 20) {
            explosionSize *= ((rocketUpgradePower - 1) / 2) + 1;
            damage *= rocketUpgradePower;
            upgradeCounter++;
        }
    }

    public override int getPowerUpCounts() {
        return upgradeCounter;
    }

    public override BulletType getBulletType() {
        return bulletType;
    }
}
