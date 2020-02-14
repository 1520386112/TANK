using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCreator : MonoBehaviour
{
    public GameObject[] props;

    private int maxNum;

    public float minTimeBeforeNextCreate = 5f;
    public float maxTimeBeforeNextCreate = 30f;
    private float realTimeBeforeNextCreate;

    private float lasCreateTime = 0f;
    void Start()
    {
        maxNum = props.Length;
        realTimeBeforeNextCreate = Random.Range(minTimeBeforeNextCreate, maxTimeBeforeNextCreate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lasCreateTime >= realTimeBeforeNextCreate)
        {
            CreateProp();
        }
    }
    private void CreateProp()
    {
        int propNum = Random.Range(0, maxNum);

        Vector2 CreatePosition = new Vector2();
        CreatePosition.x = Random.Range(-5.5f, 5.5f);
        CreatePosition.y = Random.Range(-4.5f, 4.5f);

        Instantiate(props[propNum],CreatePosition , transform.rotation);

        lasCreateTime = Time.time;
        realTimeBeforeNextCreate = Random.Range(minTimeBeforeNextCreate, maxTimeBeforeNextCreate);
    }
}