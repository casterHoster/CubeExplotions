using UnityEngine;

[RequireComponent (typeof(Explotion))]
[RequireComponent (typeof (Renderer))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Explotion _explotion;

    public int DecayProbability { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer> ();
        _explotion = GetComponent<Explotion> ();
    }

    public Cube()
    {
        DecayProbability = 100;
    } 

    public void DecreaseDecayProbability()
    {
        DecayProbability /= 2;
    }

    public void SetMaterial(Material material)
    {
        _renderer.sharedMaterial = material;
    }

    public Explotion GetExplotion()
    {
        return _explotion;
    }
}
