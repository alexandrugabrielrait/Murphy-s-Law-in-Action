using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Breakable : MonoBehaviour
{
    protected int health;

    public abstract void Break();

    public void Hit()
    {
        if (health > 0)
            health--;
        if (health == 0)
        {
            health = -1;
            Break();
        }
    }
}
