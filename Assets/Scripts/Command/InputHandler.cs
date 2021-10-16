using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public enum InputType
    {
        Keyboard,
        Cursor
    }
    
    public class InputHandler: MonoBehaviour
    {
        public PlayerCircleController playerCircleController;
        public InputType inputType;
        
        private Queue<Command> commands = new Queue<Command>();

        private void Start()
        {
            playerCircleController = GetComponent<PlayerCircleController>();
        }

        private void FixedUpdate()
        {
            if (inputType == InputType.Keyboard)
            {
                Command moveCommand = HandleKeyboardMovement();
                if (moveCommand != null)
                {
                    commands.Enqueue(moveCommand);
                    moveCommand.Execute();
                }
            }
            else if (inputType == InputType.Cursor)
            {
                Command moveCommand = HandleCursorMovement();
                if (moveCommand != null)
                {
                    commands.Enqueue(moveCommand);
                    moveCommand.Execute();
                }
            }

            if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
            {
                Command specialCommand = HandleSpecialMove();
                commands.Enqueue(specialCommand);
                specialCommand.Execute();
            }
        }

        private Command HandleKeyboardMovement()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            
            return new KeyboardMove(playerCircleController, inputX, inputY);
        }

        private Command HandleCursorMovement()
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return new CursorMove(playerCircleController, cursorPos);
        }

        private Command HandleSpecialMove()
        {
            return new SpecialControl(playerCircleController);
        }
    }
}
