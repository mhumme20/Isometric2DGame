using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
 public Rigidbody2D playerRigid;
 public float speed;
 private InputSystem_Actions PlayerInput;
 private Vector2 movementInput;
 public GameObject Playersprite;
 
 private void Move(InputAction.CallbackContext context)
 {
  //used the Quaternion.AngleAxis function to rotate the vector by 45 degrees
  // this meant i need to first convert the vector2 input into vector3
  //do the rotation and then convert back into vector 2
  
  Vector2 input = context.ReadValue<Vector2>();
  Vector3 input3D = new Vector3(input.x, input.y, 0f);
  Quaternion rotation = Quaternion.AngleAxis(-45f, Vector3.forward);
  Vector3 rotated3D = rotation * input3D;
  movementInput = new Vector2(rotated3D.x, rotated3D.y);
 }

 private void Stop(InputAction.CallbackContext context)
 {
  movementInput = Vector2.zero;

 }

 private void Awake()
 {
  PlayerInput = new InputSystem_Actions();
 }

 private void OnEnable()
 {
  PlayerInput.Enable();
  PlayerInput.Player.Move.performed += Move;
  PlayerInput.Player.Move.canceled += Stop;
 }

 private void OnDisable()
 {
  PlayerInput.Player.Move.performed -= Move;
  PlayerInput.Player.Move.canceled -= Stop;
  PlayerInput.Disable();
 }

 private void Movement()
 {
  playerRigid.linearVelocity = movementInput * speed;
 }

 private void RotateSprite()
 {
  if (movementInput != Vector2.zero)
  {
   //used Mathf.Atan2 to get calculate the angle of movement, but the result was off by 90 degrees
   //thats why its reduces the result by 90 after converting it back into degrees from radians with Mathf.Rad2Deg
   float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg-90;
   Playersprite.transform.rotation = Quaternion.Euler(0f, 0f, angle);
  }
 }
 private void FixedUpdate()
 {
  Movement();
  RotateSprite();
 }
}
