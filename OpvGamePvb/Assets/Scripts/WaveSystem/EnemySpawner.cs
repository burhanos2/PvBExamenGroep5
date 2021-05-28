using UnityEngine;

namespace WaveSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        private WavesManager _wavesManager;
        
        private void Start()
        {
            _wavesManager = GetComponent<WavesManager>();
            _wavesManager.OnNextWave += EnterWave;
            
            
            //_wavesManager.GoToNextWave();
        }

        private void EnterWave(int wave)
        {
            
        }
    }
}
