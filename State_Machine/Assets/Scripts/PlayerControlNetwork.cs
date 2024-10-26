using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControlNetwork : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    public float rotateSpeed;
    public Camera myCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if the current game window is controlled by me, turn on the camera
        if(photonView.IsMine)
        {
            Debug.Log("Im stupid");
            myCam.enabled = true;
            Debug.Log("Im stupid 2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float yMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(xMovement, 0, yMovement);

            float mouseInput = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            Vector3 lookHere = new Vector3(0, mouseInput, 0);
            transform.Rotate(lookHere);
        }
    }
}
