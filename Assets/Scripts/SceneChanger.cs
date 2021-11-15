using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject agree, yourGame;

    public void ChangeScene()
    {
        // 현재 씬 정보를 가지고 온다.
        Scene scene = SceneManager.GetActiveScene();

        // 현재 씬의 빌드 순서를 가지고 온다.
        int curScene = scene.buildIndex;

        // 현재 씬 바로 다음씬을 가져오기 위해 index + 1 을 해준다.
        int nextScene = curScene + 1;

        // 다음 씬을 불러온다
        SceneManager.LoadScene(nextScene);
    }

    public void OnBtnDownNextPage()
    {
        agree.SetActive(false);
        agree.SetActive(true);
        yourGame.SetActive(false);
    }
}
