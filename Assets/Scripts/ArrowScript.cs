using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowScript : MonoBehaviour
{
    public float turningSpeed = 15.0f;
    public Transform ball;
   private void  LateUpdate()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            float mouseMovementX = Mouse.current.delta.x.ReadValue();

           if(Mathf.Abs(mouseMovementX) > 0.1f)
           {

               transform.RotateAround(ball.position,Vector3.up , mouseMovementX * Time.deltaTime * turningSpeed);
          }

        }
    }
}
