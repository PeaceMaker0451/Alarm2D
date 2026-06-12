using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDestroyer : MonoBehaviour
{
    [SerializeField] Item[] _walls;

    private void Start()
    {
        foreach (var wall in _walls)
            wall.Grabbed += OnWallGrabbed;
    }
    
    private void OnWallGrabbed()
    {
        foreach (var wall in _walls)
        {
            wall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            wall.Grabbed -= OnWallGrabbed;
        }
        
    }
}
