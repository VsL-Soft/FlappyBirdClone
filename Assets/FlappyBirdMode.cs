using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

public class FlappyBirdMode : MonoBehaviour {
    private float topBorder;
    private float bottomBorder;
    private float midPoint;
    public float distanceBetweenBorders = 15;
    public float timer = 5;
    public bool spawnObjects = true;

    public Canvas canvas;
    public GameObject loseText;
    public GameObject obsticle;
    public GameObject player;
    public GameObject enemy;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        midPoint = player.transform.position.y;
        topBorder = midPoint + distanceBetweenBorders/2;
        bottomBorder = midPoint - distanceBetweenBorders/2;
        canvas = FindObjectOfType<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(player.transform.position.y < bottomBorder || player.transform.position.y > topBorder) {
            lose();
        }
        timer -= Time.deltaTime;
        if (spawnObjects) {
            spawnObsticle();
        }
	}

    private void lose() {
        GameObject p = (GameObject)Instantiate(loseText, new Vector3(player.transform.position.x + 20, bottomBorder + 3.5f, player.transform.position.z), new Quaternion());
        p.transform.SetParent(canvas.transform);
        fadeOut();
        showLosingScreen();
        //remove later      
        Application.LoadLevel(Application.loadedLevelName);
    }
    // TODO anything lol
    private void fadeOut() {

    }
    //TODO anything
    private void showLosingScreen() {

    }

    void spawnObsticle() {
        if(timer <= 0) {
            float r = Random.value;
            if(r >= 0.5) {
                GameObject projectile = (GameObject)Instantiate(obsticle, new Vector3(player.transform.position.x + 20, bottomBorder + 3.3f, player.transform.position.z), new Quaternion());
                GameObject e = (GameObject)Instantiate(enemy, new Vector3(player.transform.position.x + 20, bottomBorder + 3.3f, player.transform.position.z), new Quaternion());
            } else {
                GameObject projectile = (GameObject)Instantiate(obsticle, new Vector3(player.transform.position.x + 20, topBorder - 3.3f, player.transform.position.z), new Quaternion(0,0,180,0));
            }
            timer = 5;
        }
    }
}
