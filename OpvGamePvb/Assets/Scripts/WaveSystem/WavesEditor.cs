using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaveSystem
{
    [ExecuteInEditMode]
    public class WavesEditor : MonoBehaviour
    {
        [SerializeField, Range(1, 20)] 
        private int _waveAmount = 1;
        [SerializeField, Tooltip("Amount of Enemies that should spawn in specified wave.")] 
        private List<int> _enemiesPerWave = new List<int>();
        
        public int GetEnemyAmountThisWave => _enemiesPerWave[_currentWave - 1]; // getter for current wave
        public List<Transform> _enemySpawners = new List<Transform>();
        public int _currentWave = 1;
        public Action<int> OnNextWave; // int returns next wave number
        
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
