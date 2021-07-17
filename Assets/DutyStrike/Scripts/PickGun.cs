using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGun : MonoBehaviour
{
    public GameObject weaponsInHand;
    public GameObject weaponsInField;
    public GameObject pickGunText;
    private bool inTrigger;
    //public GameObject aCamera;
    //public Image weaponImage;
    //public Sprite gun;

    // Start is called before the first frame update
    void Start()
    {
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        pickGunText.SetActive(inTrigger);

        if (inTrigger && Input.GetButtonDown("GunPickBtn"))
        {
            for (int i = 0; i < weaponsInField.transform.childCount; i++)
            {
                if (!weaponsInField.transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    weaponsInField.transform.GetChild(i).gameObject.transform.position = this.transform.position;
                    weaponsInField.transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            for (int i = 0; i < weaponsInHand.transform.childCount; i++)
            {
                weaponsInHand.transform.GetChild(i).gameObject.SetActive(weaponsInHand.transform.GetChild(i).transform.name == this.gameObject.name);
            }

            this.gameObject.SetActive(false);
            //weaponImage.sprite = gun;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }
}
