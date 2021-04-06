using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDots;
    private List<Transform> _boats = new List<Transform>();
    private List<Transform> _dots = new List<Transform>();

    [SerializeField] private const float MaxDistance = 100, Scale = 2;

    void Update()
    {
        for (int i = 0; i < _boats.Count; i++)
        {
            Vector3 pos = _boats[i].position.normalized;
            _dots[i].position = ((pos / MaxDistance) * Vector3.Distance(transform.position, _boats[i].position)) / Scale;
        }
    }

    public void AddEnemy(Transform boat)
    {
        _boats.Add(boat);
        GameObject dot = Instantiate(_enemyDots, transform);
        Vector3 pos = boat.position.normalized;
        dot.transform.position = ((pos / MaxDistance) * Vector3.Distance(transform.position, boat.position)) / Scale;
        _dots.Add(dot.transform);
    }

    public void DeleteEnemy(Transform boat)
    {
        for (int i = 0; i < _boats.Count; i++)
        {
            if (boat != _boats[i]) continue;
            
            _boats.RemoveAt(i);
            Destroy(_dots[i].gameObject);
            _dots.RemoveAt(i);
            return;
        }
    }
}
