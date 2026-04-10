using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float turningSpeed = 15.0f;
    public Transform player;
    public Renderer pipeRenderer; // TRAGE TUNELUL (PIPE) AICI ÎN INSPECTOR
    public LayerMask obstacleLayer;

    Vector3 offset;
    private bool isPipeHidden = false;

    private void Start()
    {
        if (player != null)
            offset = player.position - transform.position;
    }

    private void LateUpdate()
    {
        if (player == null) return;
        transform.position = player.position - offset;

        if (Mouse.current.rightButton.isPressed)
        {
            float mouseMovementX = Mouse.current.delta.x.ReadValue();
            if (Mathf.Abs(mouseMovementX) > 0.1f)
            {
                transform.RotateAround(player.position, Vector3.up, mouseMovementX * Time.deltaTime * turningSpeed);
            }
            offset = player.position - transform.position;
        }

        CheckForObjectInWay();
    }

    void CheckForObjectInWay()
    {
        RaycastHit hit;
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(player.position, transform.position);

        Debug.DrawRay(transform.position, direction * distance, Color.red);

        // Dacă raza lovește CEVA pe layer-ul Obstacle
        if (Physics.Raycast(transform.position, direction, out hit, distance, obstacleLayer))
        {
            if (pipeRenderer != null && !isPipeHidden)
            {
                pipeRenderer.enabled = false; // Ascunde tunelul
                isPipeHidden = true;
            }
        }
        else
        {
            if (pipeRenderer != null && isPipeHidden)
            {
                pipeRenderer.enabled = true; // Arată tunelul înapoi
                isPipeHidden = false;
            }
        }
    }
}