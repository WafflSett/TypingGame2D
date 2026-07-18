using UnityEngine;

public class LaserSpawnerScript : MonoBehaviour
{
    public GameObject projectileObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnLaser(GameObject target) {
        GameObject newWord = Instantiate(projectileObject, new Vector3(0, -4.3f, 0), transform.rotation);
        LaserScript ps = newWord.GetComponent<LaserScript>();
        ps.target = target;
    }
}
