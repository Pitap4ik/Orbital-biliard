using UnityEngine;
using System;
using UnityEngine.UI;
using JetBrains.Annotations;


public class Streching : MonoBehaviour
{
    [SerializeField] private float _constant1;
    [SerializeField] private float k;
    

    private float deltax = 0;
    void Start()
    {
        
    }

    void Update()
    {
        float distance = GetDistance(transform.position);
        if (distance<6)
        {
            float F = 0;
        }
        Debug.Log(GetComponent<SpriteRenderer>().bounds.size.x);
    }
    public float GetDistance(Vector2 objectPosition)
    {
        return MathF.Sqrt(MathF.Pow(objectPosition.x, 2) + MathF.Pow(objectPosition.y, 2));
    }
}
