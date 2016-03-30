using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeLevelWithFade : MonoBehaviour {

    private IEnumerator loadLevel(string level) {
        float fadeTime = GameObject.FindGameObjectWithTag("GM").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(level);
    }

    private IEnumerator loadLevel(int id) {
        float fadeTime = GameObject.FindGameObjectWithTag("GM").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(id);
    }
}
