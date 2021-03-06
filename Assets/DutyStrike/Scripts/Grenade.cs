using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSMGameStudio.Behaviours;

public class Grenade : MonoBehaviour
{
    public GameObject explpsionEffect;
    private GameObject grenadeSound;

    private float delay = 3f;
    private float radius = 7f;
    private float force = 600f;
    private float countdown;
    private bool hasExploded;
    
    // Start is called before the first frame update
    void Start()
    {
        grenadeSound = GameObject.FindGameObjectWithTag("Grenade Sound");
        countdown = delay;
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            explode();
            hasExploded = true;
        }
    }

    void explode()
    {
        grenadeSound.GetComponent<AudioSource>().Play();
        Instantiate(explpsionEffect, transform.position, transform.rotation);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToDestroy)
        {
            if (nearbyObject.tag == "Player" || nearbyObject.tag == "NPC")
                nearbyObject.GetComponent<Stats>().HurtFromGrenade();
            else if (nearbyObject.tag == "Breakable" || nearbyObject.tag == "Breakable Wood")
                nearbyObject.GetComponent<Breakable>().Break();
            else if (nearbyObject.tag == "Terrain" && this.transform.position.y - nearbyObject.transform.position.y < 11f)
                nearbyObject.GetComponent<TerrainDamager>().ApplyDamage(this.transform.position);

            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.Destroy();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }
}
