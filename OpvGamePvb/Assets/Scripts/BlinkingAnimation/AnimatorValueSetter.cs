using UnityEngine;

//this class sets the isBlind bool on the blink animator and resets the play trigger
namespace BlinkingAnimation
{
    public class AnimatorValueSetter : MonoBehaviour
    {
        private Animator m_Animator;
        private static readonly int IsBlind = Animator.StringToHash("isBlind");
        private static readonly int PlayBlink = Animator.StringToHash("PlayBlink");

        private void Start()
        {
            m_Animator = GameObject.Find("Camera_Eyelids").GetComponent<Animator>();
        }

        private void SetBlind(int value)
        {
            switch (value)
            {
                case 0:
                    m_Animator.SetBool(IsBlind, false);
                    break;
                case 1: m_Animator.SetBool(IsBlind, true);
                    break;
            }
        }

        private void ResetTrigger()
        {
            m_Animator.ResetTrigger(PlayBlink);
        }
    }
}
