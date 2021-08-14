using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 12f;
    public GameObject theGrenade;
    public Slider greandeTime;
    public GameObject statusCanvas;

    protected bool haveGrenade;
    protected float delay;

    private void Start()
    {
        haveGrenade = false;
        delay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!statusCanvas.activeInHierarchy)
            return;

        if (!this.transform.parent.GetComponent<Stats>().IsDead())
        {
            if (haveGrenade && delay >= 4f && Input.GetButtonDown("ThrowGrenade"))
            {
                throwGrenade();
                delay = 0f;
            }

            delay += Time.deltaTime;
            greandeTime.value = delay;
        }
    }

    public bool GetHaveGrenade()
    {
        return this.haveGrenade;
    }

    public void SetHaveGrenade(bool haveGrenade)
    {
        this.haveGrenade = haveGrenade;
    }

    protected void throwGrenade()
    {
        GameObject grenade = Instantiate(theGrenade, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
