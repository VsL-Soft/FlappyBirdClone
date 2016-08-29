using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class ShopItemScript : MonoBehaviour {
    public int id = 0;
    public Color highlighted;
    public Color notHighlighted;
    public bool isFocused;
    public int price;
    public bool bought = false;
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
