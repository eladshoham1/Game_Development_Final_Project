using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapons : MonoBehaviour
{
    public GameObject weapons;

    Vector3 GetRandomPositin()
    {
        float x = Random.Range(-10, 100);
        float z = Random.Range(-40, 40);

        return new Vector3(x, 0, z);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weapons.transform.childCount; i++)
            weapons.transform.GetChild(i).transform.position = GetRandomPositin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
