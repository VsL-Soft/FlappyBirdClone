using UnityEngine;
using System.Collections;

public class CollectableItem : MonoBehaviour {
    public int increaseLiveBy = 1;
    public AudioClip soundAponCollection;
	// Use this for initialization
	void Start () {
	
	}

    void Update() {

    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            other.SendMessage("increaseLive", increaseLiveBy);
            AudioSource.PlayClipAtPoint(soundAponCollection, transform.position);
            Destroy(this.gameObject);
        }
    }
}
