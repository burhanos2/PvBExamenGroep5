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
        [SerializeField] private float _spawnOffset;
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
            var pick = Mathf.RoundToInt(Random.Range(0f, (_inflatablePropArray.Length - 1)));
            return _inflatablePropArray[pick];
        }
    
        private void TransFormOnDeath(GameObject prefabToBecome, GameObject whatToTransform)
        {
            var transform1 = whatToTransform.transform;
            var position = transform1.position;
            transform1.rotation = new Quaternion(transform.rotation.x, transform1.rotation.y + 180f, transform.rotation.z,transform1.rotation.w);
            Instantiate(prefabToBecome, new Vector3(position.x,position.y + _spawnOffset, position.z), transform1.rotation);
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
