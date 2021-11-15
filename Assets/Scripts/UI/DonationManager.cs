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
        //�ش� ������Ʈ�� �̵�
        Application.OpenURL(url);
    }
}
