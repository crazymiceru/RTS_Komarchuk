using System.Collections;
using UnityEngine;

public class UnitMobe : Unit, IMoveCommand, IStopCommand
{
    private Rigidbody _rigidBody;
    private Vector3 _target;

    [SerializeField] private float _powerMove = 1;
    [SerializeField] private float _maxSpeed = 5;
    [SerializeField] private float _minDistEndMove = 2;
    private Coroutine coroutineMove;

    protected override void Awake()
    {
        base.Awake();
        _rigidBody = GetComponent<Rigidbody>();
    }

    public override void DoSelect()
    {
        if (IsSelect) _commandButtonView.AddCommand(this);
        else _commandButtonView.RemoveCommand(this);
    }

    public void Move(Vector3 position)
    {
        _target = position;
        coroutineMove=StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        Vector3 direct;
        do
        {
            direct = _target - transform.position;
            _rigidBody.AddForce(_powerMove *_rigidBody.mass * direct.normalized);
            if (_rigidBody.velocity.sqrMagnitude > _maxSpeed * _maxSpeed) _rigidBody.velocity = _rigidBody.velocity.normalized * _maxSpeed;
             yield return new WaitForSeconds(0.1f);
        } while (direct.sqrMagnitude > _minDistEndMove);

    }

    public void Stop()
    {
        if (coroutineMove != null) StopCoroutine(coroutineMove);
    }
}
