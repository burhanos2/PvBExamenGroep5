using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace WaveSystem
{
    public class WavesManager : MonoBehaviour
    {
        private WavesEditor _wavesEditor;
        public int _currentWave = 1;
        public Action<int> OnNextWave; // int returns next wave number
        public Action<int> OnEnemyDeath; // int returns enemy point value

        public int _enemiesDeployedThisWave;

        private List<GameObject> _currentLiveEnemies = new List<GameObject>();
        public GameObject[] _spawnAreaObjects; //not list because spawning areas do not vary, they are set in-editor\
        private int _currentSpawnAreaIndex = 0;

        private IEnumerator _coroutine;
        private float _randomNo;
        private float _randomMin, _randomMax;
        private int _currentEnemyLimit;

        public GameObject _enemyObjectToSpawn;

        private void Start()
        {
            _wavesEditor = GetComponent<WavesEditor>();
            _spawnAreaObjects = GameObject.FindGameObjectsWithTag("SpawnArea");
           
            _randomMin = 1f; //set minimum and maximum wait between spawns, this is only because it is random at the moment
            _randomMax = 4f;
            _coroutine = SpawnEnemy(_randomNo);

            OnNextWave += DoOnNextWave;
        }

        private void Update()
        {
            if (_enemiesDeployedThisWave >= _currentEnemyLimit) // have all enemies been deployed?
            {
                if (_currentLiveEnemies.Count == 0) // are there no enemies left?
                {
                    GoToNextWave();
                }
            }
            else
            {
                _randomNo = Random.Range(_randomMin, _randomMax);
                StartCoroutine(_coroutine);   //spawn enemy
            }
        }

        private void GoToNextWave()
        {
            if (_currentWave <= 0 || _currentWave > _wavesEditor._waveAmount)
            {
                Debug.LogError("Current wave does not exist.");
            }
            else if (_currentWave == _wavesEditor._waveAmount)
            {
                CallGameOver();
            }
            else
            {
                _currentWave++;
                OnNextWave?.Invoke(_currentWave);
            }
        }

        private void DoOnNextWave(int newWave)
        {
            _enemiesDeployedThisWave = 0;
            _currentEnemyLimit = _wavesEditor.GetEnemiesForWave(newWave);
        }

        private void CallGameOver()
        {
            // the game has ended because the waves are done, handle this
        }
        
        private IEnumerator SpawnEnemy(float waitTime) // COROUTINE HERE
        {
            //spawn at a (maybe randomly) selected spawn area on a random position within it
            var spawnBounds = _spawnAreaObjects[_currentSpawnAreaIndex].GetComponent<Renderer>().bounds;

            var pos = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x),0,Random.Range(spawnBounds.min.z, spawnBounds.max.z)); //random position within spawn area on y = 0
            var rot = Quaternion.LookRotation(Vector3.zero, Vector3.up); //default rotation
            
            Instantiate(_enemyObjectToSpawn, pos, rot );
            
            //end routine
            if (_currentSpawnAreaIndex >= (_spawnAreaObjects.Length - 1)) //reset index var if over last index of array
            {
                _currentSpawnAreaIndex = 0;
            }
            else
            {
                _currentSpawnAreaIndex++;
            }
            yield return new WaitForSeconds(waitTime);
        }

        private void HandleEnemyDeath()
        {
            OnEnemyDeath(1); //for now the value is 1
        }

        private void DoOnEnemyDeath(int pointValue)
        {
            
        }
    }
}
