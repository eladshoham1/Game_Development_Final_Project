using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickGrenade : MonoBehaviour
{
    public GameObject pickGunText;
    public GameObject grenadeImage;

    private GrenadeThrower grenadeThrower;
    private string tagOnTrigger;

    // Start is called before the first frame update
    void Start()
    {
        grenadeThrower = null;
        tagOnTrigger = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (tagOnTrigger == "Player" && Input.GetButtonDown("GunPickBtn"))
        {
            TakeGrenade();
            grenadeImage.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tagOnTrigger = other.tag;

        for (int i = 0; !grenadeThrower && i < other.transform.childCount; i++)
        {
            if (other.transform.GetChild(i).gameObject.name == "Camera")
                grenadeThrower = other.transform.GetChild(i).gameObject.GetComponent<GrenadeThrower>();
        }

        if (tagOnTrigger == "Player")
        {
            pickGunText.GetComponentInChildren<TextMeshProUGUI>().text = "Press F To Pick " + this.gameObject.tag;
            pickGunText.SetActive(true);
        }
        else if (tagOnTrigger == "NPC")
            TakeGrenade();

    }

    private void OnTriggerExit(Collider other)
    {
        pickGunText.SetActive(false);
        tagOnTrigger = null;
    }

    void TakeGrenade()
    {
        grenadeThrower.SetHaveGrenade(true);
        this.gameObject.SetActive(false);
        pickGunText.SetActive(false);
    }
}