using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public GameObject playerMessage;
    public GameObject teammate;
    public GameObject statusCanvas;
    public GameObject gameoverCanvas;
    public MouseLook cameraLook;


    private float hp;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        hp = 100f;
        dead = false;
    }

    private void Update()
    {
        if (this.gameObject.tag == "Player")
            hpText.GetComponent<TextMeshProUGUI>().text = this.hp.ToString();
    }

    public float GetHP()
    {
        return this.hp;
    }

    public void SetHP(float hp)
    {
        this.hp = hp;
    }

    public bool IsDead()
    {
        return this.dead;
    }

    public void SetDead(bool dead)
    {
        this.dead = dead;
    }

    public void HeadShot(string shooter)
    {
        DicreaseHP(100f,shooter);
    }

    public void BodyShot(string shooter)
    {
        DicreaseHP(Random.Range(20, 30),shooter);
    }

    public void HurtFromGrenade()
    {
        DicreaseHP(Random.Range(60, 80),"Grenade");
    }

    private void IncreaseHP(float hp)
    {
        this.hp += hp;

        if (this.hp >= 100f)
            this.hp = 100f;
    }


    private void DicreaseHP(float hp, string shooter)
    {
        this.hp -= hp;

        if (this.hp <= 0f)
        {
            this.hp = 0f;
            if(teammate.GetComponent<Stats>().IsDead())
            {
                statusCanvas.SetActive(false);
                gameoverCanvas.SetActive(true);
                cameraLook.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                if (this.tag == "Player" || teammate.tag == "Player")
                    gameoverCanvas.transform.Find("Winner Status").GetComponent<TextMeshProUGUI>().text = "You're Lost GG WP";
                else
                    gameoverCanvas.transform.Find("Winner Status").GetComponent<TextMeshProUGUI>().text = "You're WIN!!!";
            }

            playerMessage.GetComponentInChildren<TextMeshProUGUI>().text = shooter + " kill " + this.transform.name;
            playerMessage.SetActive(true);
            SetDead(true);
        }
    }
}
