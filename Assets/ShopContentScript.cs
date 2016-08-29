using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class ShopContentScript : MonoBehaviour {
    public GameObject[] items;
    // Use this for initialization
    void Start() {
        JSonParser s = new JSonParser();


        List <GameObject> a = new List<GameObject>();
        foreach (GameObject o in items) {
            GameObject instanted = Instantiate(o);
            instanted.transform.SetParent(this.transform);
            a.Add(instanted);
        }
        items = a.ToArray();

        writeItemsToJson();
        List<GameObject> d = new List<GameObject>();
        for (int i = 0; i < items.Length; i++) {
            if (items[i].GetComponent<ShopItemScript>().bought == false) {
                d.Add(items[i]);
            }
        }
        items = d.ToArray();
    }

    // Update is called once per frame
    void Update() {

    }

    public void deselectItems() {
        foreach (GameObject o in items) {
            o.GetComponent<ShopItemScript>().isFocused = false;
        }
    }

    private void writeItemsToJson() {
        string s = "";
        foreach (GameObject o in items) {
            s = s + JsonUtility.ToJson(o.GetComponent<ShopItemScript>()) + "\n";
        }

        File.WriteAllText(Application.streamingAssetsPath + "/json/Items.json", s);
        
    }
}
