namespace Command
{
    public class KeyboardMove : Command
    {
        private CircleController _circleController;
        private float _x, _y;

        public KeyboardMove(CircleController circleController, float x, float y)
        {
            _circleController = circleController;
            _x = x;
            _y = y;
        }
        
        public override void Execute()
        {
            _circleController.Move(_x, _y);
        }

        public override void Undo()
        {
            _circleController.Move(-_x, -_y);
        }
    }
}