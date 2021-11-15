using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public string title;
    public string clearTitle;
    public Sprite image;
    public GameObject locationObj;
   // public GameObject locationTrigger;
    public List<Toggle> toggles = new List<Toggle>();
    public  List<GameObject> couponCharactors = new List<GameObject>();
    public Quest(string title, string clearTitle, Sprite image, GameObject locationObj, /*GameObject locationTrigger,*/List<Toggle> toggles, List<GameObject> couponCharactors)
    {
        this.title = title;
        this.clearTitle = clearTitle;
        this.image = image;
        this.locationObj = locationObj;
      //  this.locationTrigger = locationTrigger;
        this.toggles = toggles;
        this.couponCharactors = couponCharactors;
    }
}
