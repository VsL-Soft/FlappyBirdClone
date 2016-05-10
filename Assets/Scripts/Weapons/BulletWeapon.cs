using UnityEngine;
using System.Collections;
using System;

public class BulletWeapon: IWeapon {

    public float basicFirerate = 0.5f; // Firerate in seconds
    public float firerate;
    public float maxFirerate = 0.1f;
    public float projectileSpeed = 10;
    public int firerateCounter = 1;
    public float fireRateTimer;
    public GameObject bullet;
    private GameObject player;
    private PlayerControlls playerSkript;




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

    public override void fire(Vector3 v) {

        if (!isPause) {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = v; // new Vector2(player.transform.position.x, player.transform.position.y);
            Debug.Log(player.transform.position.x);
            Vector2 direction = target - myPos;
            direction.Normalize();
            Debug.Log(direction);
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            Debug.Log("test");
            //TODO: I Dont Get it!!! dafuq
               // if (fireRateTimer <= 0) {
                    Debug.Log("wtf");
                    GameObject classicProjectile = (GameObject)Instantiate(bullet, myPos, rotation);
                    classicProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                    fireRateTimer = firerate;
                //}

            }
    }

    public override void upgrade() {
        throw new NotImplementedException();
    }

}
