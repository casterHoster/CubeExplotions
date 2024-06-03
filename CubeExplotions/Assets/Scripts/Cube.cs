using UnityEngine;

[RequireComponent (typeof(Explotion))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private int _decayProbability;

    private void Awake()
    {
        _renderer = GetComponent<Renderer> ();
        _decayProbability = 100;
    }

    public void DecreaseDecayProbability()
    {
        _decayProbability /= 2;
    }

    public void SetMaterial(Material material)
    {
        _renderer.sharedMaterial = material;
    }

    public Explotion GetExplotion()
    {
        return GetComponent<Explotion>(); ;
    }

    public int GetDecayProbability()
    {
        return _decayProbability;
    }
}
