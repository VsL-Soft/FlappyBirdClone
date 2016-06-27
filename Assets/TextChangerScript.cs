using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextChangerScript : MonoBehaviour {
    private int value;
    private DataManagerScript dm;

	// Use this for initialization
	void Start () {
        dm = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManagerScript>();
        value = dm.getEndlessHighscore();
        GetComponent<Text>().text = "Highscore : " + value;
	}
	
}
