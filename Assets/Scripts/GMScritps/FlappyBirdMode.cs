using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlappyBirdMode : MonoBehaviour {
    private float topBorder;
    private float bottomBorder;
    private float midPoint;
    public bool isPause;
    public float distanceBetweenBorders = 15;
    public float timer = 5;
    private float usedTimer;
    public bool spawnObjects = true;


    public GameObject obsticle;
    public GameObject player;
    public GameObject[] enemy;


	// Use this for initialization
	void Start () { 
        isPause = false;
        usedTimer = timer;

    }
	
	// Update is called once per frame
	void Update () {
        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) {
            midPoint = player.transform.position.y;
            topBorder = midPoint + distanceBetweenBorders / 2;
            bottomBorder = midPoint - distanceBetweenBorders / 2;
            }
        }
        if (!isPause) {
            if (player.transform.position.y < bottomBorder || player.transform.position.y > topBorder) {
                player.SendMessage("reduceLive", 1);
                //TODO push player towords center

            }
            if (spawnObjects) {
                spawnObsticle();
            }
            usedTimer -= Time.deltaTime;
        }
	}

    public void pause() {
        isPause = true;
    }

    public void unPause() {
        isPause = false;
    }

    void spawnObsticle() {
        if(usedTimer <= 0) {
            float r = Random.value;
            if(r >= 0.5f) {
                GameObject projectile = (GameObject)Instantiate(obsticle, new Vector3(player.transform.position.x + 20, bottomBorder + 3.3f, player.transform.position.z), new Quaternion());
            } else {
                GameObject projectile = (GameObject)Instantiate(obsticle, new Vector3(player.transform.position.x + 20, topBorder - 3.3f, player.transform.position.z), new Quaternion(0,0,180,0));
            }
            r = Random.value;
            if (r >= 0.2f && r <= 0.85f) {
                r = Random.Range(1.0f, 8.0f);
                GameObject e = (GameObject)Instantiate(enemy[0], new Vector3(player.transform.position.x + 20, bottomBorder + r, player.transform.position.z), new Quaternion());
            }
            if (r >= 0.7f && r <= 1f) {
                r = Random.Range(1.0f, 8.0f);
                GameObject e = (GameObject)Instantiate(enemy[1], new Vector3(player.transform.position.x + 20, bottomBorder + r, player.transform.position.z), new Quaternion());
            }
            usedTimer = timer;
        }
        
    }
}
