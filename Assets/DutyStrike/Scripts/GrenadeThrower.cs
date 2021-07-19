using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 15f;
    public GameObject grenadesInHand;
    public Text grenadesCountText;

    private int grenadesCount;

    private void Start()
    {
        grenadesCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject grenade = null;

        for (int i = 0; !grenade && i < grenadesInHand.transform.childCount; i++)
        {
            GameObject currentGrenade = grenadesInHand.transform.GetChild(i).gameObject;

            if (currentGrenade.activeInHierarchy && !currentGrenade.GetComponent<Grenade>().GetThrowGrenade())
                grenade = currentGrenade;
        }

        if (grenade && Input.GetButtonDown("ThrowGrenade"))
        {
            throwGrenade(grenade);
            grenade.GetComponent<Grenade>().SetThrowGrenade(true);
        }
    }

    void throwGrenade(GameObject thegGrenade)
    {
        GameObject grenade = Instantiate(thegGrenade, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        if (this.gameObject.tag == "Player")
        {
            grenadesCount = int.Parse(grenadesCountText.GetComponent<Text>().text) - 1;
            grenadesCountText.GetComponent<Text>().text = grenadesCount.ToString();
        }
    }
}
