using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player : MonoBehaviour
{
    //campos
    private Rigidbody physics;
    public float speed; //Velocidad de movimiento

    public bool canJump; //verifica si puede saltar
    public float jumpForce; //fuerza del salto
    private float noJumpCounter;

    // Start is called before the first frame update
    void Start()
    {
        physics = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (noJumpCounter < 0.5f) { noJumpCounter -= Time.deltaTime; }
        if (noJumpCounter <= 0) { canJump = false; noJumpCounter = 0.005f; }
    }

    //Update para las fisicas
    private void FixedUpdate()
    {
        //movimiento del personaje
        physics.AddForce(Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime, 0, 0);

        //salto
        if (!canJump) { physics.AddForce(0, -2000 * Time.fixedDeltaTime, 0); }

        if (Input.GetAxisRaw("Vertical") > 0 && canJump)
        {
            physics.AddForce(0, jumpForce * Time.fixedDeltaTime, 0);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { canJump = true; noJumpCounter = 0.6f; }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { canJump = true;  noJumpCounter = 0.005f; }
    }
}
