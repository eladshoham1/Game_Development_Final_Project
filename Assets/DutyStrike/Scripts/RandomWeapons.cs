using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapons : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;

    Vector3 GetRandomPositin()
    {
        float x, y, z;
        x = Random.Range(-10, 100);
        z = Random.Range(-40, 40);
        //y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));
        y = 0;

        return new Vector3(x, y, z);
    }

    // Start is called before the first frame update
    void Start()
    {
        weapon1.transform.position = GetRandomPositin();
        weapon2.transform.position = GetRandomPositin();
        weapon3.transform.position = GetRandomPositin();
        weapon4.transform.position = GetRandomPositin();
        weapon5.transform.position = GetRandomPositin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
