using UnityEngine;
using System.Collections;

public class FlappyBirdCamera : MonoBehaviour {
    GameObject player;
    public float offsetX = 0;
    public float offsetY = 0;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player"); 
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x + offsetX, transform.position.y + offsetY, -10);
	}

    public void setYPosition(float y) {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
