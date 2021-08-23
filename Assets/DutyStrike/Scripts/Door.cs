using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject authorizedAccess;

    private Animator anim;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAuthorizedAccess(other.name))
        {
            anim.SetBool("IsOpen", true);

            if (other.gameObject.name == "Player" && !sound.isPlaying)
                sound.PlayDelayed(0.3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsAuthorizedAccess(other.name))
        {
            anim.SetBool("IsOpen", false);

            if (other.gameObject.name == "Player" && !sound.isPlaying)
                sound.PlayDelayed(0.3f);
        }
    }

    private bool IsAuthorizedAccess(string name)
    {
        for (int i = 0; i < authorizedAccess.transform.childCount; i++)
        {
            if (name.Equals(authorizedAccess.transform.GetChild(i).gameObject.name))
                return true;
        }

        return false;
    }
}
