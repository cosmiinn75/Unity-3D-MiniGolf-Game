using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 100.0f;


    private void Update()
    {

        transform.Rotate(0, 0,speed*Time.deltaTime);
    }
}
