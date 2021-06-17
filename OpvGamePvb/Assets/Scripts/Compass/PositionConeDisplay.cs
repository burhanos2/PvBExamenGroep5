using System.Collections.Generic;
using UnityEngine;

public class PositionConeDisplay : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> _cones;
     [SerializeField]
    private List<Transform> _rotationPoints;

   private void Update()
    {
        for (int i = 0; i < _cones.Count; i++)
        {
            _cones[i].localRotation = new Quaternion(_rotationPoints[i].rotation.x,_rotationPoints[i].rotation.z,-_rotationPoints[i].rotation.y,_rotationPoints[i].rotation.w);
            
        }
            
        
    }
}
