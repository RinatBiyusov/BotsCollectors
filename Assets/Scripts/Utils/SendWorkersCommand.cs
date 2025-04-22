public class SendWorkersCommand : ICommand
{
    private readonly Base _base;

    public SendWorkersCommand(Base baseInstance)
    {
        _base = baseInstance;
    }

    public void Execute() =>
        _base.DistributeWorkers();
}