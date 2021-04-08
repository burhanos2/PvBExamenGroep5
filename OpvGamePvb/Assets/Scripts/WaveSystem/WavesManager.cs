using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace WaveSystem
{
    public class WavesManager : MonoBehaviour
    {
        private WavesEditor _wavesEditor;
        public int _currentWave = 1;
        public Action<int> OnNextWave; // int returns next wave number
        public Action<int, GameObject> OnEnemyDeath; // int returns enemy point value
        
        public int _enemiesDeployedThisWave;

        private List<GameObject> _currentLiveEnemies = new List<GameObject>();
        public GameObject[] _spawnAreaObjects; //not list because spawning areas do not vary, they are set in-editor\
        private int _currentSpawnAreaIndex;
        
        private int _currentEnemyLimit;

        private bool _allowSpawning;
        public GameObject _enemyObjectToSpawn;

        private void Start()
        {
            _wavesEditor = GetComponent<WavesEditor>();
            _spawnAreaObjects = GameObject.FindGameObjectsWithTag("SpawnArea");
            _currentSpawnAreaIndex = 0;
            _allowSpawning = true;

            OnNextWave += DoOnNextWave;
            OnEnemyDeath += DoOnEnemyDeath;

            GetEnemyLimit(_currentWave);
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
            else if(_allowSpawning)
            {
                _allowSpawning = false;
                SpawnEnemy();
                //add a wait here?
                _allowSpawning = true;
            }
        }

        private void SpawnEnemy()
        {
            //spawn at a (maybe randomly) selected spawn area on a random position within it
            var spawnBounds = _spawnAreaObjects[_currentSpawnAreaIndex].GetComponent<Renderer>().bounds;

            var pos = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x),0,Random.Range(spawnBounds.min.z, spawnBounds.max.z)); //random position within spawn area on y = 0
            var rot = Quaternion.LookRotation((pos - Vector3.zero), Vector3.up); //default rotation
            
            _currentLiveEnemies.Add(Instantiate(_enemyObjectToSpawn, pos, rot ));
            _enemiesDeployedThisWave++;
            
            //TODO tf is this UwU
            Radar.Instance.AddEnemy(_currentLiveEnemies[_currentLiveEnemies.Count-1].transform);
            
            //end routine
            if (_currentSpawnAreaIndex >= (_spawnAreaObjects.Length - 1)) //reset index var if over last index of array
            {
                _currentSpawnAreaIndex = 0;
            }
            else
            {
                _currentSpawnAreaIndex++;
            }
        }
        private void GetEnemyLimit(int wave)
        {
            _currentEnemyLimit = _wavesEditor.GetEnemiesForWave(wave);
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
            GetEnemyLimit(newWave);
        }
        
        private void CallGameOver()
        {
            // the game has ended because the waves are done, handle this
        }

        private void DoOnEnemyDeath(int pointValue, GameObject enemy)
        {
            _currentLiveEnemies.Remove(enemy);
            //TODO WTF UwU
            Radar.Instance.DeleteEnemy(enemy.transform);
            Destroy(enemy); //maybe not remove here? line may need to be removed later
        }
    }
}


