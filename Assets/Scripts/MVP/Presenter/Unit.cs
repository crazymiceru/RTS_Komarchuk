using UnityEngine;

public class Unit : MonoBehaviour, IUnit, ISelectable
{
    public float HP => _hp;
    [SerializeField] protected float _hp;
    public float MaxHP => _maxHP;
    [SerializeField] protected float _maxHP;
    public bool IsGroupSelectable => _GroupSelectable;
    [SerializeField] private bool _GroupSelectable;

    [Header("Info Panel")]
    [SerializeField] protected GameObject _prefabInfoPanel;
    [SerializeField] protected Transform _positionPanel;
    [Header("Select")]
    [SerializeField] private SelectModel _selectModel;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _selectMaterial;

    public bool IsSelect => _isSelect;
    [SerializeField] private bool _isSelect;

    protected InfoPanelBasic _infoPanel;

    private Renderer[] _meshRenderers;

    protected virtual void Awake()
    {
        var _panel = Instantiate(_prefabInfoPanel, Reference.inst.FolderInfo);
        if (_panel.TryGetComponent(out InfoPanelBasic infoPanel))
        {
            _infoPanel = infoPanel;
            infoPanel.Init(_positionPanel, this);
        }
        
        _selectModel.evtSelect += Select;
        _selectModel.evtDeSelectAll += Deselect;

        _meshRenderers = GetComponentsInChildren<Renderer>();
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (_infoPanel.gameObject.activeSelf != _isSelect)
        {
            _infoPanel.gameObject.SetActive(_isSelect);
            _infoPanel.UpdateElements();
            if (_defaultMaterial!=null && _selectMaterial!=null)
            {
                foreach (var item in _meshRenderers) item.material = _isSelect ? _selectMaterial : _defaultMaterial;
            }
        }
    }

    public void Select(ISelectable selectable)
    {
        if (selectable == (ISelectable)this)
        {
            if (!IsGroupSelectable && !_isSelect) _selectModel.DeselectAll();
            _isSelect = !_isSelect;
            UpdateInfo();
        }
        else if (!IsGroupSelectable)
        {            
            _isSelect = false;
            UpdateInfo();
        }
    }

    public void Deselect()
    {
        _isSelect = false;
        UpdateInfo();
    }

    protected virtual void OnDestroy()
    {
        if (_infoPanel != null) Destroy(_infoPanel.gameObject);
    }
}
