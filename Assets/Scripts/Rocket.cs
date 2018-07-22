using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource thrustAudio;

	// Use this for initialization
	void Start () {
        
        rigidBody = GetComponent<Rigidbody>();
        thrustAudio = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {

        Thrust();
        Rotate();
		
	}

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up);

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
        
        if (Input.GetKey(KeyCode.A))
        {
            print("Rotating left");
            transform.Rotate(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            print("Rotating right");
            transform.Rotate(-Vector3.forward);
        }

        rigidBody.freezeRotation = false; // resume physics control

    }


}
