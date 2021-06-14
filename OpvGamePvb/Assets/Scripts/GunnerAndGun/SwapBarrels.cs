using UnityEngine;

public class SwapBarrels : MonoBehaviour
{
    [SerializeField] private Transform _beginSetting1;
    [SerializeField] private Transform _endSetting1;
    [SerializeField] private Transform _beginSetting2;
    [SerializeField] private Transform _endSetting2;

    [SerializeField] private Transform _beginObject;
    [SerializeField] private Transform _endObject;

    [SerializeField] private GunMovement _gunMovement;
    
    private int _barrelIndex;

    private void Start()
    {
        _barrelIndex = 1;
        _gunMovement.Shoot += SwapBetween;
    }

    private void SwapBetween()
    {
        switch (_barrelIndex)
        {
            case 1:
                _beginObject.transform.position = _beginSetting1.position;
                _endObject.transform.position = _endSetting1.position;
                _barrelIndex = 2;
                break;
            case 2:
                _beginObject.transform.position = _beginSetting2.position;
                _endObject.transform.position = _endSetting2.position;
                _barrelIndex = 1;
                break;
            default: Debug.LogError("barrel index does not exist");
                break;
        }
    }
}
