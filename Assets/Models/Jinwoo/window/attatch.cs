using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attatch : MonoBehaviour
{
    public GameObject equipItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // find the feet
        GameObject playerFeet = GameObject.FindWithTag("monitor");
        

        // activate the item
        //equipItem.gameObject.SetActive(true);

        // replacing the feet by the item
        equipItem.transform.parent = playerFeet.transform;

        //equipItem.transform.position = playerFeet.transform.position;
        //equipItem.transform.rotation = playerFeet.transform.rotation;
        //equipItem.transform.parent = playerFeet.transform.parent;

        // makes the feet vanish
        //playerFeet.SetActive(false);
    }
}
