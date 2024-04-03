using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] Transform _wayPointsParent;
    [SerializeField, Min(1)] private float _speed = 1;

    private Transform[] _waypoints;
    private float _offset = 0.001f;
    private int _currentWaypoint = 0;

    private void Start()
    {
        _waypoints = new Transform[_wayPointsParent.childCount];

        for(int i = 0; i < _waypoints.Length; i++)
            _waypoints[i] = _wayPointsParent.GetChild(i);

        if (_waypoints.Length > 0)
            transform.right = _waypoints[0].position - transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _waypoints[_currentWaypoint].position) <= _offset)
        {
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;
            transform.right = _waypoints[_currentWaypoint].position - transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }
}
