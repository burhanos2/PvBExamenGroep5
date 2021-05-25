using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//throw this script on camera

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float _randomMin = -1;
    [SerializeField]
    private float _randomMax = 1;
    
    public IEnumerator CamShake (float duration, float magnitude)
    {
        var originalPos = transform.position;

        var elapsed = 0.0f;

        while (elapsed < duration)
        {
            var random = magnitude * Random.Range(_randomMin, _randomMax) * 0.1f;
            
            transform.localPosition += transform.right * random;
            transform.localPosition += transform.up * random;
            
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
    }

}
