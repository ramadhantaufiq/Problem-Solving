using System;
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
        public CircleController circleController;
        public InputType inputType;
        
        private Queue<Command> commands = new Queue<Command>();

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

        }

        private Command HandleKeyboardMovement()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            
            return new KeyboardMove(circleController, inputX, inputY);
        }

        private Command HandleCursorMovement()
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return new CursorMove(circleController, cursorPos);
        }
            
    }
}
