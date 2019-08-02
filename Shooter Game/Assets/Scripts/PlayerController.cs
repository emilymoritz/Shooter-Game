using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public GameObject shotL;
    public GameObject shotR;
    public Transform shotSpawn;
    public Transform shotSpawnL;
    public Transform shotSpawnR;
    public float fireRate;

    private float nextFire;

    private Rigidbody rb;

    AudioSource audioData;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
            Debug.Log("started");
        }

        else if (Input.GetKeyDown(KeyCode.X) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shotL, shotSpawnL.position, shotSpawnL.rotation);
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
            Debug.Log("started");
        }

        else if (Input.GetKeyDown(KeyCode.C) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shotR, shotSpawnR.position, shotSpawnR.rotation);
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
            Debug.Log("started");
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            speed = speed * 2;
        }

    }
}