using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator DestroyThis()
    {

        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}
