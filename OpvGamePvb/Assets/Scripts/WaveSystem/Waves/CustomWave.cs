using UnityEngine;

namespace WaveSystem.Waves
{
    public abstract class CustomWave : MonoBehaviour
    {
        public GameObject _playArea; //set in editor, if null then index setting will be used
        [Tooltip("counting from 0, which play area in the index to pick")]
        public int _playAreaToSpawnIndex;
        [Tooltip("first slot is enemy type, second is seconds to wait before spawning")]
        public Vector2[] _enemyAndSpawnTimer;
    }
}
