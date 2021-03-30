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
        var cam1Temp = cam1.depth;
        cam1.depth = cam2.depth;
        cam2.depth = cam1Temp;
    }
}
