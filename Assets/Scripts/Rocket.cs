using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource thrustAudio;
    bool thrustAudioOn = false;

	// Use this for initialization
	void Start () {
        
        rigidBody = GetComponent<Rigidbody>();
        thrustAudio = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
		
	}

    private void ProcessInput()
    {
        
        if(Input.GetKey(KeyCode.Space)) 
        {
            print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up);

            if(!thrustAudioOn) 
            {
                thrustAudio.Play();
                thrustAudioOn = true;    
            }

        }

        if(thrustAudioOn && !Input.GetKey(KeyCode.Space)) 
        {
            thrustAudio.Stop();
            thrustAudioOn = false; 
        }

        if(Input.GetKey(KeyCode.A)) 
        {
            print("Rotating left");
            transform.Rotate(Vector3.forward);
        } 

        else if (Input.GetKey(KeyCode.D)) 
        {
            print("Rotating right");
            transform.Rotate(-Vector3.forward);
        }

    }
}
