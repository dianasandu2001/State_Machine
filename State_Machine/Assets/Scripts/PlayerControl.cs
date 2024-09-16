using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float yMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(xMovement, 0, yMovement);

        float mouseInput = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        Vector3 lookHere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookHere);

        //Same thing
        //transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        //transform.Rotate(lookHere);
    }
}
