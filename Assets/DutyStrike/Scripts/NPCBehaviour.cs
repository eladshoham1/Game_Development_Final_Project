using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject playerTeam;
    public GameObject aCamera;
    public GameObject weaponsInHand;
    public GameObject npc;

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
            //if (agent.transform.position == dest)
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

        dest = new Vector3(x, this.transform.position.y, z);
        agent.SetDestination(dest);
    }

    private void Shoot()
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
    }
}
