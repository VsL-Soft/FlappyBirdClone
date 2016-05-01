using UnityEngine;
using System.Collections;


public class GuiSkript : MonoBehaviour {

    public Texture2D heart;
    public Texture2D firerateIcon;
    public Texture2D laserIcon;
    public Texture2D box;
    public Texture2D unusedBuff;
    public Texture2D usedBuff;
    public float sizeOfObject = 40;
    public float positionOfObjects = 10;
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
        //Hearts
        for (int i = 0; i < player.GetComponent<PlayerControlls>().lives; i++) {
            GUI.Label(new Rect(positionOfObjects + (sizeOfObject * i), positionOfObjects, sizeOfObject, sizeOfObject), heart);
        }
        //Bullet Stuff
        switch(player.GetComponent<PlayerControlls>().bulletType) {
            case BulletType.CLASSICBULLET:
                GUI.Label(new Rect(positionOfObjects * 1.5f, positionOfObjects + sizeOfObject, sizeOfObject / 4, sizeOfObject), firerateIcon);
                break;
            case BulletType.LASER:
                GUI.Label(new Rect(positionOfObjects * 1.5f, positionOfObjects + sizeOfObject, sizeOfObject / 4, sizeOfObject), laserIcon);
                break;
            default:// Debug.Log("Not implemented");
            break;
        }
        GUI.Label(new Rect(positionOfObjects * 2f + sizeOfObject / 4, positionOfObjects + sizeOfObject, sizeOfObject * 5, sizeOfObject), box);
        for (int i = 0; i < player.GetComponent<PlayerControlls>().getWeaponBuffCounter(); i++) {
            GUI.Label(new Rect((positionOfObjects * 2f + sizeOfObject / 5) + (sizeOfObject / 5 * (i + 1)), positionOfObjects + sizeOfObject, sizeOfObject / 5, sizeOfObject), usedBuff);
        }
        for (int i = player.GetComponent<PlayerControlls>().getWeaponBuffCounter(); i < 20; i++) {
            GUI.Label(new Rect((positionOfObjects * 2f + sizeOfObject / 5) + (sizeOfObject / 5 * (i + 1)), positionOfObjects + sizeOfObject, sizeOfObject / 5, sizeOfObject), unusedBuff);
        }
        //Counter
        GUI.Label(new Rect(positionOfObjects * 2f, positionOfObjects + (sizeOfObject * 2), sizeOfObject * 4, sizeOfObject), player.GetComponent<PlayerControlls>().counter.ToString(), counterStyle);
        //Pause Button
        if (isPause) {
            if (GUI.Button(new Rect(Screen.width - positionOfObjects - sizeOfPauseButton, positionOfObjects, sizeOfPauseButton, sizeOfPauseButton), playImage, pauseButtonStyle)) {
                gm.SendMessage("unPauseTheGame");
           }
        } else {
            if (GUI.Button(new Rect(Screen.width - positionOfObjects - sizeOfPauseButton, positionOfObjects, sizeOfPauseButton, sizeOfPauseButton), pauseImage, pauseButtonStyle)) {
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
