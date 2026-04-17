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
            isHidden[i] = false; // Facem toate obiectele alese sa nu fie ascunse
        }


        if (player != null)
            offset = player.position - transform.position; 
    }

    private void LateUpdate()
    {
        if (player == null) return;
        transform.position = player.position - offset; // Camera urmareste jucatorul

        if (Mouse.current.rightButton.isPressed) 
        {
            float mouseMovementX = Mouse.current.delta.x.ReadValue();
            if (Mathf.Abs(mouseMovementX) > 0.1f)
            {
                transform.RotateAround(player.position, Vector3.up, mouseMovementX * Time.deltaTime * turningSpeed);
            }
            offset = player.position - transform.position;
        } // Facem sa se roteasca camera in jurul jucatorului

        CheckForObjectInWay();
    }

    void CheckForObjectInWay()
    {
        RaycastHit hit;
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(player.position, transform.position);

        Debug.DrawRay(transform.position, direction * distance, Color.red);

        if (Physics.Raycast(transform.position, direction, out hit, distance, obstacleLayer))
            //Daca atinge un obiect din layerul obstacle pe directia dintre jucator si camera tine minte tot 
            //in variabila hit
        {
            for (int i = 0; i < obstaclesToHide.Length; i++)
            {
                if (hit.transform.IsChildOf(obstaclesToHide[i].transform) || hit.transform == obstaclesToHide[i].transform)// daca atinge un copi al obiectului din cele din lista sau atinge chiar obiectul
                {
                    if (!isHidden[i]) // si nu e ascuns
                    {
                        Renderer[] childRenderers = obstaclesToHide[i].GetComponentsInChildren<Renderer>();

                        foreach (Renderer r in childRenderers)
                        {
                   
                            if (r.gameObject.layer != 6) // layer 6 e layerul cubului senzor
                            {
                                r.enabled = false; // oprim toti copiii obiectului mai putin cel de pe layerul 6 ca altfel nu l ar mai detecta
                            }
                        }

                        
                        if (obstaclesToHide[i].gameObject.layer != 6)
                            obstaclesToHide[i].enabled = false; //am oprit si obiectul respectiv

                        isHidden[i] = true; // l-am facut ascuns
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
            } // Resetam tot daca nu e hidden deja(e hidden DOAR daca raza atinge cubul respectiv)
        }
    }
}