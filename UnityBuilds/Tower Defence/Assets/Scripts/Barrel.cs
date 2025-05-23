using UnityEngine;

public class Barrel : MonoBehaviour
{
    //if we needed a data member to be public but not visible in the Unity editor, we could add the
    //[HideInInspector] attribute
    protected PrefabPool prefabPool;
    public int rotationSpeed;
    public float averageNumShotsPerSecond;
    public float projectileForce;

    private Transform target;

    private void Awake()
    {
        prefabPool = GameObject.Find("PrefabPool").GetComponent<PrefabPool>();
    }
    private void Update()
    {
        target = prefabPool.FindEnemyShip().transform;
        RotateGradually2D();
        Shoot();
    }

    #region Rotate Gradually
    //cnobert: adapted this code from 
    //https://answers.unity.com/questions/624856/rotate-2d-turret-toward-target-heading-lerpangle.html
    private float currentAngle = 0;
    public float initialForwardAngle; // 90
    public float threshold; //4
    public float maxRotationSpeed; //60
    private void RotateGradually2D()
    {
        float angleToTarget; // Destination angle
        float signToTarget;
        angleToTarget = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        signToTarget = Mathf.Sign(angleToTarget - currentAngle);
        if (Mathf.Abs(angleToTarget - currentAngle) > threshold)
        {
            currentAngle += signToTarget * maxRotationSpeed * Time.deltaTime;
        }
        else
        {
            currentAngle = angleToTarget;
        }
        transform.parent.transform.eulerAngles = new Vector3(0, 0, currentAngle - initialForwardAngle);
    }
    #endregion
    protected void Shoot()
    {
        //where's Time.Delta time? #kaelquestion
        int highEndOfRange = (int) (averageNumShotsPerSecond / Time.deltaTime);
        if(Random.Range(1, highEndOfRange) == 1)
        {
            Transform projectile = prefabPool.Projectile;
            if (projectile != null)
            {
                projectile.position = transform.GetChild(0).transform.position;
                Vector2 projectileDirection = transform.up;
                projectile.GetComponent<Rigidbody2D>().AddForce(projectileDirection * projectileForce);
                projectile.GetComponent<Rigidbody2D>().AddTorque(500);
            }
        }
        
    }
}
