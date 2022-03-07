using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InitExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        int r = Random.Range(1, 5);
        animator.SetBool("select0" + r.ToString(), true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
