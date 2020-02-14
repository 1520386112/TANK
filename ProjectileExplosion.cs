using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    void Update()
    {
        
    }
}
