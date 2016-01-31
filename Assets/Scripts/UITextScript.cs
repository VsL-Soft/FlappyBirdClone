using UnityEngine;
using System.Collections;

public class UITextScript : MonoBehaviour {
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //this.GetComponent<Text>().text = player.GetComponent<PlayerControlls>().lives + " | " + 3;
    }
	
	// Update is called once per frame
	void Update () {
        //this.GetComponent<GUIText>().text = player.GetComponent<PlayerControlls>().lives + " | " + 3;
    }
}
