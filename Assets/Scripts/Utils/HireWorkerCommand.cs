public class HireWorkerCommand : ICommand
{
    private readonly Base _base;

    public HireWorkerCommand(Base baseInstance)
    {
        _base = baseInstance;
    }

    public void Execute() =>
        _base.HireWorker();
}