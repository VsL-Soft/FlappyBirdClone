using UnityEngine;
using System.Collections;

public enum GameMode {
    FLAPPYMODE
}
public class GameMasterScript : MonoBehaviour {
    GameObject player;
    GameMode gamemode;


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
}
