using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Explotion))]
[RequireComponent(typeof(Renderer))]
[RequireComponent (typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Explotion _explotion;
    private List<Rigidbody> _littleCubesBodies;
    private int _maxProbabilityPercent = 100;

    public event UnityAction<Cube, bool> Pushed;

    public int DecayProbabilityPercent { get; private set; }

    public Cube()
    {
        DecayProbabilityPercent = 100;
    }

    private void Awake()
    {
        _littleCubesBodies = new List<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _explotion = GetComponent<Explotion>();
    }

    private void OnMouseUpAsButton()
    {
        bool isSplit = CalculateSplit(DecayProbabilityPercent);
        Pushed?.Invoke(this, isSplit);

        if (isSplit)
        {
            ExplodeWithLittleCubes();
        }
        else
        {
            ExplodeWithoutLittleCubes();
        }
    }

    public void Init(Material material, int olderCubeProbabilityPercent)
    {
        DecayProbabilityPercent = olderCubeProbabilityPercent / 2;
        _renderer.sharedMaterial = material;
        _explotion.DoubleForceAndRange();
    }

    public void AddLittleCubesBodies(List<Rigidbody> cubesBodies)
    {
        _littleCubesBodies = cubesBodies;
    }

    private void ExplodeWithLittleCubes()
    {
        _explotion.Implement(_littleCubesBodies);
    }

    private void ExplodeWithoutLittleCubes()
    {
        _explotion?.Implement();
    }

    private bool CalculateSplit(int currentProbability)
    {
        int probabilityCreate = Random.Range(0, _maxProbabilityPercent + 1);
        return probabilityCreate < currentProbability;
    }
}
