using UnityEngine;
using System.Collections;

public class DataManagerScript : MonoBehaviour {
    private int endlessHighscore;
    private int playerMoney;
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
	// Use this for initialization
	void Start () {
        loadEndlessHighscore();
        loadPlayerMoney();
        Debug.Log("Endless HS : " + endlessHighscore);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getEndlessHighscore() {
        return endlessHighscore;
    }

    public int loadEndlessHighscore() {
        endlessHighscore = PlayerPrefs.GetInt("endlesshighscore", 0);
        return endlessHighscore;
    }

    public bool safeEndlessHighscore(int value) {
        PlayerPrefs.SetInt("endlesshighscore", value);
        return true;
    }

    public int loadPlayerMoney() {
        return PlayerPrefs.GetInt("playermoney", 0);
    }

    public bool safePlayerMoney(int value) {
        PlayerPrefs.SetInt("playermoney", value);
        return true;
    }

    public bool addPlayerMoney(int value) {
        safePlayerMoney(loadPlayerMoney() + value);
        return true;
    }

    public void safePlayerState() {
        PlayerControlls player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlls>();
        GameMasterScript gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMasterScript>();
        switch (gm.getCurrentGameMode()) {
            case GameMode.FLAPPYMODE:
                if (player.getPointCounter() > getEndlessHighscore()) {
                    safeEndlessHighscore((int)(player.getPointCounter()));
                    endlessHighscore = (int)(player.getPointCounter());
                }
                addPlayerMoney(player.collectedMoney);
            break;
            case GameMode.MAINMENU:
                Application.Quit();
            break;
            default:
            break;
        }
    }

}
