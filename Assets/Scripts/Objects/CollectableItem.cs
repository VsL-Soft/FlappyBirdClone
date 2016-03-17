using UnityEngine;
using System.Collections;
public enum itemType{
    HEART,FIRERATE
}

public class CollectableItem : MonoBehaviour {
    public int increaseLiveBy = 0;
    public float increaseFirerate = 1.0f;
    public AudioClip soundAponCollection;
    public itemType thisItemType;
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
                    other.SendMessage("increaseFirerate", increaseFirerate);
                break;
            }
            AudioSource.PlayClipAtPoint(soundAponCollection, transform.position);
            Destroy(this.gameObject);
        }
    }
}
