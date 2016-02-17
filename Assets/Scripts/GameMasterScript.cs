using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameMode {
    FLAPPYMODE, MAINMENU
}
public class GameMasterScript : MonoBehaviour {
    GameObject player;
    public GameMode currentGamemode;
    public GameObject flappyBirdModePrefab;
    private GameObject activeGameModePrefab;
    private bool isPaused;

    // saves the object from beeing destroyed on loading another scene
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Use this for initialization
    void Start () {
	    player = GameObject.FindGameObjectWithTag("Player");
        currentGamemode = GameMode.MAINMENU;
        //setGameMode(GameMode.FLAPPYMODE);
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentGamemode) {
            case GameMode.FLAPPYMODE:
                break;
            case GameMode.MAINMENU:
            break;
        }
	}

    public void setGameMode(GameMode g) {
        switch (g) {
            case GameMode.FLAPPYMODE:
                activeGameModePrefab = (GameObject)Instantiate(flappyBirdModePrefab, Vector3.zero, new Quaternion());
                activeGameModePrefab.transform.SetParent(this.transform);
                break;
        }
    }

    public void pauseTheGame() {
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects) {
            go.SendMessage("pause", SendMessageOptions.DontRequireReceiver);
        }
        isPaused = true;
    }

    public void unPauseTheGame() {
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects) {
            go.SendMessage("unPause", SendMessageOptions.DontRequireReceiver);
        }
        isPaused = false;
    }

    public void loadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public void loadLevel(int id) {
        SceneManager.LoadScene(id);
    }

    public void pauseButten() {
        if(isPaused) {
            unPauseTheGame();
        } else {
            pauseTheGame();
        }
        
    }

    public void startEndlessMode() {
        setGameMode(GameMode.FLAPPYMODE);
    }

    public void restartMode() {
        switch (currentGamemode) {
            case GameMode.FLAPPYMODE:
            activeGameModePrefab = null;
            setGameMode(GameMode.FLAPPYMODE);
            break;
            case GameMode.MAINMENU:
            break;
        }
    }
}
