using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]private Camera cam1, cam2;
    private void Start()
    {
        Control.OnSwitchKey += SwitchCameraDepths;
    }

    private void SwitchCameraDepths() // this temp method merely illustrates how to swap cams
    {
        cam1.enabled = !cam1.enabled;
        //add effect here
        cam2.enabled = !cam1.enabled;
    }
}
