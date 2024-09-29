using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject targetLocation; //TargetArea cylinder
    public GameObject ammoSpawn; //we spaen ammo this location
    public GameObject ammo; //ammo prefab
    public GameObject gunRotator;
    public float force; // force will be the same but the angle is asjusted
    public Vector3 gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
        Vector3[] direction = HitTargetBySpeed(ammoSpawn.transform.position, targetLocation.transform.position, gravity, force);

        //instantiate ball and call it's rigidvody and shoot to direction[0] and direction[1]

        //first ammo
        GameObject projectile = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
        //give ammo force
        projectile.GetComponent<Rigidbody>().AddRelativeForce(direction[0], ForceMode.Impulse);

        //second ammo
        GameObject projectile2 = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
        //give ammo force
        projectile.GetComponent<Rigidbody>().AddRelativeForce(direction[1], ForceMode.Impulse);
    }

    //this method will return array of vector3 of both shooting directions/ angles (wheather it shoots straight of high up)
    public Vector3[] HitTargetBySpeed(Vector3 startPosition, Vector3 targetPosition, Vector3 gravityBase, float launchSpeed)
    {
        //we calculate the direction we shoot
        Vector3 AtoB = targetPosition - startPosition;
        Vector3 horizontal = GetHorizontalVector(AtoB, gravityBase, startPosition);
        Vector3 vertical = GetVerticalVector(AtoB, gravityBase, startPosition);

        float horizantalDistance = horizontal.magnitude;
        //this gives us negative vertival distance if player if below and positive if above
        float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravityBase));

        float x2 = horizantalDistance * horizantalDistance;
        float v2 = launchSpeed * launchSpeed;
        float v4 = launchSpeed * launchSpeed * launchSpeed * launchSpeed;
        float gravMag = gravity.magnitude;

        //launchtest
        //if launchtest is negative, there is no way we can hit the target with current launch force even if we shoot 45 degrees
        // if launch is positive, we can hit the target and we can calculate the angles/ directions
        float launchtest = v4 - (gravMag * ((gravMag * x2) + (2 * verticalDistance)));
        Debug.Log("launchtest" + launchtest);

        Vector3[] launch = new Vector3[2];

        if(launchtest < 0)
        {
            Debug.Log("We connot hit the target. lets shoot 2 balls to 45 degrees, because we want to");
            launch[0] = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - gravityBase.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad);
            launch[1] = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - gravityBase.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad);
        }    
        else
        {
            Debug.Log("We can hit hte target, lets calculate the angle");
            float[] tanAngle = new float[2];
            tanAngle[0] = (v2 - Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizantalDistance);
            tanAngle[1] = (v2 + Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizantalDistance);

            float[] finalAngle = new float[2];

            finalAngle[0] = Mathf.Atan(finalAngle[0]);
            finalAngle[1] = Mathf.Atan(finalAngle[1]);

            Debug.Log("Then angles we need to shoot are" + finalAngle[0]*Mathf.Rad2Deg + "and" + finalAngle[1] * Mathf.Rad2Deg);

            launch[0] = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle[0])) - gravityBase.normalized * launchSpeed * Mathf.Sin(finalAngle[0]);
            launch[1] = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle[1])) - gravityBase.normalized * launchSpeed * Mathf.Sin(finalAngle[1]);
        }
        return launch;
    }

    public Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase, Vector3 startPosition)
    {
        Vector3 output;
        Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
        perpendicular = Vector3.Cross(gravityBase, perpendicular); // Vector point horizontal direction
        output = Vector3.Project(AtoB, perpendicular); // This is the horizontal vector
        Debug.DrawRay(startPosition, output, Color.green, 10f);

        //Debug.Log(AtoB);
        //Vector3 hor = AtoB;
        //hor.y = 0;
        //Debug.Log(hor);SS
        //Debug.Log(output);

        return output;
    }

    public Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase, Vector3 startPosition)
    {
        Vector3 output;
        output = Vector3.Project(AtoB, gravityBase);
        Debug.DrawRay(startPosition, output, Color.red, 10f);
        return output;
    }
}
