using System;
using UnityEngine;

namespace Bipolar.Humanoid3D.Sound
{
    public class StepsSound : MonoBehaviour
    {
        public event Action OnSoundPlayed;

        [SerializeField]
        private Humanoid humanoid;
        [SerializeField]
        private float stepLength = 1;

        [SerializeField]
        private AudioSource audioSource;

        private float distance;
        public float Distance => distance;

        private void Reset()
        {
            humanoid = GetComponent<Humanoid>();
            audioSource = GetComponent<AudioSource>();
        }

        public void ResetDistance()
        {
            distance = 0;
        }

        private void Update()
        {
            distance += humanoid.Velocity.magnitude * Time.deltaTime;
            if (distance >= stepLength)
            {
                distance -= stepLength;
                PlayStepSound();
            }
        }

        private void PlayStepSound()
        {
            audioSource.Play();
            OnSoundPlayed?.Invoke();
        }
    }
}
