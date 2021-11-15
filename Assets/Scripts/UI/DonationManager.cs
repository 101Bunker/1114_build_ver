using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonationManager : MonoBehaviour
{
  public GameObject donationCollider;

    UIManager uIManager;
    void Start()
    {
        uIManager = GetComponent<UIManager>();
    }
    public void ClickedDonationObj()
    {
        uIManager.uiAnim.Play("PopUpDonotion_open");
    }
    public void GotoDonatoin(string url)
    {
        //해당 웹사이트로 이동
        Application.OpenURL(url);
    }
}
