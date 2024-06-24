using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Explotion))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Explotion _explotion;
    private int _maxProbabilityPercent = 100;
    private int _dividor = 2;

    public event UnityAction<Cube> CubeSplit;

    [field: SerializeField] public int DecayProbabilityPercent { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _explotion = GetComponent<Explotion>();
    }

    private void OnMouseUpAsButton()
    {
        bool isSplit = CalculateSplit(DecayProbabilityPercent);
        Exclude(isSplit);
    }

    public void Init(Material material, int olderCubeProbabilityPercent)
    {
        DecayProbabilityPercent = olderCubeProbabilityPercent / _dividor;
        _renderer.sharedMaterial = material;
        _explotion.DoubleForceAndRange();
    }

    public void Explode(List<Rigidbody> cubesBodies)
    {
        _explotion.Implement(cubesBodies);
    }

    private bool CalculateSplit(int currentProbability)
    {
        int probabilityCreate = Random.Range(0, _maxProbabilityPercent + 1);
        return probabilityCreate < currentProbability;
    }

    private void Exclude(bool isSplit)
    {
        if (isSplit)
        {
            CubeSplit?.Invoke(this);
        }
        else
        {
            _explotion.Implement();
        }
    }
}
