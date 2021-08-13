using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pick : MonoBehaviour
{
    public GameObject player;
    public GameObject pickText;
    public GameObject soundObject;

    protected void ShowText(string objectTag)
    {
        pickText.GetComponentInChildren<TextMeshProUGUI>().text = "Press F To Pick " + objectTag;
        pickText.SetActive(true);
    }

    protected void PlaySound()
    {
        AudioSource sound = soundObject.GetComponent<AudioSource>();
        sound.Play();
    }
}
