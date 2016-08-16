using UnityEngine;
using System.Collections;

public class ShopContentScript : MonoBehaviour {
    public ArrayList items = new ArrayList();
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addElement(GameObject o) {
        items.Add(o);
    }

    public void deselectItems() {
        foreach (GameObject o in items) {
            o.GetComponent<ShopItemScript>().isFocused = false;
        }
    }
}
