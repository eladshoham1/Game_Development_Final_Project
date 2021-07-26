using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI firstAidCountText;
    public GameObject playerMessage;

    private Animator anim;
    private float hp;
    private int numOfFirstAids;
    private bool dead;
    private bool deadNow;
    private float delay;

    void Start()
    {
        anim = GetComponent<Animator>();
        hp = 100f;
        numOfFirstAids = 0;
        dead = false;
    }

    private void Update()
    {
        PrintMessage();

        if (this.tag == "Player")
        {
            if (Input.GetButtonDown("FirstAid"))
                UseFirstAid();
        }
        else if (this.gameObject.GetComponent<Stats>().GetHP() < 50f)
            UseFirstAid();
    }

    public float GetHP()
    {
        return this.hp;
    }

    public void SetHP(float hp)
    {
        if (hp >= 100f)
        {
            this.hp = 100f;
        }
        else if (hp < 0f)
        {
            this.hp = 0f;
            SetDead(true);
        }
        else
        {
            this.hp = hp;
        }

        if (this.tag == "Player")
            hpText.GetComponent<TextMeshProUGUI>().text = this.hp.ToString();
    }

    public int GetNumOfFirstAid()
    {
        return this.numOfFirstAids;
    }

    public void setNumOfFirstAid(int numOfFirstAids)
    {
        this.numOfFirstAids = numOfFirstAids;

        if (this.tag == "Player")
            firstAidCountText.GetComponent<TextMeshProUGUI>().text = this.numOfFirstAids.ToString();
    }

    public bool IsDead()
    {
        return this.dead;
    }

    public void SetDead(bool dead)
    {
        this.dead = dead;

        if (this.dead)
            anim.SetInteger("NPCState", 4);
    }

    public void AddFirstAid()
    {
        setNumOfFirstAid(this.numOfFirstAids + 1);
    }

    public void UseFirstAid()
    {
        if (this.numOfFirstAids > 0)
        {
            IncreaseHP(Random.Range(40, 60));
            setNumOfFirstAid(this.numOfFirstAids - 1);
        }
    }

    public void Shot(string shooter)
    {
        DicreaseHP(Random.Range(20, 60), shooter);
    }

    public void HurtFromGrenade()
    {
        DicreaseHP(Random.Range(60, 80), "Grenade");
    }

    private void IncreaseHP(float hp)
    {
        SetHP(this.hp + hp);
    }

    private void DicreaseHP(float hp, string shooter)
    {
        SetHP(this.hp - hp);
        PlayerKilled(shooter);
    }

    private void PrintMessage()
    {
        if (deadNow)
        {
            delay += Time.deltaTime;

            if (delay >= 5f)
            {
                playerMessage.SetActive(false);
                deadNow = false;
                delay = 0f;
            }
        }
    }

    private void PlayerKilled(string shooter)
    {
        if (this.IsDead())
        {
            playerMessage.GetComponentInChildren<TextMeshProUGUI>().text = shooter + " kill " + this.transform.name;
            playerMessage.SetActive(true);
            deadNow = true;
        }
    }
}