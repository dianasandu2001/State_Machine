using UnityEngine;

public class GunRotator : MonoBehaviour
{
    public float currentAngle;
    public float startAngle;
    public bool rotating;
    public float rotateDuration;
    //Serialized make private variables visable in the inspector
    // public variables can be accessed from ourside of its class
    // private variables is avaliable only inside the class and vasically cannot change the value from outside.
    // you should make only those variables public that needs to be pubic, all other variables should be private
    // this is proper way to program and is more safe.
    [SerializeField]
    public float counter;

    // void in front of the function means that the function does not return anything.
    // if you make public void FunctionName() it means the function is public and it can be invoked from other class.
    // if the function does not have public word in front it is the private function.
    // if you replace void with string float, bool or some other vatiable tyoe, then the function MUST return that kind of variable.

    private float _xAngle;
    public float xAngle 
    {  
        get 
        { 
            return _xAngle; 
        } 
        set 
        {
            // some magic
            // we store the angle to startAngle. _xangle will be our goal angle.
            startAngle = transform.localRotation.eulerAngles.x;
            _xAngle = value;
            rotating = true;
            counter = 0; 
        } 
    } 

    void Update()
    {
        counter += Time.deltaTime;
        if(counter > rotateDuration && rotating == true)
        {
            //this will be run if rotation is done.
            rotating = false;
        }
        //rotating code, we "animate" currentAngle value
        currentAngle = Mathf.LerpAngle(startAngle, xAngle, counter / rotateDuration);
        transform.localEulerAngles = new Vector3(currentAngle, 0, 0);
    }
}
