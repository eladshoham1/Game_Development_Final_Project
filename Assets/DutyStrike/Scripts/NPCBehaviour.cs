using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private Vector3 dest;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("NPCState", 0);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        dest = agent.transform.position;

        GetFieldSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (agent.transform.position == dest)
                SetAgentDest();
        }
    }

    private void GetFieldSize()
    {
        GameObject field = GameObject.FindWithTag("Field");
        Renderer fieldSize = field.GetComponent<Renderer>();

        minX = fieldSize.bounds.center.x - fieldSize.bounds.extents.x;
        maxX = fieldSize.bounds.center.x + fieldSize.bounds.extents.x;
        minZ = fieldSize.bounds.center.z - fieldSize.bounds.extents.z;
        maxZ = fieldSize.bounds.center.z + fieldSize.bounds.extents.z;
    }

    private void SetAgentDest()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        dest = new Vector3(x, 0f, z);
        agent.SetDestination(dest);
    }
}
