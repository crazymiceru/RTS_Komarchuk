using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputControlPresenter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distGroundTarget = 0.1f;
    private Camera _camera;
    [SerializeField] private SelectModel _selectModel;
    private EventSystem _eventSystem;

    private void Awake()
    {
        _camera = Camera.main;
        _eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || _eventSystem.IsPointerOverGameObject()) return;

        if ( Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out RaycastHit hit))
        {
            var select = hit.collider.GetComponentInParent<ISelectable>();
            if (select!=null) _selectModel.Select(select);
            else
            {
                var groundSelect = hit.collider.GetComponentInParent<TagGround>();
                if (groundSelect != null) _target.position = hit.point + Vector3.up* _distGroundTarget;
            }
        }            
    }
}
