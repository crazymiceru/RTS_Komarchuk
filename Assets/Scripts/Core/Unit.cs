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
    [SerializeField] protected CommandButtonView _commandButtonView;

    public bool IsSelect => _isSelect;
    private bool _isSelect;

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
        if (_commandButtonView == null) _commandButtonView = Reference.inst.CommandButtonView;
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (_infoPanel.gameObject.activeSelf != _isSelect)
        {
            _infoPanel.gameObject.SetActive(_isSelect);
            _infoPanel.UpdateElements();
            if (_defaultMaterial != null && _selectMaterial != null)
            {
                foreach (var item in _meshRenderers) item.material = _isSelect ? _selectMaterial : _defaultMaterial;
            }
        }
    }

    public void Select(ISelectable selectable)
    {
        var oldIsSelect = _isSelect;
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
        if (oldIsSelect != _isSelect) DoSelect();
    }

    public virtual void DoSelect()
    {

    }

    public void Deselect()
    {
        var oldIsSelect = _isSelect;
        _isSelect = false;
        UpdateInfo();
        if (oldIsSelect != _isSelect) DoSelect();
    }

    protected virtual void OnDestroy()
    {
        if (_infoPanel != null) Destroy(_infoPanel.gameObject);
    }
}
