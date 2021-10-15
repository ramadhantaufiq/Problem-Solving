using UnityEngine;

namespace Command
{
    public class CursorMove : Command
    {
        private CircleController _circleController;
        private Vector2 _initialPosition;
        private Vector2 _targetPosition;

        public CursorMove(CircleController circleController, Vector2 targetPosition)
        {
            _circleController = circleController;
            _targetPosition = targetPosition;
        }
        
        public override void Execute()
        {
            _initialPosition = _circleController.transform.position;
            _circleController.Move(_targetPosition);
        }

        public override void Undo()
        {
            _circleController.Move(_initialPosition);
        }
    }
}
