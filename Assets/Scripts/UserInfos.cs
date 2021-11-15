using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfos : MonoBehaviour
{
    [SerializeField]public string userId;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }    
}
