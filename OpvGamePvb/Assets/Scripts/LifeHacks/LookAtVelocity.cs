using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LookAtVelocity : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up);
    }
}
