using System;
using UnityEngine;
using UnityEngine.Events;

public class InstantInteract : MonoBehaviour
{
    [Header("On Solve")]
    [SerializeField] private UnityEvent onSolved;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void React()
    {
        onSolved.Invoke();
    }
}
