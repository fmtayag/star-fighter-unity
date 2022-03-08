using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollingSpeed = 0.5f;
    private float offset;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollingSpeed) / 20f;
        material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
