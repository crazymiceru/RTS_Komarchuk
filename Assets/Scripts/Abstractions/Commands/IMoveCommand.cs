
using UnityEngine;

public interface IMoveCommand : ICommand
{
    void Move(Vector3 position);
}
