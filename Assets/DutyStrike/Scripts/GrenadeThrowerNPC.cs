using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowerNPC : GrenadeThrower
{
    // Update is called once per frame
    void Update()
    {
        if (delay > 0f && delay < 4f)
            delay += Time.deltaTime;
        else
            delay = 0f;
    }

    public void Throw()
    {
        if (haveGrenade && delay == 0f)
        {
            throwGrenade();
            delay += Time.deltaTime;
        }
    }
}
