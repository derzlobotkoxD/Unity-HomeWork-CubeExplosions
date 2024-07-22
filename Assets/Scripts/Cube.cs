using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour, IClickable
{
    [SerializeField] private float _explosionForce = 1000f;
    [SerializeField] private float _explosionRadius = 12f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _explosionMask;

    private float _maximumChange = 100f;
    private float _minimumChange = 0f;

    public event UnityAction<Cube> Splitted;

    public float ChangeToSplit { get; private set; } = 100f;

    public Rigidbody Rigidbody => _rigidbody;

    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void Initialization(Vector3 scale, float change)
    {
        transform.localScale = scale;
        ChangeToSplit = change;
    }

    public void OnClick() =>
        TrySplite();

    private void TrySplite()
    {
        float change = Random.Range(_minimumChange, _maximumChange);

        if (change <= ChangeToSplit)
            Splitted?.Invoke(this);
        else
            Explode();

        Destroy(gameObject);
    }

    private void Explode()
    {
        float radius = _explosionRadius / transform.localScale.magnitude;
        float force = _explosionForce / transform.localScale.magnitude;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, _explosionMask);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            rigidbody.AddExplosionForce(force, transform.position, radius);
        }
    }
}