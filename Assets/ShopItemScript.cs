using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItemScript : MonoBehaviour {
    public Color highlighted;
    public Color notHighlighted;
    public bool isFocused;
    public int price;
	// Use this for initialization
	void Start () {
        isFocused = false;
	}

    void Update() {
        if (!isFocused) {
            GetComponent<Image>().color = notHighlighted;
        }
    }
    public void debug() {
        transform.parent.GetComponent<ShopContentScript>().deselectItems();
        isFocused = true;
        GetComponent<Image>().color = highlighted; 
    }
    
}
