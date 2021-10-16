using UnityEngine;

namespace Command
{
    public class CursorMove : Command
    {
        private readonly PlayerCircleController _playerCircleController;
        private Vector2 _initialPosition;
        private Vector2 _targetPosition;

        public CursorMove(PlayerCircleController playerCircleController, Vector2 targetPosition)
        {
            _playerCircleController = playerCircleController;
            _targetPosition = targetPosition;
        }
        
        public override void Execute()
        {
            _initialPosition = _playerCircleController.transform.position;
            _playerCircleController.circleMovement.Move(_targetPosition);
        }

        public override void Undo()
        {
            _playerCircleController.circleMovement.Move(_initialPosition);
        }
    }
}
