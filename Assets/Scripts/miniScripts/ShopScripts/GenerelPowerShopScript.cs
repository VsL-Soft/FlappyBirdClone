using UnityEngine;
using System.Collections;

public class GenerelPowerShopScript : MonoBehaviour,IShopItemEffect {
    public float increasePowerByPercent;
	// Use this for initialization

    public void startEffect() {
        PlayerPrefs.SetFloat("increasedPower",PlayerPrefs.GetFloat("increasedPower") + increasePowerByPercent);
        Destroy(this.gameObject);
    }
}
