using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSound : MonoBehaviour
{
    private float delay;
    private AudioSource sound;
    // Start is called before the first frame update
    
    void Start()
    {
        delay = 0f;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("ThrowerGrenade").Length !=0)
            delay += Time.deltaTime;
        if (delay >= 2.85f)
        {
            sound.Play();
            delay = 0f;
        }
    }
}
