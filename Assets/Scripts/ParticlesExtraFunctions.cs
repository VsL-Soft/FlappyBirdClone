using UnityEngine;
using System.Collections;

public class ParticlesExtraFunctions : MonoBehaviour {
    private ParticleSystem ps;


    public void Start() {
        ps = GetComponent<ParticleSystem>();
    }
    //Destroys the particleSystemPrefa once it is done emitting
    public void Update() {
        if (ps) {
            if (!ps.IsAlive()) {
                Destroy(gameObject);
            }
        }
    }
}
