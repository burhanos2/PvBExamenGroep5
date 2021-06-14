using System.Collections;
using UnityEngine;
using SoundSystem;

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
    private CharacterState _characterState;

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
        if (_characterState._active)
        {
            Instantiate(_bulletObject,
                new Vector3(_barrelEnd.transform.position.x, _barrelEnd.transform.position.y,
                    _barrelEnd.transform.position.z),
                Quaternion.Euler(_gunMovement._barrelObject.transform.rotation.x,
                    _gunMovement._gunObject.transform.rotation.y, 0));
            AudioManager.Instance.PlayRandomSfxVariant(SfxTypes.CannonShot);
            StartCoroutine(_cameraShake.CamShake(0.5f, 0.2f));
            yield return null;
        }
    }
}


