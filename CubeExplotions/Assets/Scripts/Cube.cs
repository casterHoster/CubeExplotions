using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Explotion))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Explotion _explotion;
    private List<Cube> _littleCubes;

    public int DecayProbability { get; private set; }

    public event UnityAction<Cube> Pushed;

    private void Awake()
    {
        _littleCubes = new List<Cube>();
        _renderer = GetComponent<Renderer>();
        _explotion = GetComponent<Explotion>();
    }

    public Cube()
    {
        DecayProbability = 100;
    }

    private void OnMouseUpAsButton()
    {
        Pushed?.Invoke(this);

        if (_littleCubes.Count > 0)
        {
            ExplodeWithLittleCubes();
        }
        else
        {
            ExplodeWithoutLittleCubes();
        }
    }

    private void ExplodeWithLittleCubes()
    {
        _explotion.Implement(_littleCubes);
    }

    private void ExplodeWithoutLittleCubes()
    {
        _explotion?.Implement();
    }

    public void Init(Material material)
    {
        DecayProbability /= 2;
        _renderer.sharedMaterial = material;
        _explotion.DoubleForceAndRange();
    }

    public void AddLittleCubes(List<Cube> cubes)
    {
        _littleCubes = cubes;
    }
}
