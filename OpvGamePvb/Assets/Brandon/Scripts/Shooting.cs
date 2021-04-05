using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GunMovement gunMovement;

    [SerializeField]
    private GameObject bulletObject;

    [SerializeField] 
    private GameObject barrelEnd;

    // Start is called before the first frame update
    private void Start()
    {
        gunMovement.Shoot += Blast;
    }

    private void Blast()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        Instantiate(bulletObject, new Vector3(barrelEnd.transform.position.x, barrelEnd.transform.position.y, barrelEnd.transform.position.z),
            Quaternion.Euler(gunMovement.barrelObject.transform.rotation.x, gunMovement.gunObject.transform.rotation.y, 0));
        yield return null;
    }
}

