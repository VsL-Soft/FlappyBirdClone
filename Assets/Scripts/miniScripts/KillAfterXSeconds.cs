using UnityEngine;
using System.Collections;

public class KillAfterXSeconds : MonoBehaviour {
    public float deadAfterXSeconds;
	
	// Update is called once per frame
	void Update () {
        deadAfterXSeconds -= Time.deltaTime;
        if (deadAfterXSeconds <= 0) {
            Destroy(this);
        }
	}
}
