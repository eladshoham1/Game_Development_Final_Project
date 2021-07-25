using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickFirstAid : MonoBehaviour
{
    public GameObject pickGunText;

    private GameObject objectInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        objectInTrigger = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectInTrigger && objectInTrigger.name == "Player" && Input.GetButtonDown("GunPickBtn"))
        {
            objectInTrigger.GetComponent<Stats>().AddFirstAid();
            pickGunText.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objectInTrigger = other.gameObject;

        if (other.tag == "Player")
        {
            pickGunText.GetComponentInChildren<TextMeshProUGUI>().text = "Press F To Pick " + this.gameObject.tag;
            pickGunText.SetActive(true);
        }
        else
        {
            objectInTrigger.GetComponent<Stats>().AddFirstAid();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickGunText.SetActive(false);
        objectInTrigger = null;
    }
}
