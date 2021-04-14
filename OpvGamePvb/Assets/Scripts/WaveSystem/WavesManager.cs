using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace WaveSystem
{
    public class WavesManager : MonoBehaviour
    {
        private WavesEditor _wavesEditor;
        private int _currentWave = 1;
        public int CurrentWave
        {
            get => _currentWave;
            set => GoToWave(value);
        }
        public Action<int> OnNextWave; // int returns next wave number
        public Action<int, GameObject> OnEnemyDeath; // int returns enemy point value
        
        private int _enemiesDeployedThisWave;
        public int GetEnemiesDeployedThisWave => _enemiesDeployedThisWave;

        private List<GameObject> _currentLiveEnemies = new List<GameObject>();
        private GameObject[] _spawnAreaObjects; //not list because spawning areas do not vary, they are set in-editor\
        private int _currentSpawnAreaIndex;
        
        private int _currentEnemyLimit;

        private bool _allowSpawning;
        public GameObject _enemyObjectToSpawn;

        public static WavesManager Instance;

        void Awake()
        {
            Instance = this;
        }

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
                    GoToWave((_currentWave + 1));
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
            
            var tenPercentOfXBound = (spawnBounds.size.x * 0.1);
            var tenPercentOfZBound = (spawnBounds.size.z * 0.1);

            var coinFlip = (5 > Random.Range(0, 10));
            float rollXPos;
            float rollZPos;
            
            if (coinFlip)
            {
                rollXPos = Mathf.Clamp(
                    Random.Range(spawnBounds.min.x, spawnBounds.max.x),
                    (spawnBounds.max.x - (float)tenPercentOfXBound),
                    (spawnBounds.min.x + (float)tenPercentOfXBound));

                rollZPos = Random.Range(spawnBounds.min.z,spawnBounds.max.z);
            }
            else
            {
                rollZPos = Mathf.Clamp(
                    Random.Range(spawnBounds.min.z, spawnBounds.max.z),
                    (spawnBounds.max.z - (float)tenPercentOfZBound),
                    (spawnBounds.min.z + (float)tenPercentOfZBound));
                
                rollXPos = Random.Range(spawnBounds.min.x,spawnBounds.max.x);
            }

            var pos = new Vector3(rollXPos,0,rollZPos); //random position within spawn area on y = 0
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
        
        private void GoToWave(int input)
        {
            if (input <= 0)
            {
                Debug.LogError("Current wave does not exist.");
            }
            else if (input > _wavesEditor._waveAmount)
            {
                Debug.Log("Entering a wave above amount, calling game over");
                CallGameOver();
            }
            else
            {
                if (_currentLiveEnemies.Count != 0)
                {
                    ClearEnemies();
                }
                OnNextWave?.Invoke(input);
            }
        }

        private void DoOnNextWave(int newWave)
        {
            _enemiesDeployedThisWave = 0;
            GetEnemyLimit(newWave);
            _currentWave = newWave;
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
            //Destroy(enemy); //maybe not remove here? line may need to be removed later
        }

        private void ClearEnemies()
        {
            foreach (var enemy in _currentLiveEnemies)
            {
                Destroy(enemy);
            }
            _currentLiveEnemies.Clear();
        }
    }
}


