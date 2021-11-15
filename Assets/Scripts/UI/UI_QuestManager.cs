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

    //UI�׸�� ũ�� ���߱� ���� �߰��� //����� ����ÿ� �����൵ ������ ���÷��� ũ�⿡ ���� ���� �� �־ localScale �����ؾ���.
    Vector3 one = new Vector3(1, 1, 1);


    void Awake()
    {
        //����Ʈ���� ���� ĳ���͵� ����Ʈȭ
        ListingQuest();

        uiManager = GetComponent<UIManager>();
    }

    #region Quest �˾�/����/...
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

        //����Ʈ���� �̹����� �ؽ�Ʈ �ٲ���.
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

        //���� ĳ���� On
        for (int i = 0; i < questList[questNum].couponCharactors.Count; i++)
        {
            questList[questNum].couponCharactors[i].transform.parent.gameObject.SetActive(true);
        }

        //�гο� �ش� ����Ʈ �׸� �߰�
        GameObject inPanel = Instantiate(listInPanelPrefab);
        inPanel.transform.SetParent(ContentInPanel);
        inPanel.transform.localScale = one;
        //TMP ��� �� ������ ���� ������ Text�� ��ü��.
        inPanel.transform.GetChild(1).GetComponent<Text>().text = questTitle;

        toggleGrp_panel = inPanel.transform.GetChild(2).gameObject;

        //����Ʈ ��/����Ʈ �гο� ĳ���͸�ŭ ��� �� ������ֱ�
        for (int i = 0; i < questBar.transform.childCount; i++)
        {
            if (questBar.transform.GetChild(i).name == "Toggles")
            {
                GameObject toggleGrp = questBar.transform.GetChild(i).gameObject;

                //����Ʈ ���� ���ϴ� 3 , 3�� �̻��� ���� ��� ���� �÷��ֱ�
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
            //����Ʈ �гο��� ����Ʈ ������ ���+1
            questList[questNum].toggles.Add(toggleGrp_panel.transform.GetChild(i).GetComponent<Toggle>());
        }
    }

    int count;
    public void GotOne()
    {
        if (count < currentCount)
        {
            //����Ʈ �ٿ� ��� +1
            toggles[count].interactable = true;

            //����Ʈ �гο��� ����Ʈ ������ ���+1
            questList[questNum].toggles[count].interactable = true;
            count += 1;

            //����Ʈ �� ä��� ���� UI ����
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

                //�ݶ��̴��� ���� �͵� üũ(ĳ���͵��� �޽��ݶ��̴��� ������ ��(update���� hitinfo�� ��ġ�ϴ��� üũ�ؾ��ؼ�))
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
