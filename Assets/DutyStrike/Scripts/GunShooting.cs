using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunShooting : MonoBehaviour
{
    public GameObject aCamera;
    public ParticleSystem muzzleFlash;
    public GameObject enemy;
    public GameObject enemy2;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shot"))
        {
            RaycastHit hit;

            if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
            {
                StartCoroutine(ShowShot());
                HitType(hit.transform.gameObject.tag);
            }
        }
    }

    private void HitType(string tag)
    {
        switch (tag)
        {
            case "Head NPC1":
                HitEnemy(enemy, "head");
                break;
            case "Body NPC1":
                HitEnemy(enemy, "body");
                break;
            case "Head NPC2":
                HitEnemy(enemy2, "head");
                break;
            case "Body NPC2":
                HitEnemy(enemy2, "body");
                break;
            default:
                break;
        }
    }

    private void HitEnemy(GameObject npc, string type)
    {
        Stats stats = npc.GetComponent<Stats>();
        Animator anim = npc.GetComponent<Animator>();
        NavMeshAgent nma = npc.GetComponent<NavMeshAgent>();

        switch (type)
        {
            case "Head":
                stats.HeadShot(this.transform.parent.name);
                break;
            case "Body":
                stats.BodyShot(this.transform.parent.name);
                break;
            default:
                break;
        }

        if (stats.IsDead())
        {
            anim.SetInteger("NPCState", 4);
            nma.enabled = false;
        }
    }

    IEnumerator ShowShot()
    {
        muzzleFlash.Play();
        sound.Play();
        yield return new WaitForSeconds(0.1f);
    }
}
