using UnityEngine;

//this is to show how to trigger said animation
namespace TestScripts
{
    public class SimpleAnimationTrigger : MonoBehaviour
    {
        private Animator cameraEyelidBlink;
        private static readonly int PlayBlink = Animator.StringToHash("PlayBlink");

        private void Start()
        {
            cameraEyelidBlink = GameObject.Find("Camera_Eyelids").GetComponent<Animator>();
        
            Control.OnSwitchKey += DoBlinkingAnim;
        }

        private void DoBlinkingAnim()
        {
            cameraEyelidBlink.SetTrigger(PlayBlink);
        }
    }
}
