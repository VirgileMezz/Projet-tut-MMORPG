using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // changer angle du model avec right clic

    public Transform cible; //le ciblage de la camera
    [SerializeField] private float hauteur = 1.5f ;
    //[Range(10f, 20f)] [SerializeField] private float distance = 15f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float distanceMax = 20f;
    [SerializeField] private float distanceMin = 10f;
    [Range(0.1f, 10f)] [SerializeField] private float vitesse = 5f;
    private float zoomSpeed = 50f;
    private float rotationRecalibrage = 5.0f;
    private float zoomRecalibrage = 5.0f;
    private bool allowMouseX = true;
    private bool allowMouseY = true;
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float distanceActuelle;
    private float distanceVoulue;
    private float distanceCorrige;
    private bool peutTournerCamera = false;
    LayerMask collisionLayers = -1;
    private float collisionOffset = 0.1f;

    //public GameObject modelJoueur;
    private Quaternion playerRotation;
    [SerializeField] private GameObject player;
    private Transform playerPos;


    public void Start()
    {
        Vector3 angle = transform.eulerAngles;
        xDeg = angle.x;
        yDeg = angle.y;
        distanceActuelle = distance;
        distanceCorrige = distance;
        distanceVoulue = distance;

    }

    
    public void LateUpdate()
    {
        if (!cible)
            return;

        playerPos = player.transform;
        playerRotation = Quaternion.LookRotation(playerPos.position - transform.position);

        if (GUIUtility.hotControl == 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {

            }
            else
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
                {
                    if (allowMouseX)
                        xDeg += Input.GetAxis("Mouse X") * vitesse * 5f;
                    if (allowMouseY)
                        yDeg -= Input.GetAxis("Mouse Y") * vitesse * 5f;
                    if (Input.GetMouseButton(1))
                    {
                        
                        playerPos.rotation = Quaternion.Euler(0, transform.eulerAngles.y,0);
                    }
                }
            }
        }
        reinitialiseGrandAngle(yDeg);

        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);

        distanceVoulue -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed * Mathf.Abs(distanceVoulue);
        distanceVoulue = Mathf.Clamp(distanceVoulue, distanceMin, distanceMax);
        distanceCorrige = distanceVoulue;

        Vector3 vTargetOffset = new Vector3(0, -hauteur, 0);
        Vector3 position = cible.position - (rotation * Vector3.forward * distanceVoulue + vTargetOffset);

        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(cible.position.x, cible.position.y + hauteur, cible.position.z);

        bool estCorrige = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers))
        {
            distanceCorrige = Vector3.Distance(trueTargetPosition, collisionHit.point) - collisionOffset;
            estCorrige = true;
        }

        distanceActuelle = !estCorrige || distanceCorrige > distanceActuelle ? Mathf.Lerp(distanceActuelle, distanceCorrige, Time.deltaTime * zoomRecalibrage) : distanceCorrige;

        distanceActuelle = Mathf.Clamp(distanceActuelle, distanceMin, distanceMax);

        position = cible.position - (rotation * Vector3.forward * distanceActuelle + vTargetOffset);

        transform.rotation = rotation;
        transform.position = position;

    }
    void reinitialiseGrandAngle(float angle)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        yDeg = Mathf.Clamp(angle, -60, 80);
    }

   
}

