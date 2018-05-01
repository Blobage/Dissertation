using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public Transform m_Camera;
    public Vector3 mainCamForward;
    private Vector3 m_Move;

    [SerializeField]
    float m_GroundCheckDistance = 0.1f;
    [SerializeField]
    float speed; 
    

    float m_TurnAmount,
        m_ForwardAmount;
    Vector3 m_GroundNormal;
    private bool m_Jump,
        m_IsGrounded;



    // Use this for initialization
    void Start () {
		if(Camera.main != null)
        {
            m_Camera = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward *speed;
            Debug.DrawLine(transform.position, transform.forward,Color.blue);
        }
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.forward * speed * 2f;
            Debug.DrawLine(transform.position, transform.forward, Color.red);
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= transform.right * speed * 2f;
            Debug.DrawLine(transform.position, transform.forward, Color.red);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed;
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.right * speed * 2f;
            Debug.DrawLine(transform.position, transform.forward, Color.red);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= transform.forward * speed * 2f;
            Debug.DrawLine(transform.position, transform.forward, Color.red);
        }

    }
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if (m_Camera != null)
        {
            // calculate camera relative direction to move:
            mainCamForward = Vector3.Scale(m_Camera.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * mainCamForward + h * m_Camera.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        //m_Character.Move(m_Move, crouch, m_Jump);
        Movement(m_Move);

        m_Jump = false;
    }

    void Movement(Vector3 camMove)
    {
        if (camMove.magnitude > 1f) camMove.Normalize();
        camMove = transform.InverseTransformDirection(camMove);
        CheckGroundStatus();
        camMove = Vector3.ProjectOnPlane(camMove, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(camMove.x, camMove.z);
        m_ForwardAmount = camMove.z;
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            //m_Animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            //m_Animator.applyRootMotion = false;
        }
    }
}
