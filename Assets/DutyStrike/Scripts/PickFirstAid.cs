using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFirstAid : Pick
{
    private GameObject objectInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        objectInTrigger = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Stats>().IsDead())
        {
            pickText.SetActive(false);
            objectInTrigger = null;
            return;
        }

        if (objectInTrigger && objectInTrigger.name == "Player" && Input.GetButtonDown("GunPickBtn"))
        {
            objectInTrigger.GetComponent<Stats>().AddFirstAid();
            pickText.SetActive(false);
            this.gameObject.SetActive(false);
            PlaySound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        objectInTrigger = other.gameObject;

        if (other.tag == "Player")
        {
            ShowText(this.gameObject.tag);
        }
        else if (other.tag =="NPC")
        {
            objectInTrigger.GetComponent<Stats>().AddFirstAid();
            this.gameObject.SetActive(false);
            pickText.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickText.SetActive(false);
        objectInTrigger = null;
    }
}
