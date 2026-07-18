using UnityEngine;

public class LaserScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 startPos;
    public GameObject target;
    public float speed;
    public Vector3 goalPos;
    private float angle;
    void Start()
    {
        startPos = new Vector3(0, -4.8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        followTarget();
    }

    private void followTarget() {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            goalPos = target.transform.position;
            transform.LookAt(goalPos, new Vector3(0, 1, 0));
            transform.Rotate(0, 90, 90);

            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, goalPos, step);
        }
    }
}
