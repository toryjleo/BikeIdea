using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftComponent : BikeMovementComponent
{

    public int DashForce;


    public override void ApplyForces()
    {
        //Movement Forward and Back and applies velocity 
        appliedForce += ForwardVector().normalized * Acceleration * Input.GetAxis("Vertical") * Time.fixedDeltaTime;

        

        //Steering Takes Horizontal Input and rotates both 
        float steerInupt = Input.GetAxis("Horizontal");
        bikeMeshChild.transform.localRotation = Quaternion.Euler(maxLean * steerInupt, 0, 0);
        bikeMeshParent.transform.Rotate(Vector3.up * steerInupt * (appliedForce.magnitude + 100) * Time.fixedDeltaTime);

        //Drag and MaxSpeed Limit to prevent infinit velocity  
        appliedForce *= dragCoefficient;
        //appliedForce = Vector3.ClampMagnitude(appliedForce, MaxSpeed);

        // Debug lines
        Debug.DrawRay(rb.transform.position, ForwardVector().normalized * 30, Color.red);
        Debug.DrawRay(rb.transform.position, appliedForce.normalized * 30, Color.blue);

        appliedForce = Vector3.Lerp(appliedForce.normalized, ForwardVector().normalized, Traction * Time.fixedDeltaTime) * appliedForce.magnitude;


        

        //TODO Fix this so that it works. The shift ability needs to be good for strafing and also needs to have some Kind of COOLDOWN 
        if (Input.GetKeyDown(KeyCode.LeftShift)){

            print("Vertical: " + Input.GetAxis("Vertical"));
            print("Horizontal: " + Input.GetAxis("Horizontal"));
            Vector3 ShiftDirection = new Vector3(1, 0, 1);

            //Vector3 ShiftDirection = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            //ShiftDirection = ShiftDirection.Normalize;
            rb.AddForce(ShiftDirection*DashForce);
        }

        rb.AddForce(appliedForce);


    }

}
