using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_QuestManager : MonoBehaviour
{
    //[SerializeField] GameObject arCamera;
    public List<Quest> questList = new List<Quest>();

    [Header("Quest UI")]
    public GameObject questPopUp;
    public GameObject questPopUpClear;
    public GameObject questBar;
    public GameObject toggleGrp_bar;
    List<GameObject> toggleList = new List<GameObject>();
    List<Toggle> toggles = new List<Toggle>();

    [Header("Quest Panel")]
    public Transform ContentInPanel;
    public GameObject listInPanelPrefab;
    GameObject toggleGrp_panel;

    [Header("Quest Inventory")]
    public Transform contentInInventory;
    public GameObject inventoryList;

    UIManager uiManager;

    //UI항목들 크기 맞추기 위해 추가함 //모바일 빌드시엔 안해줘도 되지만 디스플레이 크기에 따라 변할 수 있어서 localScale 수정해야함.
    Vector3 one = new Vector3(1, 1, 1);


    void Awake()
    {
        //퀘스트마다 쿠폰 캐릭터들 리스트화
        ListingQuest();

        uiManager = GetComponent<UIManager>();
    }

    #region Quest 팝업/수락/...
    int currentCount;

    string questTitle;
    string questClearTitle;
    Sprite questImage;
    public int questNum;
    public void QuestPopUpOpen()
    {
        uiManager.uiAnim.Play("PopUpQuest_open");

        Managers.Sound.Play("popup1", SoundManager.SoundType.Effect);

        questTitle = questList[questNum].title;
        questClearTitle = questList[questNum].clearTitle;
        questImage = questList[questNum].image;

        //퀘스트마다 이미지랑 텍스트 바꿔줌.
        questPopUp.transform.GetChild(1).GetComponent<Text>().text = questTitle;
        questPopUpClear.transform.GetChild(1).GetComponent<Text>().text = questClearTitle;
        questPopUp.transform.GetChild(2).GetComponent<Image>().sprite = questImage;
        questPopUpClear.transform.GetChild(2).GetComponent<Image>().sprite = questImage;

        currentCount = questList[questNum].couponCharactors.Count;
    }
    public void OnClickAcceptQuest()
    {
        // questAccept = true;
        questBar.SetActive(true);
        questBar.transform.GetChild(1).GetComponent<Text>().text = questTitle;

        //쿠폰 캐릭터 On
        for (int i = 0; i < questList[questNum].couponCharactors.Count; i++)
        {
            questList[questNum].couponCharactors[i].transform.parent.gameObject.SetActive(true);
        }

        //패널에 해당 퀘스트 항목 추가
        GameObject inPanel = Instantiate(listInPanelPrefab);
        inPanel.transform.SetParent(ContentInPanel);
        inPanel.transform.localScale = one;
        //TMP 사용 시 깨지는 글자 때문에 Text로 대체함.
        inPanel.transform.GetChild(1).GetComponent<Text>().text = questTitle;

        toggleGrp_panel = inPanel.transform.GetChild(2).gameObject;

        //퀘스트 바/퀘스트 패널에 캐릭터만큼 토글 수 만들어주기
        for (int i = 0; i < questBar.transform.childCount; i++)
        {
            if (questBar.transform.GetChild(i).name == "Toggles")
            {
                GameObject toggleGrp = questBar.transform.GetChild(i).gameObject;

                //퀘스트 조건 최하는 3 , 3개 이상일 때만 토글 갯수 늘려주기
                if (currentCount > 3)
                {
                    for (int j = 0; j < currentCount - 3; j++)
                    {
                        GameObject toggle = Instantiate(toggleGrp.transform.GetChild(0).gameObject);
                        toggle.transform.SetParent(toggleGrp.transform);
                        toggle.transform.localScale = one;
                        GameObject toggle_panel = Instantiate(toggleGrp_panel.transform.GetChild(0).gameObject);
                        toggle_panel.transform.SetParent(toggleGrp_panel.transform);
                        toggle_panel.transform.localScale = one;
                    }
                }
            }
        }
        for (int i = 0; i < currentCount; i++)
        {
            toggles.Add(toggleGrp_bar.transform.GetChild(i).GetComponent<Toggle>());
            //퀘스트 패널에서 퀘스트 각각에 토글+1
            questList[questNum].toggles.Add(toggleGrp_panel.transform.GetChild(i).GetComponent<Toggle>());
        }
    }

    int count;
    public void GotOne()
    {
        if (count < currentCount)
        {
            //퀘스트 바에 토글 +1
            toggles[count].interactable = true;

            //퀘스트 패널에서 퀘스트 각각에 토글+1
            questList[questNum].toggles[count].interactable = true;
            count += 1;

            //퀘스트 다 채우면 쿠폰 UI 나옴
            if (count == currentCount)
            {
                uiManager.uiAnim.Play("PopUpQuestClear_open");

                GameObject list_Inventory = Instantiate(inventoryList);
                list_Inventory.transform.SetParent(contentInInventory);
                list_Inventory.transform.localScale = one;
            }
        }
    }
    #endregion
    void ListingQuest()
    {
        for (int questListCount = 0; questListCount < questList.Count; questListCount++)
        {
            for (int i = 0; i < questList[questListCount].locationObj.transform.childCount; i++)
            {
                //questList[questListCount].couponCharactors.Add(questList[questListCount].locationObj.transform.GetChild(i).transform.gameObject);
                //questList[questListCount].couponCharactors[i].transform.parent.gameObject.SetActive(false);

                //콜라이더를 가진 것들 체크(캐릭터들은 메쉬콜라이더를 가져야 함(update에서 hitinfo랑 일치하는지 체크해야해서))
                for (int j = 0; j < questList[questListCount].locationObj.transform.GetChild(j).transform.childCount; j++)
                {
                    if (questList[questListCount].locationObj.transform.GetChild(j).transform.GetChild(j).GetComponent<MeshCollider>() != null)
                    {
                        questList[questListCount].couponCharactors.Add(questList[questListCount].locationObj.transform.GetChild(i).transform.GetChild(j).gameObject);
                        questList[questListCount].locationObj.transform.GetChild(i).transform.GetChild(j).transform.parent.gameObject.SetActive(false);
                    }
                }

            }
        }
    }
    public void ClickedInPanel()
    {
        uiManager.uiAnim.Play("QuestBarUp");
    }
}
