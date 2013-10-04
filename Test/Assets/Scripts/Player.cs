using UnityEngine;
using System.Collections;

public class Player : Actor {

    public override void Init()
    {
        base.Init();
        //StartCoroutine("GetInputs");
    }

    void Update()
    {
        Vector3 t = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical")
        );
        
        /** Sprint Capability (Currently breaks Dashing)
        if (Input.GetAxis("Left Trigger") > 0 && !MovementScript.Dashing)
            MovementScript.Speed = MovementScript.BaseSpeed * SprintMultiplier;
        else
            MovementScript.Speed = MovementScript.BaseSpeed;
        **/

        if (Input.GetButtonDown("Dash") && !MovementScript.Dashing)
            MovementScript.StartCoroutine("Dash");

        MovementScript.IsStraifing = Input.GetAxis("Left Trigger") > 0;

        if (t == Vector3.zero)
        {
            animation.Stop("run");
            animation.Play("idle");
        }
        else if (MovementScript.Dashing)
        {
            animation.Stop();
            animation.Stop("run");
        }
        else
        {
            animation.Stop("idle");
            animation.Play("run");
        }

        MovementScript.Target = Vector3.Normalize(t);
    }
}
