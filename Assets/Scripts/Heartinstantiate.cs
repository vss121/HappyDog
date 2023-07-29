
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heartinstantiate : MonoBehaviour
{
    public GameObject prefab;
    public GameObject HeartObj;
    public GameObject PosObj;

    GameObject instantiateB;
    GameObject instantiateC;
   
    public float movespeed = 2.0f;
    public Image HeartImg;
    
    Color HeartCo;
    
    Button HeartBtn;

    public Vector3 Pos;
    private void Start()
    {
        HeartCo = HeartImg.color;
        HeartBtn = HeartObj.GetComponent<Button>();
        Pos = PosObj.transform.position;
    }
    public void LikeBtnClick()
    {
        instantiateB = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y,-1), Quaternion.identity, GameObject.Find("BackGroundCanvus").transform);
        instantiateC = Instantiate(prefab, new Vector3(Pos.x, Pos.y, Pos.z), Quaternion.identity, GameObject.Find("BackGroundCanvus").transform);
        StartCoroutine(Instance(0.3f));
    }
    IEnumerator Instance(float coolTime)
    {
        HeartCo.a = 0.5f;
        HeartImg.color = HeartCo;
        HeartBtn.enabled = false;
        Vector3 newPos = instantiateB.transform.position;
        Vector3 newPos2 = instantiateC.transform.position;
        float filledTime = 0f;
        while (filledTime <= coolTime)
        {
            yield return new WaitForFixedUpdate();
            newPos.y += movespeed * Time.deltaTime;
            instantiateB.transform.position = newPos;
            newPos2.y += movespeed * Time.deltaTime;
            instantiateC.transform.position = newPos2;
            filledTime += Time.deltaTime;
            HeartImg.fillAmount = filledTime / coolTime;
        }
        HeartCo.a = 1.0f;
        HeartImg.color = HeartCo;
        HeartBtn.enabled = true;
        Destroy(instantiateB);
        Destroy(instantiateC);
    }
}