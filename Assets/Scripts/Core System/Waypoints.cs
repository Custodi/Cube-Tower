using UnityEditor;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    public Transform[] Points => _points;
}