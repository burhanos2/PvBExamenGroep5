using UnityEngine;

namespace Floaters
{
    [RequireComponent(typeof(Rigidbody))]
    public class Floater : MonoBehaviour
    {
        private Rigidbody _rb;
        [SerializeField] private float _objectHeight = 1f;
        [SerializeField] private float _displacementAmount = 3f;
        [SerializeField] private float _setDrag = 1f;
        private void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
            _rb.drag = _setDrag;
        }

        private void FixedUpdate()
        {
            var waveHeight = SineWaveManager.Instance.GetWaveHeight(transform.position.x);
            if (!(transform.position.y < waveHeight)) return;
            var displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / _objectHeight) * _displacementAmount;
            _rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y)*displacementMultiplier, 0f), ForceMode.Acceleration);
        }
    }
}
