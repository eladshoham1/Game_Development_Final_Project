using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHour : MonoBehaviour
{
    public Text hourText;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animationState = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] myAnimatorClip = anim.GetCurrentAnimatorClipInfo(0);
        float min = myAnimatorClip[0].clip.length * animationState.normalizedTime + 5 * 60;
        float hour = min / 60;
        min %= 60;
        hour %= 24;
        hourText.GetComponent<Text>().text = (hour < 10 ? "0" : "") + (int)hour + ":" + (min < 10 ? "0" : "") + (int)min;
    }
}
