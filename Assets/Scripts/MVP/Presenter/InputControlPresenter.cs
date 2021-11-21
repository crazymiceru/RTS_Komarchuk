using System.Linq;
using UnityEngine;

public class InputControlPresenter : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private SelectModel _selectModel;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _selectModel.CountUI==0)
        {
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            var select = hits.Select(hit => hit.collider.GetComponentInParent<ISelectable>()).FirstOrDefault(c => c != null);
            if (select != null)
            {
                _selectModel.Select(select);
            }
        }
    }
}
