using System.Collections.Generic;
using UnityEngine;
using WaveSystem.Waves;

namespace WaveSystem
{
    [ExecuteInEditMode]
    public class WavesEditor : MonoBehaviour
    {
        [Range(1, 50)] 
        public int _waveAmount = 1;
        
        [SerializeField, Tooltip("Amount of Enemies that should spawn in specified wave.")] 
        private List<int> _enemiesPerWave = new List<int>();
        
        public int GetEnemiesForWave(int waveNumber) => _enemiesPerWave[(waveNumber - 1)]; // get enemy amount for specified wave
        public List<Transform> _enemySpawners = new List<Transform>();
        [Tooltip("Drag any waves you want to have specific enemies or timings here")]
        public CustomWave[] _customWaves;
        
        
        #if (UNITY_EDITOR)
            private void Update()
            {
                if (_waveAmount == _enemiesPerWave.Count) return; // if it is the same, dont bother going through the updating process
                UpdateList();
            }
        #endif
        
        private void UpdateList()
        {
            if (_waveAmount > _enemiesPerWave.Count)
            {
                FillWaves((_waveAmount - _enemiesPerWave.Count));
            }
            else if (_waveAmount < _enemiesPerWave.Count)
            {
                _enemiesPerWave.RemoveRange(_waveAmount, (_enemiesPerWave.Count - _waveAmount));
            }
        }
        
        private void FillWaves(int amount)
        {
            var v = amount;
            for (int i = 0; i < v; i++)
            {
                _enemiesPerWave.Add(1);
            }
        }
    }
}
