﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject camera;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private Transform cam;                 
    private Vector3 camForward;             
    private Vector3 move;

    private Vector3 playerFwd;

    void Start () {

        characterController = GetComponent<CharacterController>();
        cam = camera.transform;
    }

    // Update is called once per frame
    void Update () {
        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        playerFwd = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //transform.rotation = Quaternion.LookRotation(moveDirection);
        move = vertical * playerFwd + horizontal * cam.right;
        //move = vertical * camForward + horizontal * cam.right;
        characterController.SimpleMove(move * moveSpeed);


    }

   
}