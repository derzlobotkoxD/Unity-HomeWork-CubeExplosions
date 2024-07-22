using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private float _force = 1000;
    [SerializeField] private float _explosionRadius = 12f;

    private void OnEnable()
    {
        _spawner.Created += Explode;
    }

    private void OnDisable()
    {
        _spawner.Created -= Explode;
    }

    private void Explode(List<Cube> cubes, Vector3 center)
    {
        foreach (Cube cube in cubes)
            cube.Rigidbody.AddExplosionForce(_force, center, _explosionRadius);
    }
}