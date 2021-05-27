using UnityEngine;

public class Floater : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _objectHeight = 1f;
    [SerializeField] private float _displacementAmount = 3f;
    [SerializeField] private float _setDrag = 1.5f;
    private float _waterHeight;
    
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.drag = _setDrag;
        _waterHeight = 0f;
    }

    private void FixedUpdate()
    {
        if (!(transform.position.y < _waterHeight)) return;
        var displacementMultiplier = Mathf.Clamp01(-transform.position.y / _objectHeight) * _displacementAmount;
        _rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y)*displacementMultiplier, 0f), ForceMode.Acceleration);
    }
}
