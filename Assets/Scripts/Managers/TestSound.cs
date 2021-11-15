using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{

    public AudioClip eggTouch;

    private void OnTriggerEnter(Collider other)
    {
        Managers.Sound.Play(eggTouch);
    }
}
