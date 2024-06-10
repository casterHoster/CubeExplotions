using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Explotion))]
[RequireComponent (typeof (Renderer))]

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
        _renderer = GetComponent<Renderer> ();
        _explotion = GetComponent<Explotion> ();
    }

    public Cube()
    {
        DecayProbability = 100;
    }

    private void OnMouseUpAsButton()
    {
        Pushed?.Invoke(this);
        Explode();
    }

    public void Init(Material material)
    {
        DecayProbability /= 2;
        _renderer.sharedMaterial = material;
    }

    public void Explode()
    {
        _explotion.Implement(_littleCubes);
    }

    public void AddLittleCubes(List<Cube> cubes)
    {
        _littleCubes = cubes;
    }
}
