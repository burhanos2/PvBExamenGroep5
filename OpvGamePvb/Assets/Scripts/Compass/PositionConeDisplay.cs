using System.Collections.Generic;
using UnityEngine;

public class PositionConeDisplay : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> _cones;
    
    [SerializeField]
    private List<Transform> _rotationPoints; // 0 is gun1, 1 is gun2

    public int _coneState;

   private void Update()
    {
        for (var i = 0; i < _cones.Count; i++)
        {
            _cones[i].localRotation = new Quaternion(_rotationPoints[i].rotation.x,_rotationPoints[i].rotation.z,-_rotationPoints[i].rotation.y,_rotationPoints[i].rotation.w);
            if (_coneState == 2)
            {
                _cones[1].Rotate(0,0,90f);
            }
        }
    }
}
