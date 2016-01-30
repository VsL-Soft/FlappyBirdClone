﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameMode {
    FLAPPYMODE
}
public class GameMasterScript : MonoBehaviour {
    GameObject player;
    GameMode gamemode;

    // saves the object from beeing destroyed on loading another scene
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Use this for initialization
    void Start () {
	    player = GameObject.FindGameObjectWithTag("Player");
        gamemode = GameMode.FLAPPYMODE;
        setGameMode(GameMode.FLAPPYMODE);
	}
	
	// Update is called once per frame
	void Update () {
        switch (gamemode) {
            case GameMode.FLAPPYMODE:
                break;
        }
	}

    public void setGameMode(GameMode g) {
        switch (g) {
            case GameMode.FLAPPYMODE:
                GameObject mode = (GameObject)Instantiate(Resources.Load("Flappybird"));
                mode.transform.SetParent(this.transform);
                break;
        }
    }

    public void pauseTheGame() {
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects) {
            go.SendMessage("pause", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void unPauseTheGame() {
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects) {
            go.SendMessage("unPause", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void loadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public void loadLevel(int id) {
        SceneManager.LoadScene(id);
    }
}
