using UnityEngine;
using System.Collections;

public enum BulletType {
    CLASSICBULLET, LASER
}
public abstract class IWeapon: MonoBehaviour {

    protected bool isPause;

    public BulletType bulletType;

    public abstract void fire(Vector3 v);
    public abstract void upgrade();

    public void pause() {
        isPause = true;
    }

    public void unPause() {
        isPause = false;
    }
}
