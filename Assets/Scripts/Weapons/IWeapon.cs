﻿using UnityEngine;
using System.Collections;

public enum BulletType {
    CLASSICBULLET, LASER, ROCKET
}
public abstract class IWeapon: MonoBehaviour {

    protected bool isPause;

    public abstract void fire(float extraDmg);
    public abstract void upgrade();
    public abstract int getPowerUpCounts();
    public abstract BulletType getBulletType();

    public void pause() {
        isPause = true;
    }

    public void unPause() {
        isPause = false;
    }
}
