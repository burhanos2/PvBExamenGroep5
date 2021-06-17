using UnityEngine;
using System;
using System.Collections.Generic;
using SoundSystem;
using WaveSystem.Waves;
using Random = UnityEngine.Random;

namespace WaveSystem
{
    public enum EnemyTypes
    {
        Regular = 0,
        SpeedBoat = 1,
    }
    
    public class WavesManager : MonoBehaviour
    {
        [SerializeField] private float _enemySpawnHeight; 
        [SerializeField] private GameOverManager _gameOverManager;
        private WavesEditor _wavesEditor;
        private int _currentWave = 1; //waves start at 1 and not 0!!
        public int CurrentWave
        {
            get => _currentWave;
            set => GoToWave(value);
        }
        public Action<int> OnNextWave; // int returns next wave number
        public Action<int, GameObject> OnEnemyDeath; // int returns enemy point value
        
        private int _enemiesDeployedThisWave;
        public int GetEnemiesDeployedThisWave => _enemiesDeployedThisWave;

        public List<GameObject> _currentLiveEnemies = new List<GameObject>();
        private GameObject[] _spawnAreaObjects; //not list because spawning areas do not vary, they are set in-editor\
        private int _spawnAreaCycleIndex;
        
        private int _currentEnemyLimit;

        private bool _allowSpawning;
        public GameObject[] _enemyObjectsToSpawn;

        [SerializeField] private float _defaultWaitTimeInSeconds = 1f;
        public static WavesManager Instance;

        private bool _gameoverCalled;

        private EnemyPlayAreaManager _enemyPlayAreaManager;
        private CustomWave[] _customWaves;
        private int _currentEnemyPlayAreaIndex;
        private Renderer _currentSpawnAreaRend;

        private EnemyTypes _currentEnemyType;
        private const EnemyTypes DefaultEnemyType = EnemyTypes.Regular;

        public Vector4 GetCurrentPlayArea {
            get
            {
                if (_customWaves.Length != 0 &&
                    _customWaves.Length >= _currentWave &&
                    _customWaves[_currentWave - 1]?._playArea != null
                    )
                {
                    return _enemyPlayAreaManager.GetBoundsIfPlayArea(_customWaves[_currentWave - 1]?._playArea);
                }
                return _enemyPlayAreaManager.GetBoundsOfArea(_currentEnemyPlayAreaIndex);
            }
        }
        //bool GameRunning = false;
        private void Awake()
        {
            Instance = this;
            _gameoverCalled = false;
        }

        private void Start()
        {
            _currentEnemyType = DefaultEnemyType; // make sure its regular on default just in case
            _wavesEditor = GetComponent<WavesEditor>();
            _spawnAreaObjects = GameObject.FindGameObjectsWithTag("SpawnArea");
            _spawnAreaCycleIndex = 0;
            _allowSpawning = true;
            _enemyPlayAreaManager = gameObject.transform.parent.GetComponentInChildren<EnemyPlayAreaManager>();
            _customWaves = _wavesEditor._customWaves; // copy over custom waves
            CheckCustomWave(_currentWave);
            
            OnNextWave += DoOnNextWave;
            OnEnemyDeath += DoOnEnemyDeath;

            GetEnemyLimit(_currentWave);
        }

        private void Update()
        {   
            //if(GameRunning)
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
                
                if (EnemyHasCustomValues())
                {
                    var enemyWaitInSeconds = _customWaves[_currentWave - 1]._enemyAndSpawnTimer[_enemiesDeployedThisWave].y;
                    _currentEnemyType = (EnemyTypes) _customWaves[_currentWave - 1]._enemyAndSpawnTimer[_enemiesDeployedThisWave].x;
                    Invoke("SpawnEnemy", enemyWaitInSeconds);
                }
                else
                {
                    _currentEnemyType = DefaultEnemyType;
                    Invoke("SpawnEnemy", _defaultWaitTimeInSeconds);
                }
            }
        }

        private bool EnemyHasCustomValues()
        {
            return _customWaves.Length >= _currentWave 
                   && _customWaves[_currentWave - 1]._enemyAndSpawnTimer.Length >= _enemiesDeployedThisWave + 1;
        }

        private void CheckCustomWave(int waveToCheck)
        {
            if (_customWaves.Length >= waveToCheck) // is there a custom wave set?
            {
                _currentEnemyPlayAreaIndex = _customWaves[waveToCheck - 1]._playAreaIndex;
                _currentSpawnAreaRend = _customWaves[waveToCheck - 1]._spawnArea != null ? _customWaves[waveToCheck - 1]._spawnArea.GetComponent<Renderer>() : _spawnAreaObjects[_spawnAreaCycleIndex].GetComponent<Renderer>();
                Radar.Instance._maxDistance = _customWaves[waveToCheck - 1]._radarMax;
            }
            else // if not, randomize
            {
                _currentEnemyPlayAreaIndex = Random.Range(0, _enemyPlayAreaManager.GetPlayAreaObjects.Length - 1);
                _currentSpawnAreaRend = _spawnAreaObjects[_spawnAreaCycleIndex].GetComponent<Renderer>();
            }
        }

        private void SpawnEnemy()
        {
            //spawn at a (maybe randomly) selected spawn area on a random position within it
            var spawnBounds = _currentSpawnAreaRend.bounds;
            
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

            var pos = new Vector3(rollXPos,_enemySpawnHeight,rollZPos); //random position within spawn area on enemySpawnHeight
            var rot = Quaternion.LookRotation((pos - Vector3.zero), Vector3.up); //default rotation
            
            _currentLiveEnemies.Add(Instantiate(_enemyObjectsToSpawn[(int)_currentEnemyType], pos, rot ));
            AudioManager.Instance.PlayRandomSfxVariant(SfxTypes.Splash);
            _enemiesDeployedThisWave++;
            
            //TODO tf is this UwU
            Radar.Instance.AddEnemy(_currentLiveEnemies[_currentLiveEnemies.Count-1].transform);
            
            //end routine
            _allowSpawning = true;
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
            if (_gameoverCalled) return;
            
            if (input > _wavesEditor._waveAmount)
            {
                Debug.Log("Entering a wave above amount, calling game over");
                CallGameOver();
                _gameoverCalled = true;
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
            if (_spawnAreaCycleIndex >= (_spawnAreaObjects.Length - 1)) //reset index var if over last index of array
            {
                _spawnAreaCycleIndex = 0;
            }
            else
            {
                _spawnAreaCycleIndex++;
            }
            CheckCustomWave(newWave);
        }
        
        public void CallGameOver()
        {
            // the game has ended because the waves are done, handle this
            _gameOverManager.OnGameOver?.Invoke(_gameOverManager._scoreKeeping._currentScore);
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


