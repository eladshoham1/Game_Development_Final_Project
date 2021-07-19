using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGrenade : MonoBehaviour
{
    public GameObject grenadesInField;
    public GameObject pickGunText;
    public Text grenadesCountText;

    private GameObject grenadesInHand;
    private string tagOnTrigger;
    private int grenadesCount;

    // Start is called before the first frame update
    void Start()
    {
        grenadesInHand = null;
        tagOnTrigger = null;
        grenadesCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tagOnTrigger == "Player" && Input.GetButtonDown("GunPickBtn"))
        {
            TakeGrenade();
            grenadesCount = int.Parse(grenadesCountText.GetComponent<Text>().text) + 1;
            grenadesCountText.GetComponent<Text>().text = grenadesCount.ToString();
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
                    grenadesInHand = FindGrenadesInHand(other.transform.GetChild(i).gameObject);
                    break;
                }
            }
        }
        else if (other.tag == "NPC")
        {
            grenadesInHand = FindGrenadesInHand(other.gameObject);
            for (int i = 0; i < grenadesInHand.transform.childCount; i++)
                TakeGrenade();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickGunText.SetActive(false);
        grenadesInHand = null;
        tagOnTrigger = null;
    }

    GameObject FindGrenadesInHand(GameObject theObject)
    {
        for (int i = 0; i < theObject.transform.childCount; i++)
        {
            if (theObject.transform.GetChild(i).gameObject.name == "GrenadesInHand")
                return theObject.transform.GetChild(i).gameObject;
        }

        return null;
    }

    void TakeGrenade()
    {
        for (int i = 0; i < grenadesInHand.transform.childCount; i++)
        {
            if (this.gameObject.name == grenadesInHand.transform.GetChild(i).gameObject.name)
                grenadesInHand.transform.GetChild(i).gameObject.SetActive(true);
        }

        this.gameObject.SetActive(false);
        pickGunText.SetActive(false);
    }
}