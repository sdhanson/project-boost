using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float shipThrust = 100f;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles; 

    Rigidbody rigidBody;
    AudioSource thrustAudio;

    // made a type State that can either be alive, dying, or transcending
    enum State { Alive, Dying, Transcending };
    // create a variable of this type and give it a value
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        
        rigidBody = GetComponent<Rigidbody>();
        thrustAudio = GetComponent<AudioSource>();
		
	}
	
	// FixedUpdate is called every 0.02 sec
	void FixedUpdate () {

        if(state == State.Alive) {

            Thrust();
            Rotate();
            
        }

		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        if(state != State.Alive) { return; }

        switch(collision.gameObject.tag) {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence() {
        
        state = State.Transcending;
        // give method name as string, and 1 second delay

        // auto stop when scene loads!
        successParticles.Play();
        Invoke("LoadNextScene", 1f);

    }

    private void StartDeathSequence()
    {
        
        state = State.Dying;
        deathParticles.Play();
        Invoke("LoadFirstLevel", 1f);

    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Thrust()
    {
        float thrustThisFrame = shipThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);

            if (!thrustAudio.isPlaying)
            {
                thrustAudio.Play();
            }

            mainEngineParticles.Play();

        }
        else if (thrustAudio.isPlaying)
        {
            thrustAudio.Stop();
            mainEngineParticles.Stop();
        }

    }

    private void Rotate()
    {

        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control

    }


}
