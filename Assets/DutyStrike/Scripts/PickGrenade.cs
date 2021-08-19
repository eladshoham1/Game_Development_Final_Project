using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGrenade : Pick
{
    public GameObject grenadeImage;

    private GrenadeThrower grenadeThrower;
    private bool isPlayerInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        grenadeThrower = null;
        isPlayerInTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Stats>().IsDead())
        {
            pickText.SetActive(false);
            isPlayerInTrigger = false;
            return;
        }

        if (isPlayerInTrigger && Input.GetButtonDown("GunPickBtn"))
        {
            TakeGrenade();
            PlaySound();
            grenadeImage.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInTrigger = other.gameObject.tag == "Player";
        grenadeThrower = null;

        for (int i = 0; !grenadeThrower && i < other.transform.childCount; i++)
        {
            if (other.transform.GetChild(i).gameObject.name == "Camera")
            {
                if (other.gameObject.tag == "NPC" && !other.gameObject.GetComponent<Stats>().IsDead())
                    grenadeThrower = other.transform.GetChild(i).gameObject.GetComponent<GrenadeThrowerNPC>();
                else if (other.gameObject.tag == "Player")
                    grenadeThrower = other.transform.GetChild(i).gameObject.GetComponent<GrenadeThrower>();
            }
        }

        if (other.gameObject.tag == "Player")
            ShowText(this.gameObject.tag);
        else if (other.gameObject.tag == "NPC" && !other.gameObject.GetComponent<Stats>().IsDead())
            TakeGrenade();
    }

    private void OnTriggerExit(Collider other)
    {
        pickText.SetActive(false);
        isPlayerInTrigger = false;
    }

    void TakeGrenade()
    {
        if (grenadeThrower)
        {
            grenadeThrower.SetHaveGrenade(true);
            this.gameObject.SetActive(false);
            pickText.SetActive(false);
        }
    }
}