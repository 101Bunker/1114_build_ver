using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
  public void PlayParticles()
    {
        particles.Play();
    }
}
