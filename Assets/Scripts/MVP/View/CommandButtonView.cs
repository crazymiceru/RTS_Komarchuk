using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtonView : MonoBehaviour
{
    public Action OnClickAttack;
    public Action OnClickPatrol;
    public Action<Vector3> OnClickMove;
    public Action OnClickBuild;
    public Action OnClickStop;

    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _patrolButton;
    [SerializeField] private Button _moveButton;
    [SerializeField] private Button _buildButton;
    [SerializeField] private Button _stopButton;

    [SerializeField] private Transform target;

    private Dictionary<Type, ForButton> _buttonsByExecutorType;

    private class ForButton
    {
        public readonly Button Button;
        public readonly Action Action;
        public int CountAction;
        public ForButton(Button button, Action action)
        {
            Button = button;
            Action = action;
            CountAction = 0;
        }
    }

    private void Awake()
    {
        _buttonsByExecutorType = new Dictionary<Type, ForButton>();
        _buttonsByExecutorType.Add(typeof(IAttackCommand), new ForButton(_attackButton, () => OnClickAttack?.Invoke()));
        _buttonsByExecutorType.Add(typeof(IPatrolCommand), new ForButton(_patrolButton, () => OnClickPatrol?.Invoke()));
        _buttonsByExecutorType.Add(typeof(IMoveCommand), new ForButton(_moveButton, () => Move()));
        _buttonsByExecutorType.Add(typeof(IBuildCommand), new ForButton(_buildButton, () => OnClickBuild?.Invoke()));
        _buttonsByExecutorType.Add(typeof(IStopCommand), new ForButton(_stopButton, () => OnClickStop.Invoke()));

        foreach (var item in _buttonsByExecutorType)
        {
            item.Value.Button.onClick.AddListener(() => item.Value.Action?.Invoke());
            item.Value.Button.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        ClearButtons();
    }

    void Move()
    {
       if (target!=null) OnClickMove(target.position);
    }

    public void ClearButtons()
    {
        foreach (var item in _buttonsByExecutorType)
            item.Value.Button.onClick.RemoveAllListeners();        
    }

    public void AddCommand(ICommand command)
    {
        if (command is IBuildCommand commandConvertBuild) { OnClickBuild += commandConvertBuild.Build; _buttonsByExecutorType[typeof(IBuildCommand)].CountAction++; };
        if (command is IMoveCommand commandConvertMove) { OnClickMove += commandConvertMove.Move; _buttonsByExecutorType[typeof(IMoveCommand)].CountAction++; };
        if (command is IStopCommand commandConvertStop) { OnClickStop += commandConvertStop.Stop; _buttonsByExecutorType[typeof(IStopCommand)].CountAction++; };
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        foreach (var item in _buttonsByExecutorType)
        {
            if (item.Value.Button!=null) item.Value.Button.gameObject.SetActive(item.Value.CountAction > 0 ? true : false);
        }
    }

    public void RemoveCommand(ICommand command)
    {
        if (command is IBuildCommand commandConvertBuild) { OnClickBuild -= commandConvertBuild.Build; _buttonsByExecutorType[typeof(IBuildCommand)].CountAction--; };
        if (command is IMoveCommand commandConvertMove) { OnClickMove -= commandConvertMove.Move; _buttonsByExecutorType[typeof(IMoveCommand)].CountAction--; };
        if (command is IStopCommand commandConvertStop) { OnClickStop -= commandConvertStop.Stop; _buttonsByExecutorType[typeof(IStopCommand)].CountAction--; };
        UpdateButtons();
    }
}
