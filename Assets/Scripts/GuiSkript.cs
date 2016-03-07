using UnityEngine;
using System.Collections;

public class GuiSkript : MonoBehaviour {

    public Texture2D heart;
    public Texture2D firerateIcon;
    public float sizeOfHearts = 40;
    public float positionOfHearts = 10;
    public GUIStyle counterStyle;
    private GameObject player;
    private GameObject gm;
    public Texture2D pauseImage;
    public Texture2D playImage;
    private bool isPause;
    public GUIStyle pauseButtonStyle;
    public float sizeOfPauseButton;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GM");
        isPause = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnGUI() {
        for (int i = 0; i < player.GetComponent<PlayerControlls>().lives; i++) {
            GUI.Label(new Rect(positionOfHearts + (sizeOfHearts * i), positionOfHearts, sizeOfHearts, sizeOfHearts), heart);
        }
        for (int i = 0; i < player.GetComponent<PlayerControlls>().firerateCounter; i++) {
            GUI.Label(new Rect(positionOfHearts + ((sizeOfHearts / 5)  + (sizeOfHearts / 2) * i), positionOfHearts + sizeOfHearts - 10, sizeOfHearts/5, sizeOfHearts), firerateIcon);
        }
            GUI.Label(new Rect(positionOfHearts + 10, positionOfHearts + (sizeOfHearts * 2), sizeOfHearts * 4, sizeOfHearts), player.GetComponent<PlayerControlls>().counter.ToString(), counterStyle);
        if (isPause) {
            if (GUI.Button(new Rect(Screen.width - positionOfHearts - sizeOfPauseButton, positionOfHearts, sizeOfPauseButton, sizeOfPauseButton), playImage, pauseButtonStyle)) {
                gm.SendMessage("unPauseTheGame");
           }
        }
        else {
            if (GUI.Button(new Rect(Screen.width - positionOfHearts - sizeOfPauseButton, positionOfHearts, sizeOfPauseButton, sizeOfPauseButton), pauseImage, pauseButtonStyle)) {
                gm.SendMessage("pauseTheGame");
            }
        }
    }

    public void pause() {
        isPause = true;
    }

    public void unPause() {
        isPause = false;
    }
}
