using UnityEngine;
using System.Collections;
using System;

public class LaserWeapon : IWeapon {

    static BulletType bulletType;
    GameObject player;
    static LineRenderer lr;
    static Vector2 target;
    static Vector2 myPos;
    static Vector2 direction;
    static RaycastHit2D hit;
    public float initialLaserPower = 15f;
    static float laserPower;
    static int laserCounter = 1;

    // Use this for initialization
    void Start () {
        laserCounter = 1;
        laserPower = initialLaserPower;
        bulletType = BulletType.LASER;
        player = GameObject.FindGameObjectWithTag("Player");
        lr = GetComponent<LineRenderer>();
        lr.sortingLayerName = "player";
        lr.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate () {

        target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        myPos = player.transform.position;
        direction = target - myPos;
        direction.Normalize();
        hit = Physics2D.Raycast(myPos, direction, 20f);
        lr.enabled = false;
    }


    public override void fire() {
        if (hit.collider != null) {
            if (hit.collider.tag == "Enemy" || hit.collider.tag == "FlappyObsticle" || hit.collider.tag == "Player") {
                if (hit.collider.tag == "Enemy") {
                    hit.transform.SendMessage("getDamage", laserPower * Time.deltaTime);
                }
                Vector3[] points = { myPos, hit.point };
                lr.SetPositions(points);

            }
            else {
                Vector3[] points = { myPos, (myPos + (direction * 20)) };
                lr.SetPositions(points);
            }
        }
        else {
            Vector3[] points = { myPos, (myPos + (direction * 20)) };
            lr.SetPositions(points);
        }
        lr.enabled = true;
    }

    public override void upgrade(float power) {
        if (laserCounter < 20) {
            laserPower = laserPower * power;
            laserCounter++;
        }
    }

    public override int getPowerUpCounts() {
        return laserCounter;
    }

    public override BulletType getBulletType() {
        return bulletType;
    }
}
