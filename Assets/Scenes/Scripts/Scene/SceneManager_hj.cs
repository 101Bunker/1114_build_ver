using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_hj : MonoBehaviour

{
    //[SerializeField] private(string level)
    public void ButtonMoveScene(string level)
    {
        SceneManager.LoadScene(level);
    }
}






    //public void LoadLevel()
    //{
    //    // load the nextlevel
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    //}