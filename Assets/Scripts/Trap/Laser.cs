using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] GameObject objectLaser;
    [SerializeField] Transform transformDir;
    [SerializeField] Transform transformStartLaser;
    [SerializeField] Transform effectEnd;
    [SerializeField] LighteningScript lightening;
    Vector3 Difference;

    // Use this for initialization
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.sortingOrder = 11;
      //  _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
       // _lineRenderer.material.color = Color.red;
        _lineRenderer.positionCount = 2;
    }
    void Update()
    {
        Difference = transformDir.position - objectLaser.transform.position;


        _lineRenderer.SetPosition(0, transformStartLaser.position);
        RaycastHit2D hit = Physics2D.Raycast(transformStartLaser.position, Difference, 100);
        if (hit.collider.tag == "GroundNotThrough" || hit.collider.tag == "Player" || hit.collider.tag == "Enemy" || hit.collider.tag == "TNT")
        {
            _lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, transform.position.z));
            effectEnd.transform.position = hit.point;
            lightening.target.transform.position = hit.point;
        }
        else
        {
            _lineRenderer.SetPosition(1, transformDir.position);
            effectEnd.transform.position = transformDir.position;
            lightening.target.transform.position = transformDir.position;
        }
        //else
        //{
        //    _lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, transform.position.z)*100);
        //}
        

    }
}
