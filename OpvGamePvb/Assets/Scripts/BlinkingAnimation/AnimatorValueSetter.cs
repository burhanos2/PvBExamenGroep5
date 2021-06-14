using UnityEngine;
using System;

//this class sets the isBlind bool on the blink animator and resets the play trigger
namespace BlinkingAnimation
{
    public class AnimatorValueSetter : MonoBehaviour
    {
        public Action OnBlind;
        private Animator _animator;
        private static readonly int IsBlind = Animator.StringToHash("isBlind");
        private static readonly int PlayBlink = Animator.StringToHash("PlayBlink");

        private void Start()
        {
            _animator = GameObject.Find("Camera_Eyelids").GetComponent<Animator>();
        }

        private void SetBlind(int value)
        {
            switch (value)
            {
                case 0:
                    _animator.SetBool(IsBlind, false);
                    break;
                case 1: _animator.SetBool(IsBlind, true);
                    OnBlind?.Invoke();
                    break;
            }
        }

        private void ResetTrigger()
        {
            _animator.ResetTrigger(PlayBlink);
        }
    }
}
