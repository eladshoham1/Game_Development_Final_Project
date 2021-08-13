using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject player;
    public GameObject aCamera;
    public ParticleSystem muzzleFlash;
    public GameObject statusCanvas;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!statusCanvas.activeInHierarchy)
            return;

        if (!player.GetComponent<Stats>().IsDead() && this.transform.parent.tag == "Player" && Input.GetButtonDown("Shot"))
        {
            RaycastHit hit;
            StartCoroutine(ShowShot());

            if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
            {

                if (hit.transform.tag == "Player" || hit.transform.tag == "NPC")
                {
                    if (this.transform.parent.parent.parent.parent.name != hit.transform.parent.name)
                        hit.transform.gameObject.GetComponent<Stats>().Shot(this.transform.parent.tag);
                }
            }
        }
    }

    IEnumerator ShowShot()
    {
        muzzleFlash.Play();
        sound.Play();
        yield return new WaitForSeconds(0.1f);
    }
}
