using UnityEngine;
using WaveSystem;
using Random = UnityEngine.Random;

namespace TransformToProp
{
    public class EnemyTransformer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _inflatablePropArray;
        private WavesManager _wavesManager;
        [SerializeField] private GameObject _particleSystemPrefab;
        private void Start()
        {
            _wavesManager = GetComponent<WavesManager>();
            _wavesManager.OnEnemyDeath += DoOnDeath;
            
        }

        private void DoOnDeath(int points, GameObject toTransform)
        {
            TransFormOnDeath(RandomProp(), toTransform);
        }
        private GameObject RandomProp()
        {
            return _inflatablePropArray[Random.Range(0, (_inflatablePropArray.Length - 1))];
        }
    
        private void TransFormOnDeath(GameObject prefabToBecome, GameObject whatToTransform)
        {
            var transform1 = whatToTransform.transform;
            var position = transform1.position;
            Instantiate(prefabToBecome, position, transform1.rotation);
            DoTransformationEffect(position);
            Destroy(whatToTransform);
        }

        private void DoTransformationEffect(Vector3 whereToDo)
        {
            var transform1 = transform;
            Instantiate(_particleSystemPrefab, whereToDo, transform1.rotation, transform1);
        }
    }
}
