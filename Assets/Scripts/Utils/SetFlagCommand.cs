    public class SetFlagCommand : ICommand
    {
        
        private readonly Base _base;
        private readonly FlagPlacementHandler _flagPlacementHandler;

        public SetFlagCommand(Base targetBase, FlagPlacementHandler flagPlacementHandler)
        {
            _base = targetBase;
            _flagPlacementHandler = flagPlacementHandler;
        }

        public void Execute()
        {
            _flagPlacementHandler.PlaceFlag(_base);
        }
    }
