using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]


public class Stick : MonoBehaviour
{

    public string StuckObjectTag = "Obstacle";
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(StuckObjectTag))
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
    
}
