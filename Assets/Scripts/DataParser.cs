using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;

public class DataParser : MonoBehaviour {
    private string jsonString;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool getBoughtStatus(int id) {
        jsonString = File.ReadAllText(Application.streamingAssetsPath + "/json/Items.json");

        Debug.Log(jsonString);
        return false;
    }
}
