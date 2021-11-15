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
            curInt = int.Parse(other.gameObject.tag);   //tag말고 다른 방법 생각해보기
            print($"quest{curInt}");
            questManager.questNum = curInt;
            questManager.QuestPopUpOpen();
            other.GetComponent<CapsuleCollider>().enabled = false;

            //별 표시 지우기
            for (int i = 0; i < other.transform.childCount; i++)
            {
                other.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
