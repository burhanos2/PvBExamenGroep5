using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GunMovement _gunMovement;

    [SerializeField]
    private GameObject _bulletObject;

    [SerializeField] 
    private GameObject _barrelEnd;

    // Start is called before the first frame update
    private void Start()
    {
        _gunMovement.Shoot += Blast;
    }

    private void Blast()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        Instantiate(_bulletObject, new Vector3(_barrelEnd.transform.position.x, _barrelEnd.transform.position.y, _barrelEnd.transform.position.z),
            Quaternion.Euler(_gunMovement.barrelObject.transform.rotation.x, _gunMovement.gunObject.transform.rotation.y, 0));
        yield return null;
    }
}

