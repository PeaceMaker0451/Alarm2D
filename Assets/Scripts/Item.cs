using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public event Action Grabbed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void SetGrabbed()
    {
        _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        Grabbed?.Invoke();
    }

    public void SetReleased()
    {
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
    }
}
