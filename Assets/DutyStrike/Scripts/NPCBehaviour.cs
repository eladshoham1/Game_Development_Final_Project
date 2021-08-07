using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    public GameObject leader;
    public GameObject enemyLeader;
    public GameObject enemy;

    protected Vector3 dest;
    protected float minX;
    protected float maxX;
    protected float minZ;
    protected float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("NPCState", 2);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        dest = this.transform.position;

        GetFieldPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (!leader.GetComponent<Stats>().IsDead() && (this.gameObject.name == "NPC" || this.gameObject.name == "NPC2"))
                agent.SetDestination(leader.transform.position);
            else
            {
                agent.SetDestination(dest);
                if (Vector3.Distance(this.transform.position, dest) <= 3f)
                    SetDestPosition();
            }

            if (!enemyLeader.GetComponent<Stats>().IsDead() && Vector3.Distance(this.transform.position, enemyLeader.transform.position) <= 7f)
            {
                FaceTarget(enemyLeader);
                agent.SetDestination(enemyLeader.transform.position);
                this.GetComponent<NPCAttack>().Attack();
            } else if (!enemy.GetComponent<Stats>().IsDead() && Vector3.Distance(this.transform.position, enemy.transform.position) <= 7f)
            {
                FaceTarget(enemy);
                agent.SetDestination(enemy.transform.position);
                this.GetComponent<NPCAttack>().Attack();
            }
        }
    }

    protected void GetFieldPosition()
    {
        GameObject field = GameObject.FindWithTag("Field");
        Renderer fieldSize = field.GetComponent<Renderer>();

        minX = fieldSize.bounds.center.x - fieldSize.bounds.extents.x;
        maxX = fieldSize.bounds.center.x + fieldSize.bounds.extents.x;
        minZ = fieldSize.bounds.center.z - fieldSize.bounds.extents.z;
        maxZ = fieldSize.bounds.center.z + fieldSize.bounds.extents.z;
    }

    protected void SetDestPosition()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        dest = new Vector3(x, this.transform.position.y, z);
    }

    private void FaceTarget(GameObject enemy)
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
