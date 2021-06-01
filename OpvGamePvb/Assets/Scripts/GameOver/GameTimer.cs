using UnityEngine;
using WaveSystem;

public class GameTimer : MonoBehaviour
{
    private bool _started = false;
    private float _time;

    [SerializeField] private float _maxTime = 60 * 10;

    public void StartTimer() => _started = true;
    
    void Update()
    {
        if(!_started) return;

        _time += Time.deltaTime;

        if (_time > _maxTime)
            HandleTimeLimit();
    }

    private void HandleTimeLimit()
    {
        WavesManager.Instance.CallGameOver();
    }

}
