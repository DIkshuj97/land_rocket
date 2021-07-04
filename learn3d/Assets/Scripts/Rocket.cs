using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{   //Speeds
    [SerializeField] float rcsThrust = 50f;
    [SerializeField] float thrustspeed = 50f;
    //audioclips
    [SerializeField] AudioClip mainengine;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip success;
    //particles
    [SerializeField] ParticleSystem mainengineparticle;
    [SerializeField] ParticleSystem deathparticle;
    [SerializeField] ParticleSystem successparticle;

    Rigidbody myrigidbody;
    AudioSource myaudio;

    bool isTransitioning = false;
    bool collisionsAreDisabled = false;


    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody>();
        myaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning)
        {
            Thrust();
            Rotate();
        }
        if (Debug.isDebugBuild)
        {
            Respondtodebugkeys();
        }
    }

    private void Respondtodebugkeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            FindObjectOfType<LevelLoader>().LoadNextScene();
        }

        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionsAreDisabled = !collisionsAreDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning || collisionsAreDisabled)
        {
            return;
        }

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                StartSuccessScene();
                break;

            default:
                StartDeathscene();
                break;
        }
    }

    private void StartSuccessScene()
    {
        isTransitioning = true;
        myaudio.Stop();
        myaudio.PlayOneShot(success);
        successparticle.Play();
        FindObjectOfType<LevelControl>().WinCondition();
    }
     private void StartDeathscene()
    {
        isTransitioning=true;
        myaudio.Stop();
        myaudio.PlayOneShot(Death);
        deathparticle.Play();
        FindObjectOfType<LevelControl>().HandleLoseCondition();
    }

 
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            myaudio.Stop();
            mainengineparticle.Stop();
        }
    }

    private void ApplyThrust()
    {
        myrigidbody.AddRelativeForce(Vector3.up * thrustspeed);
        if (!myaudio.isPlaying)
        {
            myaudio.PlayOneShot(mainengine);
        }
        mainengineparticle.Play();
    }

    private void Rotate()
    {
        myrigidbody.angularVelocity = Vector3.zero;
        
        float rotationspeed = Time.deltaTime * rcsThrust;
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward*rotationspeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotationspeed);
        }
    }
}