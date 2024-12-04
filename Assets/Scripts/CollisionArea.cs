using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionArea : MonoBehaviour
{
    public List<GameObject> CurrentCollisions { get; private set; }

    private void Start()
    {
        CurrentCollisions = new List<GameObject>();
        //StartCoroutine(PrintCollisionsCount(1.0f));
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator PrintCollisionsCount(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print($"Collisions: {GetCollisionsCount()}");
        }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D col)
    {
        CurrentCollisions.Add(col.gameObject);

        foreach (GameObject gObject in CurrentCollisions)
        {
            //print(gObject.name);
        }

        print($"Collisions: {GetCollisionsCount()}");
    }

    void OnTriggerExit2D(UnityEngine.Collider2D col)
    {
        CurrentCollisions.Remove(col.gameObject);

        if (CurrentCollisions.Count == 0)
        {
            //Debug.Log("No collisions.");
        }
        else
        {
            foreach (GameObject gObject in CurrentCollisions)
            {
                //print(gObject.name);
            }
        }

        print($"Collisions: {GetCollisionsCount()}");
    }

    public int GetCollisionsCount()
    {
        return CurrentCollisions.Count;
    }
}
