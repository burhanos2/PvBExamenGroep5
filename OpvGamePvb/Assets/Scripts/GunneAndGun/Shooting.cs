using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private CameraShake _cameraShake; // throw camera here in the editor (after you've added camerashake to camera)
    
    [SerializeField]
    private GunMovement _gunMovement;

    [SerializeField]
    private GameObject _bulletObject;

    [SerializeField] 
    private GameObject _barrelEnd;
    
    [SerializeField]
    private float _waitingtime = 2;
    private bool _shootable = true;

    // Start is called before the first frame update
    private void Start()
    {
        _gunMovement.Shoot += Blast;
    }

    private void Blast()
    {
        StartCoroutine(Shoot());
        StartCoroutine(_cameraShake.CamShake(0.15f, 0.2f));
    }

    private IEnumerator Shoot()
    {
        if (_shootable)
        {
            Instantiate(_bulletObject,
                new Vector3(_barrelEnd.transform.position.x, _barrelEnd.transform.position.y,
                    _barrelEnd.transform.position.z),
                Quaternion.Euler(_gunMovement.barrelObject.transform.rotation.x,
                    _gunMovement.gunObject.transform.rotation.y, 0));

            StartCoroutine(inputDelay());
            yield return null;
        }
    }
    
    private IEnumerator inputDelay()
    {
        _shootable = false;

        yield return new WaitForSeconds(_waitingtime);

        _shootable = true;
        
        yield return null;
    }
}


