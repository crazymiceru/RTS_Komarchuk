using UnityEngine;

public class BuilderBasic : Unit, IBuilding
{
    [SerializeField] private GameObject _unitBuild;
    private InfoPanelButton _infoPanelButton;

    protected override void Awake()
    {
        base.Awake();
        if (_infoPanel is InfoPanelButton infoPanelButton)
        {
            _infoPanelButton = infoPanelButton;
            _infoPanelButton.Button.onClick.AddListener(Build);
        }
    }

    public void Build()
    {
        var addRandom = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        Instantiate(_unitBuild, transform.position + addRandom, Quaternion.Euler(0, Random.Range(0, 360), 0), Reference.inst.FolderActiveElements);
    }

    protected override void OnDestroy()
    {
        if (_infoPanelButton!=null) _infoPanelButton.Button.onClick.RemoveListener(Build);
        base.OnDestroy();
    }
}
