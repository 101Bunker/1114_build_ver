using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickQuestList : MonoBehaviour
{
    Animator uiAnim;
    private void Awake()
    {
        uiAnim = GameObject.Find("Canvas").GetComponent<Animator>();
    }
    public void OnClick(string anim)
    {
        uiAnim.Play(anim);
    }
    public void OnClickInInventory()
    {
        GameObject go = GameObject.Find("Canvas").transform.Find("Panels").transform.Find("Coupon_1").gameObject;

        go.SetActive(true);
    }
}
