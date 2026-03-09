using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCast : MonoBehaviour
{
    public Character owner;

    private void Awake()
    {
        owner = GetComponent<Character>();
    }
}
