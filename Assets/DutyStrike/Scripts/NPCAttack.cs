using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSMGameStudio.Behaviours;

public class NPCAttack : MonoBehaviour
{
    public GameObject aCamera;
    public GameObject weaponsInHand;

    private ParticleSystem muzzleFlash;
    private AudioSource sound;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        delay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0f)
            delay -= Time.deltaTime;
        if (delay < 0f)
            delay = 0f;
    }

    public void Attack()
    {
        if (HaveWeaponInHand() && HaveGrenade())
        {
            switch (Random.Range(0, 2))
            {
                case 0: GunShooting(); break;
                case 1: ThrowGrenade(); break;
            }
        }
        else if (HaveWeaponInHand())
            GunShooting();
        else if (HaveGrenade())
            ThrowGrenade();
    }

    private void GunShooting()
    {
        RaycastHit hit;

        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
        {
            if (delay == 0f)
            {
                if (hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "NPC")
                {
                    StartCoroutine(ShowShot());
                    hit.transform.gameObject.GetComponent<Stats>().Shot(this.transform.tag);
                }
                else if (hit.transform.tag == "Breakable")
                    hit.transform.gameObject.GetComponent<Breakable>().Break();
                
                delay = 2f;
            }
        }
    }

    private void ThrowGrenade()
    {
        aCamera.GetComponent<GrenadeThrowerNPC>().Throw();
    }

    public bool HaveWeaponInHand()
    {
        for (int i = 0; i < weaponsInHand.transform.childCount; i++)
        {
            if (weaponsInHand.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                sound = weaponsInHand.transform.GetChild(i).gameObject.GetComponent<AudioSource>();
                FindMuzzleFlash(weaponsInHand.transform.GetChild(i).gameObject);
                return true;
            }
        }

        return false;
    }

    public bool HaveGrenade()
    {
        return aCamera.GetComponent<GrenadeThrowerNPC>().GetHaveGrenade();
    }

    private void FindMuzzleFlash(GameObject weapon)
    {
        for (int i = 0; i < weapon.transform.childCount; i++)
        {
            if (weapon.transform.GetChild(i).gameObject.tag == "Muzzle Flash")
            {
                muzzleFlash = weapon.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                break;
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
