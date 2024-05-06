using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [SerializeField] private float damaged=0;
    [SerializeField] private float fuel = 0;
    [SerializeField] private float capacity=100;
    [SerializeField] private float laps;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody rb;
    private void Awake()
    {
        rb= GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * verticalInput * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
        if (verticalInput != 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float rotation = horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotation);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            damaged += 5;
            if (damaged==100)
                SceneManager.LoadScene(0);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Red")
        {
            fuel += 20;
            if (fuel > capacity)
                fuel = capacity;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Blue")
        {
            capacity += 10;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Box30")
        {
            damaged -= 30;
            if (damaged < 0)
                damaged = 0;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Boxfull")
        {
            damaged = 0;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Win")
        {
                laps += 1;
        }
    }
}
