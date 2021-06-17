using UnityEngine;
using UnityEngine.UI;
using WaveSystem;

public class DisplayLiveEnemyAmount : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private WavesManager _wavesManager;
    
    private void Update()
    {
        _text.text = "Radar: " + _wavesManager._currentLiveEnemies.Count;
    }
}
