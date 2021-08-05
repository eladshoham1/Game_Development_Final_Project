using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    //public GameObject playerTeam;
    //public GameObject aCamera;
    //public GameObject weaponsInHand;
    //public GameObject npc;
    public GameObject leader;
    public GameObject target;

    private Animator anim;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("NPCState", 0);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (leader.GetComponent<Stats>().IsDead())
                agent.SetDestination(target.transform.position);
            else
                agent.SetDestination(leader.transform.position);
        }
    }

    /*private void Shoot()
    {
        for (int i = 0; i < weaponsInHand.transform.childCount; i++)
        {
            if (weaponsInHand.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                for (int j = 0; j < playerTeam.transform.childCount; j++)
                {

                    if (Vector3.Distance(this.gameObject.transform.position, playerTeam.transform.GetChild(j).gameObject.transform.position) < 50f)
                    {
                        RaycastHit hit;

                        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
                        {

                            if (hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "NPC")
                            {
                                if (this.transform.parent.parent.parent.parent.name != hit.transform.parent.name)
                                    hit.transform.gameObject.GetComponent<Stats>().Shot(this.transform.parent.tag);
                            }
                        }
                    }
                }
                break;
            }
        }
    }*/
}
