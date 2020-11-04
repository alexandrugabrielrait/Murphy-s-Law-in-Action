using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float jumpHeight = 1f;
    public float gravityAcceleration = 9.81f;
    public bool canDoubleJump = false;
    public float pushForce = 2f;
    public float crouchCoeficient = 0.7f;
    public Transform model;
    public GameObject waterFilter;
    public DeathMenu deathUI;
    public bool isDead;

    bool secondJump;
    public bool inWater;

    public Vector3 velocity;
    public Vector3 bonusVelocity;
    public bool isCrouching, isControllable;

    void Start()
    {
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            isControllable = true;
            bonusVelocity = Vector3.zero;
        }

        float x = 0;
        float z = 0;
        if (isControllable)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravityAcceleration);
                secondJump = canDoubleJump;
            }
            else if (Input.GetButtonDown("Jump") && secondJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravityAcceleration);
                secondJump = false;
            }
        }

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            secondJump = false;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y -= gravityAcceleration * Time.deltaTime;

        controller.Move((velocity + bonusVelocity) * Time.deltaTime);

        if (isControllable)
            if (Input.GetButton("Crouch"))
                isCrouching = true;
            else
                isCrouching = false;

        CrouchFunction();

        waterFilter.GetComponent<MeshRenderer>().enabled = inWater;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        body.velocity = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z) * pushForce;
    }

    void CrouchFunction()
    {
        if (isCrouching == true)
        {
            model.localScale = new Vector3(1, crouchCoeficient, 1);
            controller.height = 2 * crouchCoeficient;
        }
        else
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.up, out hit, 1))
            {
                model.localScale = new Vector3(1, 1, 1);
                controller.height = 2f;
            }
        }
    }

    public void Respawn()
    {
        isControllable = true;
    }

    public void Die(int deathId)
    {
        if (!isDead)
        {
            isDead = true;
            ++SaveSystem.data.deaths;
            SaveSystem.SaveData();
            deathUI.Show(deathId);
        }
    }

    private IEnumerator ReloadLevel(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
