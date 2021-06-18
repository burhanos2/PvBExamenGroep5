using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDots;
    private List<Transform> _boats = new List<Transform>();
    private List<Transform> _dots = new List<Transform>();

    public float _maxDistance = 150f;
    private const float Scale = 2.1f;
    

    public static Radar Instance;

    void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        for (int i = 0; i < _boats.Count; i++)
        {
            Vector3 pos = _boats[i].position.normalized;
            float dis = Vector3.Distance(transform.position, _boats[i].position);
            dis = dis > _maxDistance ? _maxDistance : dis;
            _dots[i].localPosition = ((pos / _maxDistance) * dis) / Scale;
        }
    }

    public void AddEnemy(Transform boat)
    {
        _boats.Add(boat);
        GameObject dot = Instantiate(_enemyDots, transform);
        Vector3 pos = boat.position.normalized;
        dot.transform.localPosition = ((pos / _maxDistance) * Vector3.Distance(transform.position, boat.position)) / Scale;
        _dots.Add(dot.transform);
    }

    public void DeleteEnemy(Transform boat)
    {
        //print(_boats.Count);
        for (int i = 0; i < _boats.Count; i++)
        {
            if (boat == _boats[i])
            {
                _boats.RemoveAt(i);
                Destroy(_dots[i].gameObject);
                _dots.RemoveAt(i);
                
                //print(_boats.Count);
                return;
            }
        }
    }
}
