﻿using UnityEngine;
using System.Collections;
public enum itemType{
    HEART,FIRERATE,LASERPOWER,LASER,BULLET
}

public class CollectableItem : MonoBehaviour {
    public int increaseLiveBy = 0;
    public float increaseFirerate = 1.0f;
    public float increaseLaserDamage = 1.0f;
    public AudioClip soundAponCollection;
    public itemType thisItemType;
    public GameObject Weapon;
	// Use this for initialization
	void Start () {
	
	}

    void Update() {

    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            switch (thisItemType) {
                case itemType.HEART:
                    other.SendMessage("increaseLive", increaseLiveBy);
                break;
                case itemType.FIRERATE:
                    if (other.GetComponent<PlayerControlls>().getWeaponType() == BulletType.CLASSICBULLET) {
                        other.GetComponent<PlayerControlls>().weapon.GetComponent<IWeapon>().upgrade(increaseFirerate);
                    }
                    //other.SendMessage("increaseFirerate", increaseFirerate);
                break;
                case itemType.LASERPOWER:
                    if (other.GetComponent<PlayerControlls>().getWeaponType() == BulletType.LASER) {
                        other.GetComponent<PlayerControlls>().weapon.GetComponent<IWeapon>().upgrade(increaseLaserDamage);
                    }
                    //other.SendMessage("increaseLaserPower", increaseLaserDamage);
                    break;
                case itemType.BULLET:
                    other.SendMessage("setWeapon", Weapon);
                    break;
                case itemType.LASER:
                    other.SendMessage("setWeapon", Weapon);
                    break;
                default:
                    Debug.Log("Collectible ItemType not yet implementet");
                    break;
            }
            if (soundAponCollection != null) {
                AudioSource.PlayClipAtPoint(soundAponCollection, transform.position);
            }
            Destroy(this.gameObject);
        }
    }
}
