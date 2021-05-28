using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//throw this script on camera

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float RandomMin = -1;
    [SerializeField]
    private float RandomMax = 1;
    
    public IEnumerator CamShake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float shakex = Random.Range(RandomMin, RandomMax) * magnitude;
            float shakey = Random.Range(RandomMin, RandomMax) * magnitude;

            transform.localPosition = new Vector3(shakex, shakey, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
    }

}
