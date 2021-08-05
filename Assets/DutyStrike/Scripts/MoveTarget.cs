using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        GameObject field = GameObject.FindWithTag("Field");
        Renderer fieldSize = field.GetComponent<Renderer>();

        minX = fieldSize.bounds.center.x - fieldSize.bounds.extents.x;
        maxX = fieldSize.bounds.center.x + fieldSize.bounds.extents.x;
        minZ = fieldSize.bounds.center.z - fieldSize.bounds.extents.z;
        maxZ = fieldSize.bounds.center.z + fieldSize.bounds.extents.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
            SetTargetPosition();
    }

    private void SetTargetPosition()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        this.transform.position = new Vector3(x, 9f, z);
    }

}
