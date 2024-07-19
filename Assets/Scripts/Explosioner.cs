using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private float _force = 1000;

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
        {
            Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

            Vector3 direction = cube.transform.position - center;

            rigidbody.AddForce(direction * _force, ForceMode.Force);
        }
    }
}