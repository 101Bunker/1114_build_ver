using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLocationChecker : MonoBehaviour
{
    [SerializeField] UI_QuestManager questManager;
    public List<GameObject> storeList = new List<GameObject>();
    int curInt;
    private void OnTriggerEnter(Collider other)
    {
        if (storeList.Contains(other.gameObject))
        {
            curInt = int.Parse(other.gameObject.tag);   //tag���� �ٸ� ��� �����غ���
            print($"quest{curInt}");
            questManager.questNum = curInt;
            questManager.QuestPopUpOpen();
            other.GetComponent<CapsuleCollider>().enabled = false;

            //�� ǥ�� �����
            for (int i = 0; i < other.transform.childCount; i++)
            {
                other.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
