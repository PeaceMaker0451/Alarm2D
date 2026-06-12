using UnityEngine;

public class HouseDestroyer : MonoBehaviour
{
    [SerializeField] private Item[] _walls;

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
