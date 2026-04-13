using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float amplitude = 1.0f;
    [SerializeField] private string axis = "x"; // x , y , z
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Transform ball;
    Vector3 startPos;
    Vector3 ballPos;
    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {

        float movement = amplitude * Mathf.Sin(speed * Time.time);
        Vector3 direction = Vector3.zero;

        string cleanAxis = axis.ToLower();

        if (cleanAxis.Equals("x")) direction = Vector3.left;
        else if (cleanAxis.Equals("y")) direction = Vector3.up;
        else if (cleanAxis.Equals("z")) direction = Vector3.forward;


        transform.position = startPos + direction * movement;
        
      

    }


  



}
