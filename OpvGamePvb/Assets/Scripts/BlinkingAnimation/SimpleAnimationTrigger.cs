using UnityEngine;

//this is to show how to trigger said animation
public class SimpleAnimationTrigger : MonoBehaviour
{
    private Animator cameraEyelidBlink;
    void Start()
    {
        cameraEyelidBlink = GameObject.Find("Camera_Eyelids").GetComponent<Animator>();
        
        Control.OnSwitchKey += DoBlinkingAnim;
    }

    private void DoBlinkingAnim()
    {
        cameraEyelidBlink.SetTrigger("PlayBlink");
    }
}
