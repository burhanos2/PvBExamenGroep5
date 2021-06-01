using UnityEngine;
using System;

//this class sets the isBlind bool on the blink animator and resets the play trigger
namespace BlinkingAnimation
{
    public class AnimatorValueSetter : MonoBehaviour
    {
        public Action OnBlind;
        private Animator animator;
        private static readonly int IsBlind = Animator.StringToHash("isBlind");
        private static readonly int PlayBlink = Animator.StringToHash("PlayBlink");

        private void Start()
        {
            animator = GameObject.Find("Camera_Eyelids").GetComponent<Animator>();
        }

        private void SetBlind(int value)
        {
            switch (value)
            {
                case 0:
                    animator.SetBool(IsBlind, false);
                    break;
                case 1: animator.SetBool(IsBlind, true);
                    OnBlind?.Invoke();
                    break;
            }
        }

        private void ResetTrigger()
        {
            animator.ResetTrigger(PlayBlink);
        }
    }
}
