using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDots;
    private List<Transform> _boats = new List<Transform>();
    private List<Transform> _dots = new List<Transform>();

    private float _maxDistance = 100;

    public Transform tester;
    void Start()
    {
        AddEnemy(tester);
    }
    
    void Update()
    {
        for (int i = 0; i < _boats.Count; i++)
        {
            //TODO dont normalize
            Vector3 pos = _boats[i].position.normalized;
            _dots[i].position = (pos / _maxDistance) * Vector3.Distance(transform.position, _boats[i].position);
        }
    }

    void AddEnemy(Transform boat)
    {
        _boats.Add(boat);
        GameObject dot = Instantiate(_enemyDots, transform);
        //TODO dont normalize
        Vector3 pos = boat.position.normalized;
        dot.transform.position = (pos / _maxDistance) * Vector3.Distance(transform.position, boat.position);
        _dots.Add(dot.transform);
    }

    void DeleteEnemy()
    {
        
    }
}
