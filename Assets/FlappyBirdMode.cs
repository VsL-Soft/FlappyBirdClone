using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Button PlayAgainButton;


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

        if (player.transform.position.y < bottomBorder || player.transform.position.y > topBorder) {
            player.SendMessage("reduceLive", 1);
            //TODO push player towords center
            
        }
        timer -= Time.deltaTime;
        if (spawnObjects) {
            spawnObsticle();
        }
	}
    
    //TODO mothod unneccessary
    private void lose() {
        //TODO pause
        pause();
        fadeOut();
        //remove later      
        //Application.LoadLevel(Application.loadedLevelName);
    }
    // TODO anything lol
    private void fadeOut() {
        
    }

    public void ButtonClicked() {
        SceneManager.LoadScene("Level1");
    }

    //TODO pause
    public void pause() {
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
