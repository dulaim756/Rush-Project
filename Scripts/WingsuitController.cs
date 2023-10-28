using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WingsuitController : MonoBehaviour
{
    [SerializeField] float min_speed = 12.5f;
    [SerializeField] float max_speed = 13.8f;

    [SerializeField] float min_drag = 4;
    [SerializeField] float max_drag = 6;

    Rigidbody rb;

    private Vector3 rot;

    public float percentage;

    public AudioMixer am;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rot = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        //rotates the player
        //X
        rot.x += 20 * Input.GetAxis("Vertical") * Time.deltaTime;
        rot.x = Mathf.Clamp(rot.x, 0, 45);
        //Y
        rot.y += 20 * Input.GetAxis("Horizontal") * Time.deltaTime;
        //Z
        rot.z += -5 * Input.GetAxis("Horizontal") * Time.deltaTime;
        rot.z = Mathf.Clamp(rot.z, -5, 5);
        transform.rotation = Quaternion.Euler(rot);

        percentage = rot.x / 45;
        // Drag: Fast(4), Slow(6)
        float mod_drag = (percentage * (min_drag - max_drag)) + max_drag;
        // Speed: Fast(13.8), Slow(12.5)
        float mod_speed = percentage * (max_speed - min_speed) + min_speed;

        rb.drag = mod_drag;
        Vector3 localV = transform.InverseTransformDirection(rb.velocity);
        localV.z = mod_speed;
        rb.velocity = transform.TransformDirection(localV);

        am.SetFloat("Pitch", 1 + percentage);
    }
}
