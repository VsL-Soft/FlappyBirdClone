using UnityEngine;
using System.Collections;

public class GuiSkript : MonoBehaviour {

    public Texture2D heart;
    public float sizeOfHearts = 40;
    public float positionOfHearts = 10;
    public GUIStyle counterStyle;
    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnGUI() {
        for (int i = 0; i < player.GetComponent<PlayerControlls>().lives; i++) {
            GUI.Label(new Rect(positionOfHearts + (sizeOfHearts * i), positionOfHearts, sizeOfHearts, sizeOfHearts), heart);
        }
        GUI.Label(new Rect(positionOfHearts + 10, positionOfHearts + sizeOfHearts, sizeOfHearts * 4, sizeOfHearts), player.GetComponent<PlayerControlls>().counter.ToString(), counterStyle);
    }
}
