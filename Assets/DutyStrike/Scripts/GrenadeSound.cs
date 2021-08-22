using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSound : MonoBehaviour
{
    private AudioSource sound;
    private bool isGrenadeExplosion;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isGrenadeExplosion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrenadeExplosion && GameObject.FindGameObjectWithTag("Grenade Explosion"))
        {
            sound.Play();
            isGrenadeExplosion = true;
        } else if (!GameObject.FindGameObjectWithTag("Grenade Explosion"))
            isGrenadeExplosion = false;
    }
}
