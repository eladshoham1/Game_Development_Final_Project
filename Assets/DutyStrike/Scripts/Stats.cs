using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI firstAidCountText;
    public GameObject profile;
    public GameObject target;
    public GameObject weaponsInHand;
    public GameObject weaponsInField;

    private Animator anim;
    private float hp;
    private int numOfFirstAids;
    private bool dead;

    private NavMeshAgent nma;

    void Start()
    {
        anim = GetComponent<Animator>();
        hp = 100f;
        numOfFirstAids = 0;
        dead = false;

        if (this.gameObject.tag == "NPC")
            nma = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
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
        if (dead)
        {
            anim.SetInteger("NPCState", 4);
            profile.GetComponent<RawImage>().color = new Color32(0, 0, 0, 120);
            DropWeapon();

            if (this.gameObject.tag == "Player")
                target.SetActive(false);
            else if (this.gameObject.tag == "NPC")
                nma.enabled = false;
        }

        this.dead = dead;
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
        if (!dead)
            SetHP(this.hp - hp);
    }

    private void DropWeapon()
    {
        for (int i = 0; i < weaponsInHand.transform.childCount; i++)
        {
            if (weaponsInHand.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                weaponsInHand.transform.GetChild(i).gameObject.SetActive(false);
                return;
            }
        }
    }
}