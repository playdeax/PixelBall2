using System;
using UnityEngine;

namespace BlueBall.Scripts.Enemys.Spike
{
    public class SawSound : MonoBehaviour
    {
        public AudioSource source;
        private void OnBecameVisible()
        {
            source.Play();   
        }

        private void OnBecameInvisible()
        {
            source.Stop();
        }
    }
}