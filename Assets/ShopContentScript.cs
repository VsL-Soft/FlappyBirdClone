using UnityEngine;
using System.Collections;

public class ShopContentScript : MonoBehaviour {
    public GameObject itemObject;
    public ArrayList items = new ArrayList();
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 22; i++) {
            GameObject b = Instantiate(itemObject);
            addElement(b);
            b.transform.SetParent(transform);
        }
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
