using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoPanelBasic : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _elementHP;
    [SerializeField] private SelectModel _selectModel;

    private Transform _target;
    
    private IUnit _unit;

    public void Init(Transform target,IUnit unit)
    {
        _target = target;
        _unit = unit; 
        UpdateElements();
    }

    private void Update()
    {
        if (_unit == null) return;

        UpdateElements();
    }

    public void UpdateElements()
    {
        transform.position = Reference.inst.MainCamera.WorldToScreenPoint(_target.position);
        _elementHP.fillAmount = _unit.MaxHP / _unit.HP;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {  
        _selectModel.MouseOnUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selectModel.MouseOffUI();
    }
}
