
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float turningSpeed = 15.0f;
    public float force = 0.0f;
    public float forceMax = 12.0f;
    float timer = 0;
    public float cooldownForce = 0.2f;
    private bool isMoving = false;
    public GameObject arrow;
    private float arrowScale = 0.1f;
    private Color arrowColor = new Color(1,1,1);
    public TextMeshProUGUI strokeText;
    public int strokes = 0;
    private Vector3 lastPos;
    private bool isResetting = false;
    private bool isAtFullForce = false;
    private float cancelTimer = 0.0f;
    private bool shotCancelled = false;
    public AudioClip puttSound;
    private void Start()
    {
        DisplayStrokeText();
        rb = GetComponent<Rigidbody>();
        //Dezactivam sageata la start
        arrow.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        lastPos = transform.position;
    }



    private void Update()
    {

        //La viteza asta mica consideram mingea oprita
        CheckForStop();
        if (!isMoving)
        {
            HandleInput();
        }
    }

    private void LateUpdate()
    {
        if (!isMoving)
        {
            RotateArrow();
        }
    }






    void DisplayStrokeText()
    {
        strokeText.text = "Strokes: " + strokes.ToString();
    }


    void CheckForStop()
    {
        if (rb.linearVelocity.magnitude <= 0.05f && rb.isKinematic == false)
        {
            ResetSpeed();
            isMoving = false;
        }
    }

    void HandleInput()
    {

        //pressed
        if (Mouse.current.leftButton.wasPressedThisFrame && !isMoving)
        {
            if (transform.parent == null)
            {
                lastPos = transform.position;
            } // retinem ultima pozitie

            //Initializam forta
            force = 1.0f;
            timer = 0;
            //aratam sageata pe ecran foarte mica
            arrowScale = 0.01f;
            arrow.gameObject.GetComponent<Transform>().localScale = new Vector3(0.05f, arrowScale, 0.05f);

        }



        //held
        if (Mouse.current.leftButton.isPressed && force >= 1.0f && isMoving == false)
        {
            
            ArrowColor();
            arrow.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            timer += Time.deltaTime;

            if (timer >= cooldownForce && !isAtFullForce)
            {   //Marim sageata
                force += 1.0f;
                
                arrow.gameObject.GetComponent<Transform>().localScale = new Vector3(0.05f, arrowScale, 0.05f);
                arrow.gameObject.GetComponent<SpriteRenderer>().color = arrowColor;
                if (force < forceMax)
                {
                    //Daca nu e la forta maxima crestem sageata
                    arrowScale += 0.0075f;
                }

                timer = 0;
            }

            if (force > forceMax)
            {
                force = forceMax;
                isAtFullForce = true;
            }

            if (isAtFullForce)
            { 
                float maxTime = 1.0f;
                cancelTimer += Time.deltaTime;
                if(cancelTimer > maxTime)
                {
                    shotCancelled = true;
                    force = 0.0f;
                    arrow.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    cancelTimer = 0.0f;
                    isAtFullForce = false;
                }
            }


        }


        //released
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (force >= 1.0f && !shotCancelled)
            {
                strokes++;
                DisplayStrokeText();
                Shoot();
                isMoving = true;
            }

            force = 0;
            isAtFullForce = false;
            shotCancelled = false;
            cancelTimer = 0.0f;
            arrow.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    void Shoot()
    {
        if(puttSound != null)
        {
            AudioSource.PlayClipAtPoint(puttSound, transform.position, 1.0f);
        }
        float camY = Camera.main.transform.eulerAngles.y;
        Vector3 finalDir = Quaternion.Euler(0, camY, 0) * Vector3.forward * force;
        rb.AddForce(finalDir, ForceMode.VelocityChange);
      //  Debug.DrawRay(transform.position, finalDir * 50.0f, Color.red, 10.0f);
    }


    void RotateArrow()
    {
       arrow.transform.position = transform.position;
        float camY = Camera.main.transform.eulerAngles.y;
        arrow.transform.rotation = Quaternion.Euler(0, camY, 0);

  


    }


    void ArrowColor()
    {
            if(force <= 0.33f * forceMax)
        {
            arrowColor = Color.green;
        }
        else if(force >= 0.33f * forceMax && force <= 0.66f*forceMax)
        {
            arrowColor.r += 2*Time.deltaTime;
        }
        else if(force >= 0.6f*forceMax && force <= forceMax)
        {
            arrowColor.g -= 2*Time.deltaTime;
        }           



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("isBumper"))
        {
            Vector3 normal = collision.contacts[0].normal;
            float bumperSpeed = 3.0f;
            Vector3 reflectedDir = Vector3.Reflect(rb.linearVelocity, normal).normalized;
            rb.AddForce(reflectedDir*bumperSpeed, ForceMode.VelocityChange);
        }


        if (collision.gameObject.CompareTag("isTerrain") )
        {
            if (!isResetting) {
                isResetting = true;
                StartCoroutine(ResetPositionAfterDelay()); 
            }

            
        }


        if (collision.gameObject.CompareTag("isPlatform"))
        {
            transform.SetParent(collision.transform);
            rb.linearDamping = 2.0f;
            
        }


    }



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("isPlatform"))
        {
            transform.SetParent(null);
            rb.linearDamping = 1.0f;
        }
    }

    IEnumerator ResetPositionAfterDelay()
    {
       
        yield return new WaitForSeconds(0.8f);


        if (rb != null)
        {
            ResetSpeed();
        }

        rb.isKinematic = true;

        transform.position = lastPos;

        yield return new WaitForSeconds(0.1f);

        rb.isKinematic = false;
        isResetting = false;
        isMoving = false;
    }
  
    private void ResetSpeed()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

}
