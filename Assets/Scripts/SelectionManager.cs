using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string creativeTag = "CreativeObject";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    public Camera cam;
    private Transform _selection;

    private void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag) || (selection.CompareTag(creativeTag) && selection.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }
        }
    }
}
