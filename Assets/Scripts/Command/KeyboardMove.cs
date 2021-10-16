namespace Command
{
    public class KeyboardMove : Command
    {
        private PlayerCircleController _playerCircleController;
        private float _x, _y;

        public KeyboardMove(PlayerCircleController playerCircleController, float x, float y)
        {
            _playerCircleController = playerCircleController;
            _x = x;
            _y = y;
        }
        
        public override void Execute()
        {
            _playerCircleController.circleMovement.Move(_x, _y);
        }

        public override void Undo()
        {
            _playerCircleController.circleMovement.Move(-_x, -_y);
        }
    }
}