using UnityEngine;

[RequireComponent (typeof(Explotion))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer> ();
    }

    public void SetMaterial(Material material)
    {
        _renderer.sharedMaterial = material;
    }

    public Explotion GetExplotion()
    {
        return GetComponent<Explotion>();
    }
}
