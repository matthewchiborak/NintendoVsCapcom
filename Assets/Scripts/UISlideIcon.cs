using UnityEngine;
using System.Collections;

public class UISlideIcon : MonoBehaviour
{

    public Vector3 startPosition;
    public Vector3 endPosition;
    //public float framesForSlide;
    private float framesForSlide;
    private int count;
    private float yInc;
    private float xInc;

    // Use this for initialization
    void Start()
    {
        framesForSlide = Random.Range(30, 45);

        count = 0;
        // transform.localPosition = new Vector3(startPosition.x + GetComponentInParent<RectTransform>().transform.position.x, startPosition.y + GetComponentInParent<RectTransform>().transform.position.y, startPosition.z);
        transform.localPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z);

        if (endPosition.x < 0)
        {
            yInc = (endPosition.y - startPosition.y + 85) / framesForSlide;
            xInc = (endPosition.x - startPosition.x + 570) / framesForSlide;
        }
        else
        {
            yInc = (endPosition.y - startPosition.y + 85) / framesForSlide;
            xInc = (endPosition.x - startPosition.x - 570) / framesForSlide;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (count < framesForSlide)
        {
            count++;
            transform.localPosition = new Vector3(transform.localPosition.x + xInc, transform.localPosition.y + yInc, transform.localPosition.z);
            
        }
    }
}
