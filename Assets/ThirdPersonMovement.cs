using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    
    public Transform cam;
    public float speed = 8f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Rigidbody rb;
    float horizontal;
    float vertical;

    float transformAngle;
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8f;
            animator.SetBool("shift",true);
        }
        else
        {
            speed = 6f;
            animator.SetBool("shift", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("attackbool", true);
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("attackbool", false);

        }




    }
    private void FixedUpdate()
    {
        Vector3 poz = new Vector3(horizontal, 0, vertical).normalized;
        animator.SetFloat("speed", poz.magnitude);
        if (poz.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(poz.x, poz.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;


            //rb.velocity = moveDir * speed ;
            //rb.AddForce(moveDir*10f);
            rb.velocity= Vector3.Lerp(rb.velocity, new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed),0.1f);








        }
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
