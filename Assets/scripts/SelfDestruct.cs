using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private float timer;

    public float TimeToDestroy;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
