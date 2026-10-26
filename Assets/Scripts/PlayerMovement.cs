using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 30f;
    [SerializeField] private float rotationForce = 2.5f;

    [SerializeField] private ParticleSystem thrustParticleSystem;
    [SerializeField] private ParticleSystem leftRotationParticleSystem;
    [SerializeField] private ParticleSystem rightRotationParticleSystem;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip thrustAudioClip;


    Rigidbody rb;
    AudioSource audioSource;

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThurst();
        ProcessRotation();
    }

    

    private void ProcessThurst()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessRotation()
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        
        float rotationValue = rotation.ReadValue<float>();

        if (rotationValue < 0)
        {
            RotateRight();
        }
        else if (rotationValue > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotation();

        }
    }

    private void StopRotation()
    {
        //TODO: Stop rotation sound and particles
    }

    private void RotateLeft()
    {
        transform.Rotate(Vector3.back * rotationForce);
        if(!leftRotationParticleSystem.isPlaying)
        {
            rightRotationParticleSystem.Stop();
            leftRotationParticleSystem.Play();
        }
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.forward * rotationForce);
        if(!rightRotationParticleSystem.isPlaying)
        {
            leftRotationParticleSystem.Stop();
            rightRotationParticleSystem.Play();
        }
            
   
    }

    /// <summary>
    /// Star thrusting, and starts the partycle system and the audio of thrusting
    /// </summary>

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * moveForce);



        if(!thrustParticleSystem.isPlaying)
        {
            thrustParticleSystem.Play();
        }
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudioClip);
        }
    }

    /// <summary>
    /// Stop thrusting and stop partycles and audio when the thrust input is released.
    /// </summary>

    private void StopThrusting()
    {
        thrustParticleSystem.Stop();
        audioSource.Stop();
    }
}
