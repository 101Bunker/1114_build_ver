using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public Animator uiAnim;
    UI_QuestManager questManager;
    DonationManager donationManager;

    public Text userName;

    [SerializeField] GameObject outdoor;
    [SerializeField] GameObject loadingUI;

    UserInfos userinfos;
    GetAccountInfoResult result;
    void Awake()
    {
        if (GameObject.Find("userInfo") != null)
        {
            userinfos = GameObject.Find("userInfo").GetComponent<UserInfos>();
            userName.text = $"{userinfos.userId}님";
        }
        uiAnim = GetComponent<Animator>();
        questManager = GetComponent<UI_QuestManager>();
        donationManager = GetComponent<DonationManager>();
    }


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;

        if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hitinfo))
        {
            if (Input.GetMouseButtonDown(0))
            {
                //쿠폰 캐릭터 선택했을 때
                if (questManager.questList[questManager.questNum].couponCharactors.Contains(hitinfo.transform.gameObject))
                {
                    hitinfo.transform.parent.GetComponent<ParticleManager>().PlayParticles();
                    hitinfo.transform.gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(hitinfo.transform.parent.transform.gameObject, 0.5f);
                    Managers.Sound.Play("eggTouch1", SoundManager.SoundType.Effect);
                    questManager.GotOne();
                }

                //사랑의 열매 클릭했을 때
                if (hitinfo.transform.gameObject == donationManager.donationCollider)
                {
                    donationManager.ClickedDonationObj();
                    Managers.Sound.Play("bellTouch", SoundManager.SoundType.Effect);
                }
            }
        }


        if (outdoor.activeSelf)
        {
            loadingUI.SetActive(false);
        }
    }
    #region Button Functions
    bool clickedQuest;
    bool clickedInven;
    bool clickedMy;

    public GameObject[] panels;

    public void OnClickQuest()
    {
        clickedQuest = !clickedQuest;
        clickedInven = false;
        clickedMy = false;
        if (clickedQuest)
        {
            uiAnim.Play("Panel_Quest_up");
        }
        else
        {
            uiAnim.Play("Panel_Quest_down");
        }
    }
    public void OnClickInventory()
    {
        clickedInven = !clickedInven;
        clickedQuest = false;
        clickedMy = false;
        if (clickedInven)
        {
            uiAnim.Play("Panel_Inventory_up");
        }
        else
        {
            uiAnim.Play("Panel_Inventory_down");
        }
    }
    public void OnClickMy()
    {
        clickedMy = !clickedMy;
        clickedInven = false;
        clickedQuest = false;
        if (clickedMy)
        {
            uiAnim.Play("Panel_My_up");
        }
        else
        {
            uiAnim.Play("Panel_My_down");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

}
