
public interface ICommandExecutor<T>:ICommand where T : ICommand
{
    void ExecuteCommand(T command);
}