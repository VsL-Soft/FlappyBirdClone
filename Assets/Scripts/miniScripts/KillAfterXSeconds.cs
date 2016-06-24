using UnityEngine;
using System.Collections;

public class KillAfterXSeconds : MonoBehaviour {
    public float deadAfterXSeconds;
    private bool isPaused = false;
	// Update is called once per frame
	void Update () {
        if (isPaused == false) {
            deadAfterXSeconds -= Time.deltaTime;
        }
        if (deadAfterXSeconds <= 0) {
            Destroy(this.gameObject);
        }
	}

    public void pause() {
        isPaused = true;
    }

    public void unPause() {
        isPaused = false;
    }
}
