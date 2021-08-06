using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : NPCBehaviour
{
    public GameObject leader;

    private Animator anim;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("NPCState", 2);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        GetFieldPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (leader.GetComponent<Stats>().IsDead())
            {
                agent.SetDestination(dest);
                if (Vector3.Distance(this.transform.position, dest) <= 4f)
                    SetDestPosition();
            }
            else
                agent.SetDestination(leader.transform.position);
        }
    }


}
