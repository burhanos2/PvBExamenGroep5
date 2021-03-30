using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GunMovement GM;

    [SerializeField]
    private GameObject Bullet;

    [SerializeField] 
    private GameObject BulletSpawn;

    // Start is called before the first frame update
    void Start()
    {
        GM.Shoot += Blast;
    }

    public void Blast()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Shoot()
    {
        Instantiate(Bullet, new Vector3(BulletSpawn.transform.position.x, BulletSpawn.transform.position.y, BulletSpawn.transform.position.z),
            Quaternion.Euler(GM.Barrel.transform.rotation.x, GM.Gun.transform.rotation.y, 0));
        Debug.Log("nice");
        return null;
    }
}

