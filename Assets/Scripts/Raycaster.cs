using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Camera _camera;

    [Range(0, 100)][SerializeField] private float _maxDistence = 35f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Cast();
    }

    private void Cast()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistence, _mask))
            if (hit.collider.TryGetComponent<IClickable>(out IClickable clickable))
                clickable.OnClick();
    }
}
