using UnityEngine;
using System.Collections;
public enum itemType{
    HEART,FIRERATE,LASERPOWER,LASER,BULLET,COIN,
}

public class CollectableItem : MonoBehaviour {
    public int increaseLiveBy = 0;
    public AudioClip soundAponCollection;
    public itemType thisItemType;
    public GameObject Weapon;
    public AudioClip onCollect;
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
                        other.GetComponent<PlayerControlls>().weapon.GetComponent<IWeapon>().upgrade();
                    }
                    //other.SendMessage("increaseFirerate", increaseFirerate);
                break;
                case itemType.LASERPOWER:
                    if (other.GetComponent<PlayerControlls>().getWeaponType() == BulletType.LASER) {
                        other.GetComponent<PlayerControlls>().weapon.GetComponent<IWeapon>().upgrade();
                    }
                    //other.SendMessage("increaseLaserPower", increaseLaserDamage);
                    break;
                case itemType.BULLET:
                    other.SendMessage("setWeapon", Weapon);
                    break;
                case itemType.LASER:
                    other.SendMessage("setWeapon", Weapon);
                    break;
                case itemType.COIN:
                    other.SendMessage("increaseMoney", increaseLiveBy);
                    AudioSource.PlayClipAtPoint(onCollect, transform.position);
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
