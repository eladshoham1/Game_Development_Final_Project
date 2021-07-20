using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public TextMeshProUGUI hpText;

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

        IncreaseHP(0.1f);
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

    public void HeadShot()
    {
        DicreaseHP(100f);
    }

    public void BodyShot()
    {
        DicreaseHP(25f);
    }

    private void IncreaseHP(float hp)
    {
        this.hp += hp;

        if (this.hp >= 100f)
            this.hp = 100f;
    }

    private void DicreaseHP(float hp)
    {
        this.hp -= hp;

        if (this.hp <= 0f)
        {
            this.hp = 0f;
            SetDead(true);
        }
    }
}
