namespace Command
{
    public class SpecialControl: Command
    {
        private readonly PlayerCircleController _playerController;

        public SpecialControl(PlayerCircleController playerController)
        {
            _playerController = playerController;
        }
        
        public override void Execute()
        {
            _playerController.zoomMovement.Zoom();
        }

        public override void Undo()
        {
            _playerController.zoomMovement.Zoom();
        }
    }
}