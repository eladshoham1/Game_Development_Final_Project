using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGun : Pick
{
    public GameObject weaponsInField;
    public GameObject target;
    public Sprite gun;
    public Image weaponImage;

    private GameObject weaponsInHand;
    private string tagOnTrigger;
    private bool npcTakeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        weaponsInHand = null;
        tagOnTrigger = null;
        npcTakeWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Stats>().IsDead())
        {
            pickText.SetActive(false);
            weaponsInHand = null;
            tagOnTrigger = null;
            return;
        }

        if (tagOnTrigger == "Player" && Input.GetButtonDown("GunPickBtn"))
        {
            TakeWeapon();
            PlaySound();
            weaponImage.sprite = gun;
            target.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tagOnTrigger = null;
        weaponsInHand = FindWeaponsInHand(other.gameObject);

        if (!weaponsInHand)
            return;

        if (other.tag == "Player")
        {
            ShowText(this.gameObject.tag);
            tagOnTrigger = other.tag;
        }
        else if (other.tag == "NPC")
        {
            for (int i = 0; i < weaponsInHand.transform.childCount; i++)
            {
                if (!npcTakeWeapon)
                {
                    TakeWeapon();
                    npcTakeWeapon = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickText.SetActive(false);
        weaponsInHand = null;
        tagOnTrigger = null;
        npcTakeWeapon = false;
    }

    GameObject FindWeaponsInHand(GameObject theObject)
    {
        GameObject camera = null;

        for (int i = 0; i < theObject.transform.childCount; i++)
        {
            if (theObject.transform.GetChild(i).gameObject.name == "Camera")
            {
                camera = theObject.transform.GetChild(i).gameObject;
                break;
            }
        }

        if (!camera)
            return null;

        for (int i = 0; i < camera.transform.childCount; i++)
        {
            if (camera.transform.GetChild(i).gameObject.name == "WeaponsInHand")
                return camera.transform.GetChild(i).gameObject;
        }

        return null;
    }

    void TakeWeapon()
    {
        string currentWeaponInHand = "";

        for (int i = 0; currentWeaponInHand == "" && i < weaponsInHand.transform.childCount; i++)
        {
            if (weaponsInHand.transform.GetChild(i).gameObject.activeInHierarchy)
                currentWeaponInHand = weaponsInHand.transform.GetChild(i).gameObject.name;
        }

        if (currentWeaponInHand != "")
        {
            for (int i = 0; i < weaponsInField.transform.childCount; i++)
            {
                if (currentWeaponInHand == weaponsInField.transform.GetChild(i).gameObject.name)
                {
                    weaponsInField.transform.GetChild(i).gameObject.transform.position = this.transform.position;
                    weaponsInField.transform.GetChild(i).gameObject.SetActive(true);
                    break;
                }
            }
        }

        for (int i = 0; i < weaponsInHand.transform.childCount; i++)
            weaponsInHand.transform.GetChild(i).gameObject.SetActive(this.gameObject.name == weaponsInHand.transform.GetChild(i).transform.name);

        this.gameObject.SetActive(false);
        pickText.SetActive(false);
    }
}
