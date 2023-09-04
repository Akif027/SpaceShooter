using System.Collections;

using UnityEngine;


public class Bullet : MonoBehaviour
{

    public float disableDelay = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DisableAfterDelay());
    }

    private IEnumerator DisableAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(disableDelay);


        gameObject.SetActive(false);
    }



    /*    private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.tag == "ObjectdisableManger")
            {
                gameObject.SetActive(false);
                Debug.Log("disabled");

            }
        }*/
}