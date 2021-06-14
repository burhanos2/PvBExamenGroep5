using UnityEngine;

//this is to show how to trigger said animation
namespace TestScripts
{
    public class SimpleAnimationTrigger : MonoBehaviour
    {
        private Animator _cameraEyelidBlink;
        private static readonly int PlayBlink = Animator.StringToHash("PlayBlink");

        private void Start()
        {
            _cameraEyelidBlink = GameObject.Find("Camera_Eyelids").GetComponent<Animator>();
        
            Control.Instance.OnSwitchKey += DoBlinkingAnim;
        }

        private void DoBlinkingAnim()
        {
            _cameraEyelidBlink.SetTrigger(PlayBlink);
        }
    }
}
