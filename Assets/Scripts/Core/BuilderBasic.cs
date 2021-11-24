using UnityEngine;

public class BuilderBasic : Unit, IBuildCommand
{
    [SerializeField] private GameObject _unitBuild;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void DoSelect()
    {
        if (IsSelect) _commandButtonView.AddCommand(this);
        else _commandButtonView.RemoveCommand(this);
    }

    protected override void OnDestroy()
    {
        _commandButtonView.RemoveCommand(this);
        base.OnDestroy();
    }

    public void Build()
    {
        var addRandom = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        Instantiate(_unitBuild, transform.position + addRandom, Quaternion.Euler(0, Random.Range(0, 360), 0), Reference.inst.FolderActiveElements);
    }
}
