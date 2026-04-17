
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
    private bool isLevelFinished = false;
    private void Start()
    {
        DisplayStrokeText();
        rb = GetComponent<Rigidbody>();
        //Dezactivam sageata la start
        arrow.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        lastPos = transform.position;
        strokes = 0;
    }



    private void Update()
    {

        //La viteza asta mica consideram mingea oprita
        CheckForStop();
        if (!isMoving && !isLevelFinished && Time.timeScale != 0)
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

        //MouseBtn Pressed
        if (Mouse.current.leftButton.wasPressedThisFrame && !isMoving)
        {
            if (transform.parent == null)
            {
                lastPos = transform.position;
            } // Retinem ultima pozitie doar daca nu e pe platforma

            //Initializam forta
            force = 1.0f;
            timer = 0;
            //Aratam sageata pe ecran foarte mica
            arrowScale = 0.01f;
            arrow.gameObject.GetComponent<Transform>().localScale = new Vector3(0.05f, arrowScale, 0.05f);

        }



        //MoutBtn Held
        if (Mouse.current.leftButton.isPressed && force >= 1.0f && isMoving == false)
        {
            
            ArrowColor(); //Schimbam culoarea sagetii bazat pe procentul fortei din forceMax

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
                force = forceMax; // Daca e la forta maxima pastram aceeasi forta
                isAtFullForce = true;
            }

            if (isAtFullForce)
            { 
                float maxTime = 1.0f;
                cancelTimer += Time.deltaTime;
                if(cancelTimer > maxTime) // Daca ramane la forta maxim prea mult timp resetam sageata si forta
                {
                    shotCancelled = true;
                    force = 0.0f;
                    arrow.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    cancelTimer = 0.0f;
                    isAtFullForce = false;
                }
            }


        }


        //MouseBtn Released
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
            arrow.gameObject.GetComponent<SpriteRenderer>().enabled = false; //Resetam tot
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
        rb.AddForce(finalDir, ForceMode.VelocityChange); // Adaugam viteza bazat pe unde se uita camera(unde arata sageata)
    }


    void RotateArrow() //Rotim sageata in functie de camera
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
            Vector3 reflectedDir = Vector3.Reflect(rb.linearVelocity, normal).normalized; // Reflectam viteza pe directia normalei
            rb.AddForce(reflectedDir*bumperSpeed, ForceMode.VelocityChange); // Aplicam o forta pe acea directie reflectata
        }


        if (collision.gameObject.CompareTag("isTerrain") )
        {
            if (!isResetting) {
                isResetting = true;
                StartCoroutine(ResetPositionAfterDelay()); // Se reseteaza pozitia dupa cateva momente ce a atins pamantul
            }

            
        }


        if (collision.gameObject.CompareTag("isPlatform"))
        {
            transform.SetParent(collision.transform); // Facem sa se miste in acelasi timp cu platforma
            rb.linearDamping = 2.0f; // Incetinim mingea cand e pe platforma
            
        }


    }



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("isPlatform"))
        {
            transform.SetParent(null); 
            rb.linearDamping = 1.0f; // Resetam la normal
        }
    }

    IEnumerator ResetPositionAfterDelay()
    {
       
        yield return new WaitForSeconds(0.8f); // Dupa 0.8 secunde


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("isHole") && !isLevelFinished) //Oprim mingea sa nu mai poata sa loveasca jucatorul
        {
            isLevelFinished = true;
            arrow.SetActive(false);
            ResetSpeed();
            rb.isKinematic = true;
        }
    }
    private void ResetSpeed()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }


}
