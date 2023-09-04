using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 180f;
    public float BulletSpeed = 100;

    public float tiltAngle = 30f;

    public float maxVerticalAngle = 80f;
    public float maxHorizontalAngle = 45f;

    public float maxXPosition = 10f;
    public float minXPosition = -10f;

    public Transform firePoint;
    public float fireRate = 0.5f;

    private float currentVerticalAngle = 0f;
    private float currentHorizontalAngle = 0f;
    private float nextFireTime = 0f;

    private Rigidbody rb;
    private Objectpool projectilePool;
    private bool isCursorHidden = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        projectilePool = FindObjectOfType<Objectpool>();
    }

    private void Update()
    {
       
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 localMoveDirection = transform.forward * Mathf.Clamp01(verticalInput) +
                                     transform.right * horizontalInput;

        Vector3 movement = localMoveDirection * moveSpeed * Time.deltaTime;

     
        Vector3 newPosition = rb.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, minXPosition, maxXPosition);
        newPosition.y = Mathf.Clamp(newPosition.y, minXPosition, maxXPosition);
       
        rb.MovePosition(newPosition);

        float rotationInputX = Input.GetAxis("Mouse X");

        Quaternion horizontalRotation = Quaternion.Euler(0f, rotationInputX * rotationSpeed * Time.deltaTime, 0f);

        rb.MoveRotation(rb.rotation * horizontalRotation);

        float rotationInputY = Input.GetAxis("Mouse Y");

        Quaternion verticalRotation = Quaternion.Euler(-rotationInputY * rotationSpeed * Time.deltaTime, 0f, 0f);

        currentVerticalAngle += -rotationInputY * rotationSpeed * Time.deltaTime;
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, -maxVerticalAngle, maxVerticalAngle);

        currentHorizontalAngle += rotationInputX * rotationSpeed * Time.deltaTime;
        currentHorizontalAngle = Mathf.Clamp(currentHorizontalAngle, -maxHorizontalAngle, maxHorizontalAngle);

        Quaternion newRotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0f);

        
        float tiltInput = Input.GetAxis("Horizontal");
        Quaternion tiltRotation = Quaternion.Euler(0f, 0f, -tiltInput * tiltAngle);
        newRotation *= tiltRotation;

        rb.MoveRotation(newRotation);
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
        }


        if (Input.GetMouseButtonDown(1))
        {
           
            isCursorHidden = !isCursorHidden;

            Cursor.visible = !isCursorHidden;

            Cursor.lockState = isCursorHidden ? CursorLockMode.Locked : CursorLockMode.None;

        }
    }

    private void Shoot()
    {

        nextFireTime = Time.time + fireRate;

        GameObject projectile = projectilePool.GetpoolObject();
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.GetComponent<Rigidbody>().velocity = transform.forward * BulletSpeed;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Asteroid"))
        {

            float damage = 40f;
            PlayerHealth.currentHealth -= damage;

            if (PlayerHealth.currentHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
