using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed = 2f;
    public Vector3 moveDir = Vector3.zero;
    private bool canrun = false;
    private bool canjump = false;
    public Text Health;
    public Text Hungerlevel;

    private HungerSystem hungerSystem;
    private HealthSystem healthSystem;
    public float walkspeed = 10.0f;
    public float jumpspeed = 20.0f;
    public float rotationspeed = 30.00f;
    public float sprintspeed = 20.0f;
    public float health = 100.0f;
    public float hungerlevel = 0f;
    float timeLeft = 3600.0f,elapsedtime=0f;

    private CharacterController characterController;
    private Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
        hungerSystem = GetComponent<HungerSystem>();
       // hungerSystem.SetHealthSystem(healthSystem)
        

    }


    // Update is called once per frame
    void Update()
    {
        speed = walkspeed;
        canrun = false;

      //  Health.text = health.ToString();
        //Hungerlevel.text = hungerlevel.ToString();

      /*  if (elapsedtime > timeLeft)
        {
            hungerlevel += 10;
            elapsedtime = 0;
            if (hungerlevel >= 80)
            {
                health -= 10;
            }
        }
        else
        {
            elapsedtime += Time.deltaTime;
        }*/

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintspeed;
            canrun = true;
        }

        if (characterController.isGrounded)
        {
            moveDir = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("canjump", true);
                moveDir.y = jumpspeed;
            }
        }
       health= Mathf.Clamp(health, 0, 100);
       hungerlevel= Mathf.Clamp(hungerlevel, 0, 100);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("canjump", false);
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationspeed * Time.deltaTime, 0));
        moveDir.y -= 11.8f * Time.deltaTime;
        characterController.Move(moveDir * Time.deltaTime);

        var magnitude = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;
        animator.SetFloat("speed", magnitude);
        animator.SetBool("canrun", canrun);

    }

   /* void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("pineapple"))
        {
            Destroy(hit.gameObject);
            health += 10;
        }

        if (hit.gameObject.CompareTag("meat"))
        {
            Destroy(hit.gameObject);
            health += 15;
            Debug.Log(health);
        }

        if (hit.gameObject.CompareTag("cactus"))
        {
           // Destroy(hit.gameObject);
            health -= 1;
        }
    }*/
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            var obstacle = hit.gameObject.GetComponent<Obstacle>();
            if (obstacle)
            {
                healthSystem.DecreaseHealth(obstacle.health);
                Debug.Log("Hit");
            }
        }

        if (hit.gameObject.CompareTag("Food"))
        {
            var food = hit.gameObject.GetComponent<Food>();
            if (food)
            {
                Destroy(hit.gameObject);
                healthSystem.IncreaseHealth(food.health);
                hungerSystem.DecreaseHungerLevel(food.hunger);
            }
        }
    }


}

