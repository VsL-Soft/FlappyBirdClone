using UnityEngine;
using System.Collections;

public class ChangeSceneAfterXSeconds : MonoBehaviour {
    public float timer = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            GameObject.FindGameObjectWithTag("GM").GetComponent<GameMasterScript>().loadLevel("StartScreen");
            timer = 20;
        }
	}
}
