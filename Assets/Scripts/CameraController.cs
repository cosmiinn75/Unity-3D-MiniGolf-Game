using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float turningSpeed = 15.0f;
    public Transform player;
    public Renderer[] obstaclesToHide; 
    public LayerMask obstacleLayer;

    Vector3 offset;
    private bool[] isHidden;

    private void Start()
    {
        isHidden = new bool[obstaclesToHide.Length];
        for(int i = 0; i < obstaclesToHide.Length; i++)
        {
            isHidden[i] = false;
        }


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

        if (Physics.Raycast(transform.position, direction, out hit, distance, obstacleLayer))
        {
            for (int i = 0; i < obstaclesToHide.Length; i++)
            {
                if (hit.transform.IsChildOf(obstaclesToHide[i].transform) || hit.transform == obstaclesToHide[i].transform)
                {
                    if (!isHidden[i])
                    {
                        Renderer[] childRenderers = obstaclesToHide[i].GetComponentsInChildren<Renderer>();

                        foreach (Renderer r in childRenderers)
                        {
                   
                            if (r.gameObject.layer != 6) // layer 6 e layerul cubului senzor
                            {
                                r.enabled = false;
                            }
                        }

                        
                        if (obstaclesToHide[i].gameObject.layer != 6)
                            obstaclesToHide[i].enabled = false;

                        isHidden[i] = true;
                    }
                    return;
                }
            }
        }

      
        for (int j = 0; j < obstaclesToHide.Length; j++)
        {
            if (isHidden[j]) 
            {
                Renderer[] childRenderers = obstaclesToHide[j].GetComponentsInChildren<Renderer>();
                foreach (Renderer r in childRenderers)
                {   if (r.gameObject.layer != 6)
                    {
                        r.enabled = true;
                    }
                }
                obstaclesToHide[j].enabled = true;
                isHidden[j] = false;
            }
        }
    }
}