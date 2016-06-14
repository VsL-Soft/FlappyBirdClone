using UnityEngine;
using System.Collections;

public enum BulletType {
    CLASSICBULLET, LASER
}
public abstract class IWeapon: MonoBehaviour {

    protected bool isPause;
    public int upgradeCounts = 0;

    public BulletType bulletType;

    public abstract void fire();
    public abstract void upgrade(float power);

    public void pause() {
        isPause = true;
    }

    public void unPause() {
        isPause = false;
    }
}
