using System;
using UnityEngine;

public class DiedEventHandler : MonoBehaviour
{
    public Action Died { get; set; } 
    public void DiedEvent()
    {
        Died?.Invoke();
    }
}