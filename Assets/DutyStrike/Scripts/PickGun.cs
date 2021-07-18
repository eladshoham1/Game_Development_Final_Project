using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGun : MonoBehaviour
{
    public GameObject weaponsInField;
    public GameObject pickGunText;
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
        if (tagOnTrigger == "Player" && Input.GetButtonDown("GunPickBtn"))
        {
            TakeWeapon();
            weaponImage.sprite = gun;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tagOnTrigger = null;

        if (other.tag == "Player")
        {
            pickGunText.GetComponentInChildren<Text>().text = "Press F To Pick " + this.gameObject.tag;
            pickGunText.SetActive(true);
            tagOnTrigger = other.tag;

            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).gameObject.name == "Camera")
                {
                    weaponsInHand = FindWeaponsInHand(other.transform.GetChild(i).gameObject);
                    break;
                }
            }
        }
        else if (other.tag == "NPC")
        {
            weaponsInHand = FindWeaponsInHand(other.gameObject);
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
        pickGunText.SetActive(false);
        weaponsInHand = null;
        tagOnTrigger = null;
    }

    GameObject FindWeaponsInHand(GameObject theObject)
    {
        for (int i = 0; i < theObject.transform.childCount; i++)
        {
            if (theObject.transform.GetChild(i).gameObject.name == "WeaponsInHand")
                return theObject.transform.GetChild(i).gameObject;
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
        pickGunText.SetActive(false);
    }
}
