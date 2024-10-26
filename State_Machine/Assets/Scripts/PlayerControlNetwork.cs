using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerControlNetwork : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    public float rotateSpeed;
    public Camera myCam;

    public GameObject ammo;
    public GameObject ammoSpawn;

    public bool grounded;

    bool showCurser = false;

    public float health;
    public string playerName;

    public TMP_Text healthField;
    public TMP_Text nameField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if the current game window is controlled by me, turn on the camera
        if(photonView.IsMine)
        {
            myCam.enabled = true;
            Cursor.visible = showCurser;
            Cursor.lockState = showCurser ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //healthField.text = health.ToString();
        if (photonView.IsMine)
        {
            float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float yMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(xMovement, 0, yMovement);

            float mouseInput = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            Vector3 lookHere = new Vector3(0, mouseInput, 0);
            transform.Rotate(lookHere);

            if (Input.GetButtonDown("Fire1"))
            {
                photonView.RPC("Shoot", RpcTarget.All);
            }

            if(Input.GetButtonDown("Jump") && grounded)
            {
                GetComponent<Rigidbody>().linearVelocity = Vector3.up * 5;
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                showCurser = !showCurser;
                Cursor.visible = showCurser;
                Cursor.lockState = showCurser ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }
    }

    [PunRPC]
    public void Shoot()
    {
        GameObject ammoInstance = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
        ammoInstance.GetComponent<Rigidbody>().AddForce(ammoSpawn.transform.forward * 30, ForceMode.Impulse);
        Destroy(ammoInstance, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (photonView.IsMine)
            {
                photonView.RPC("LoseHealth", RpcTarget.AllBuffered);
            }
            Destroy(collision.gameObject);
        }
    }

    public void LoseHealth()
    {
        health -= 10;
        Debug.Log("Im losing health");
        healthField.text = health.ToString();
        if (health < 0 && photonView.IsMine)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
