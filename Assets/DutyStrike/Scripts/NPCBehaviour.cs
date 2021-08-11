using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject leader;
    public GameObject enemyLeader;
    public GameObject enemy;
    public GameObject weaponsInField;

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
        anim.SetInteger("NPCState", 2);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        GetFieldPosition();
        GenerateDest();
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

            if (this.GetComponent<NPCAttack>().HaveWeaponInHand() || this.GetComponent<NPCAttack>().HaveGrenade())
            {
                if (!enemyLeader.GetComponent<Stats>().IsDead() && Vector3.Distance(this.transform.position, enemyLeader.transform.position) <= 7f)
                    AttackEnemy(enemyLeader);
                else if (!enemy.GetComponent<Stats>().IsDead() && Vector3.Distance(this.transform.position, enemy.transform.position) <= 7f)
                    AttackEnemy(enemy);
            }
        }
    }

    private void GetFieldPosition()
    {
        GameObject field = GameObject.FindWithTag("Field");
        Renderer fieldSize = field.GetComponent<Renderer>();

        minX = fieldSize.bounds.center.x - fieldSize.bounds.extents.x;
        maxX = fieldSize.bounds.center.x + fieldSize.bounds.extents.x;
        minZ = fieldSize.bounds.center.z - fieldSize.bounds.extents.z;
        maxZ = fieldSize.bounds.center.z + fieldSize.bounds.extents.z;
    }

    private void SetDestPosition()
    {
        if (this.GetComponent<NPCAttack>().HaveWeaponInHand())
            GenerateDest();
        else
            GoToWeapon();
    }

    private void GenerateDest()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        dest = new Vector3(x, this.transform.position.y, z);
    }

    private void GoToWeapon()
    {
        float distance = 0f;
        Vector3 target = weaponsInField.transform.GetChild(0).gameObject.transform.position;

        for (int i = 0; i < weaponsInField.transform.childCount; i++)
        {
            if (weaponsInField.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                distance = Vector3.Distance(this.transform.position, weaponsInField.transform.GetChild(i).gameObject.transform.position);

                if (distance < Vector3.Distance(this.transform.position, target))
                    target = weaponsInField.transform.GetChild(i).gameObject.transform.position;
            }
        }

        dest = target;
    }

    private float GetDistance(Vector3 target)
    {
        return Vector3.Distance(this.transform.position, target);
    }

    private void FaceTarget(GameObject enemy)
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void AttackEnemy(GameObject enemy)
    {
        FaceTarget(enemy);
        agent.SetDestination(enemy.transform.position);
        this.GetComponent<NPCAttack>().Attack();
    }
}