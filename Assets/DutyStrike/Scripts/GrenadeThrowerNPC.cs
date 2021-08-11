using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowerNPC : GrenadeThrower
{

    void Start()
    {
        delay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay < 4f)
            delay += Time.deltaTime;
    }

    public float GetDelay()
    {
        return this.delay;
    }

    public void Throw()
    {
        if (haveGrenade && delay >= 4f)
        {
            throwGrenade();
            delay = 0f;
        }
    }
}
