using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PositionConeDisplay))]
public class ConeActiveHandler : MonoBehaviour
{   
    [SerializeField]
    private List<GameObject> _cones;

    [SerializeField] private RectTransform _directions;
    [SerializeField] private Vector3 _normalPosition;
    [SerializeField] private Vector3 _flippedPosition;
    private Vector3 _rotation1;
    private Vector3 _rotation2;

    private PositionConeDisplay _positionConeDisplay;
    
    public static ConeActiveHandler Instance;
    
    private void Awake()
    {
        Instance = this;
        _rotation1 = new Vector3(0, 0, -90f);
        _rotation2 = new Vector3(0, 180, -90f);
    }

    private void Start()
    {
        _positionConeDisplay = GetComponent<PositionConeDisplay>();
        ChangeActivity(0);
    }

    public void ChangeActivity(int newActive)
    {
        switch (newActive)
        {
            case 0: //radar Captain.cs
                gameObject.SetActive(false);
                _directions.localPosition = _normalPosition;
                _directions.transform.localEulerAngles = _rotation1;
                _positionConeDisplay._coneState = 0;
                break;
            
            case 1: // gun 1 Cannoneer.cs
                gameObject.SetActive(true);
                
                _cones[0].SetActive(true);
                _cones[1].SetActive(false);
                
                _directions.localPosition = _normalPosition;
                _directions.transform.localEulerAngles = _rotation1;
                _positionConeDisplay._coneState = 1;
                break;
            
            case 2: // gun 1 Grenade.cs
                gameObject.SetActive(true);
                
                _cones[0].SetActive(false);
                _cones[1].SetActive(true);
                
                _directions.localPosition = _flippedPosition;
                _directions.transform.localEulerAngles = _rotation2;
                _positionConeDisplay._coneState = 2;
                break;
            
            default:
                Debug.LogError("index does not exist at ConeActiveHandler.cs!");
                break;
        }
    }
}
