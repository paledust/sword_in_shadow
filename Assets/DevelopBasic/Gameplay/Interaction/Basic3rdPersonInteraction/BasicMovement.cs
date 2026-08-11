using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BasicMovement : MonoBehaviour
{
[Header("Player movement")]
    [SerializeField] private float walkSpeed   = 1;
    [SerializeField] private float sprintSpeed = 2;
    [SerializeField] private float turnSmooth  = 1;
    [Range(0,1)] public float SpeedScale  = 1;
    
    private bool canMove = true;
    private bool canRotate = true;
    
    public Vector3 velocity{get{return m_rigid.linearVelocity;}}
    private Rigidbody m_rigid;
    
    void Awake()=>m_rigid = GetComponent<Rigidbody>();
    public void MovePlayer(Vector3 moveVector, bool sprinting){
        if(!canMove) return;
        moveVector.y = 0;
        if(canRotate){
            if(moveVector!=Vector3.zero){
                Quaternion targetRotation = Quaternion.LookRotation(moveVector);
                Quaternion newRotatation  = Quaternion.Slerp(m_rigid.rotation, targetRotation, turnSmooth*Time.fixedDeltaTime);
                m_rigid.MoveRotation(newRotatation);
            }
            moveVector = transform.forward * moveVector.magnitude * SpeedScale * (sprinting?sprintSpeed:walkSpeed);
            moveVector.y = m_rigid.linearVelocity.y;
        }
        else{
            moveVector = moveVector * SpeedScale * (sprinting?sprintSpeed:walkSpeed);
            moveVector.y = m_rigid.linearVelocity.y;
        }
        if(!m_rigid.isKinematic)
            m_rigid.linearVelocity = moveVector;
        else
            m_rigid.MovePosition(m_rigid.position + moveVector * Time.fixedDeltaTime);
    }
    public void SwitchCanMove(bool value)=>canMove = value;
    public void SwitchCanRotate(bool value)=>canRotate = value;
    public void ResetVelocity(){m_rigid.linearVelocity = Vector3.zero;}
    public void SwitchPhysics(bool isOn){m_rigid.isKinematic = !isOn;}
}