﻿using UnityEngine;
using System.Collections;

public class MusicManagerScript : MonoBehaviour {

    [SerializeField]
    Sound[] sounds;

    void Start() {
        for (int i = 0; i < sounds.Length; i++) {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("Music");
    }

    public void PlaySound(string _name) {
        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == _name) {
                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }

    public void StopSound(string _name) {
        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == _name) {
                sounds[i].Stop();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }
}
