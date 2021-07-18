using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapons : MonoBehaviour
{
    Vector3 GetRandomPositin()
    {
        float x = Random.Range(-20, 145);
        float z = Random.Range(-65, 50);

        return new Vector3(x, 0, z);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).transform.position = GetRandomPositin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
