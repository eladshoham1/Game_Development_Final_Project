using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReplaceStatus : MonoBehaviour
{
    public GameObject player;
    public GameObject aCamera;
    public GameObject weaponsInHand;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI firstAidCountText;
    public GameObject grenade;
    public Slider greandeTime;
    public Image weaponImage;
    public Sprite m4;
    public Sprite grenadeLauncher;
    public Sprite pistol;
    public Sprite railGun;
    public Sprite shotgun;

    private Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = this.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Stats>().IsDead())
        {
            hpText.GetComponent<TextMeshProUGUI>().text = "" + stats.GetHP();
            firstAidCountText.GetComponent<TextMeshProUGUI>().text = "" + stats.GetNumOfFirstAid();
            grenade.SetActive(aCamera.GetComponent<GrenadeThrowerNPC>().GetHaveGrenade());
            greandeTime.value = aCamera.GetComponent<GrenadeThrowerNPC>().GetDelay();

            switch (GetNameOfWeaponInHand())
            {
                case "M4":
                    weaponImage.sprite = m4;
                    break;
                case "M79 Grenade Launcher":
                    weaponImage.sprite = grenadeLauncher;
                    break;
                case "Pistol":
                    weaponImage.sprite = pistol;
                    break;
                case "Rail Gun":
                    weaponImage.sprite = railGun;
                    break;
                case "Shotgun":
                    weaponImage.sprite = shotgun;
                    break;
            }
        }
    }

    private string GetNameOfWeaponInHand()
    {
        for (int i = 0; i < weaponsInHand.transform.childCount; i++)
        {
            if (weaponsInHand.transform.GetChild(i).gameObject.activeInHierarchy)
                return weaponsInHand.transform.GetChild(i).gameObject.name;
        }

        return "";
    }
}
