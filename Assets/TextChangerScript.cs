using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum Texttype {
    HIGHSCORE, COINS
}
public class TextChangerScript : MonoBehaviour {
    private int value;
    private DataManagerScript dm;
    public Texttype texttype;

	// Use this for initialization
	void Start () {
        dm = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManagerScript>();
        switch (texttype) {
            case Texttype.COINS:
            value = dm.loadPlayerMoney();
            GetComponent<Text>().text = "Coins: " + value;
            break;
            case Texttype.HIGHSCORE:
                value = dm.getEndlessHighscore();
                GetComponent<Text>().text = "Highscore: " + value;
            break;
            default:
            Debug.Log("Texttype not yet implemented");
            break;
        }
	}
	
}
