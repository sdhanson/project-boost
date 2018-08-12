using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float shipThrust = 100f;

    Rigidbody rigidBody;
    AudioSource thrustAudio;

	// Use this for initialization
	void Start () {
        
        rigidBody = GetComponent<Rigidbody>();
        thrustAudio = GetComponent<AudioSource>();
		
	}
	
	// FixedUpdate is called every 0.02 sec
	void FixedUpdate () {

        Thrust();
        Rotate();
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag) {
            case "Friendly":
                // do nothing
                print("OK");
                break;
            case "Finish":
                SceneManager.LoadScene(1);
                // do nothing
                break;
            default:
                print("Dead");
                SceneManager.LoadScene(0);
                // do nothing
                break;
        }
    }

    private void Thrust()
    {
        float thrustThisFrame = shipThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);

            if (!thrustAudio.isPlaying)
            {
                thrustAudio.Play();
            }

        }
        else if (thrustAudio.isPlaying)
        {
            thrustAudio.Stop();
        }

    }

    private void Rotate()
    {

        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A))
        {
            print("Rotating left");
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            print("Rotating right");
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control

    }


}
