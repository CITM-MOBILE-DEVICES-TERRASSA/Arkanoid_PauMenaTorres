 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 10f; 
    public float maxX = 7.5f;
    public float minX = -7.5f;
    float horizontalMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontalMovement = Input.GetAxis("Horizontal");
        Debug.Log("Run..." + horizontalMovement);
        if ((horizontalMovement > 0 && transform.position.x < maxX) || (horizontalMovement < 0 && transform.position.x > minX))
        {
            transform.position += Vector3.right * horizontalMovement * Time.deltaTime * speed;
            
        }
    }

}
