using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]

public class Explotion : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _force;

    private int _factorForceAndRangeIncrease = 2;
    private int _factorImplementCalculate = 10;
    private float _distance;


    public void Implement(List<Rigidbody> littleCubesBodies)
    {
        if (littleCubesBodies.Count == 0)
        {
            littleCubesBodies = GetExplodableObjects();
        }

        foreach (Rigidbody explodableObject in littleCubesBodies)
        {
            _distance = GetDistance(explodableObject);
            
            if (_distance != 0)
            {
                _force = _force / _distance * _factorImplementCalculate;
                explodableObject.AddExplosionForce(_force, transform.position, _range);
            }
        }

        Destroy(gameObject);
    }

    public void DoubleForceAndRange()
    {
        _range *= _factorForceAndRangeIncrease;
        _force *= _factorForceAndRangeIncrease;
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

    private float GetDistance(Rigidbody explodableObject)
    {
        return (transform.position - explodableObject.position).magnitude;
    }
}
