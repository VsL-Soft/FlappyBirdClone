using UnityEngine;
using System.Collections;


public enum Category {
    SKIN,POWERUPS,WEAPONS
}
public class UiManagerScript : MonoBehaviour {
    public Category currentCategory;
    public GameObject[] itemButtons;

	// Use this for initialization
	void Start () {
        currentCategory = Category.POWERUPS;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void switchCategory(Category c) {
        currentCategory = c;
        /*
        foreach (GameObject b in itemButtons) {

        }
         */
    }
}
