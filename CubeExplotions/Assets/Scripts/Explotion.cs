using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]

public class Explotion : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _force;

    public void Implement(List<Cube> littleCubes)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(littleCubes))
        {
            if (GetDistance(explodableObject) != 0)
            {
                _force = _force / GetDistance(explodableObject) * 10;
                explodableObject.AddExplosionForce(_force, transform.position, _range);
            }
        }

        Destroy(gameObject);
    }

    public void Implement()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            if (GetDistance(explodableObject) != 0)
            {
                _force = _force / GetDistance(explodableObject) * 10;
                explodableObject.AddExplosionForce(_force, transform.position, _range);
            }
        }

        Destroy(gameObject);
    }

    public void DoubleForceAndRange()
    {
        _range *= 2;
        _force *= 2;
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        List<Rigidbody> reachesObjects = new List<Rigidbody>();
        Collider[] hits = Physics.OverlapSphere(transform.position, _range);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                reachesObjects.Add(hit.attachedRigidbody);
            }
        }

        return reachesObjects;
    }

    private List<Rigidbody> GetExplodableObjects(List<Cube> cubes)
    {
        List<Rigidbody> reachesObjects = new List<Rigidbody>();

        foreach (Cube cube in cubes)
        {
            if (cube.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                reachesObjects.Add(rigidbody);
            }
        }

        return reachesObjects;
    }

    private float GetDistance(Rigidbody explodableObject)
    {
        return (transform.position - explodableObject.position).magnitude;
    }
}
