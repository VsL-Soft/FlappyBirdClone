using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiSkript : MonoBehaviour {

    public Button PlayAgainButton;
    public Button MainManuButton;
    public Texture2D heart;
    public Texture2D firerateIcon;
    public Texture2D laserIcon;
    public Texture2D box;
    public Texture2D unusedBuff;
    public Texture2D usedBuff;
    public float sizeOfObject = 40;
    public float positionOfObjects = 10;
    public GUIStyle counterStyle;
    private PlayerControlls player;
    private GameObject gm;
    public Texture2D pauseImage;
    public Texture2D playImage;
    private bool isPause;
    public GUIStyle pauseButtonStyle;
    public float sizeOfPauseButton;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlls>();
        gm = GameObject.FindGameObjectWithTag("GM");
        PlayAgainButton.gameObject.SetActive(false);
        MainManuButton.gameObject.SetActive(false);
        isPause = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnGUI() {
        //Hearts
        for (int i = 0; i < player.lives; i++) {
            GUI.Label(new Rect(positionOfObjects + (sizeOfObject * i), positionOfObjects, sizeOfObject, sizeOfObject), heart);
        }
        //Pause Button
        if (isPause) {
            if (GUI.Button(new Rect(Screen.width - positionOfObjects - sizeOfPauseButton, positionOfObjects, sizeOfPauseButton, sizeOfPauseButton), playImage, pauseButtonStyle)) {
                if (!player.isDead) {
                    gm.SendMessage("unPauseTheGame");
                }
           }
        } else {
            if (GUI.Button(new Rect(Screen.width - positionOfObjects - sizeOfPauseButton, positionOfObjects, sizeOfPauseButton, sizeOfPauseButton), pauseImage, pauseButtonStyle)) {
                gm.SendMessage("pauseTheGame");
            }
        }
        //Counter
        GUI.Label(new Rect(positionOfObjects * 2f, positionOfObjects + (sizeOfObject * 2), sizeOfObject * 4, sizeOfObject), player.pointCounter.ToString(), counterStyle);
        //Bullet Stuff
        switch(player.weapon.GetComponent<IWeapon>().bulletType) {
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
        for (int i = 0; i < player.upgradeCounts; i++) {
            GUI.Label(new Rect((positionOfObjects * 2f + sizeOfObject / 5) + (sizeOfObject / 5 * (i + 1)), positionOfObjects + sizeOfObject, sizeOfObject / 5, sizeOfObject), usedBuff);
        }
        for (int i = player.upgradeCounts; i < 20; i++) {
            GUI.Label(new Rect((positionOfObjects * 2f + sizeOfObject / 5) + (sizeOfObject / 5 * (i + 1)), positionOfObjects + sizeOfObject, sizeOfObject / 5, sizeOfObject), unusedBuff);
        }
    }

    public void pause() {
        isPause = true;
        PlayAgainButton.gameObject.SetActive(true);
        MainManuButton.gameObject.SetActive(true);
    }

    public void unPause() {
        isPause = false;
        PlayAgainButton.gameObject.SetActive(false);
        MainManuButton.gameObject.SetActive(false);
    }
}
