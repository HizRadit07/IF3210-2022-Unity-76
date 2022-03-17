using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        // mendapat nilai mask dari layer bernama floor
        floorMask = LayerMask.GetMask("Floor");

        //get component animator
        anim = GetComponent<Animator>();

        //get rigid body
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //get horizontal input (-1,0,1)
        float h = Input.GetAxisRaw("Horizontal");
        //get nilai input vertical (-1,0,1)
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        //set nilai x dan y
        movement.Set(h, 0f, v);

        //normalize nilai vector agar total panjang adalah 1
        movement = movement.normalized * speed * Time.deltaTime;

        //move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }
    void Turning()
    {
        //buat ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //raycast untuk floor hit
        RaycastHit floorHit;
        //do raycast
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //get vector of posisi player dan posisi floorhit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //get new look rotation
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }


    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
