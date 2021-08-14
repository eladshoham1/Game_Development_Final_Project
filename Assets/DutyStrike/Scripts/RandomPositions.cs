using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositions : MonoBehaviour
{
    private GameObject[] noItemsFields;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        GetFieldSize();
        GetNoItemsFields();

        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).transform.position = GetRandomPositin();
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

    private void GetNoItemsFields()
    {
        noItemsFields = GameObject.FindGameObjectsWithTag("No Items Field");
    }

    private Vector3 GetRandomPositin()
    {
        float x, z;

        do
        {
            x = Random.Range(minX, maxX);
            z = Random.Range(minZ, maxZ);
        }
        while (IsInNoItemsField(x, z));

        return new Vector3(x, 9.5f, z);
    }

    private bool IsInNoItemsField(float x, float z)
    {
        Renderer noItemsfieldSize = null;

        for (int i = 0; i < noItemsFields.Length; i++)
        {
            noItemsfieldSize = noItemsFields[i].GetComponent<Renderer>();

            if (x > noItemsfieldSize.bounds.center.x - noItemsfieldSize.bounds.extents.x &&
                x < noItemsfieldSize.bounds.center.x + noItemsfieldSize.bounds.extents.x &&
                z > noItemsfieldSize.bounds.center.z - noItemsfieldSize.bounds.extents.z &&
                z < noItemsfieldSize.bounds.center.z + noItemsfieldSize.bounds.extents.z)
                return true;
        }

        return false;
    }
}
