using UnityEngine;
using System.Collections;

public class FireSprite : Role {
    
    public const string waterProfab = "prefab/sprite/fighter003";

    public Vector3 linerSpeed;
    private Vector3 circleDot;
    private float radius;
    //private Rigidbody planetBody;
    //private Rigidbody rigidbody;

    public float _radius_length;
    public float _angle_speed;
    private float temp_angle;
    private Vector3 _pos_new;
    //public Vector3 _center_pos;


    public bool _round_its_center;
    public FireSprite()
    {
        gameObject = ResManager.Instance.createSingle(waterProfab);
        transform = gameObject.transform;
        _angle_speed = 1.3f;
        _radius_length = 0.8f;
        type = Role_Type.FIRE;
        //rigidbody = gameObject.AddComponent<Rigidbody>();
        //rigidbody.useGravity = false;
        //rigidbody.velocity = new Vector3(0, 1, 0);
        //mass = rigidbody.mass;
        transform.localPosition = Vector3.zero;
        isRun = false;
    }


    public override void FixedUpdate()
    {
        if (isRun)
        {
            if (isChangePlant)
            {
                isRun = false;
                isChangePlant = false;
                Vector3 pos = virtualObj.transform.position;
                iTween.MoveTo(gameObject, pos, 1.5f);
                TimeManager.Instance.delayCall(() =>
                {
                    transform.parent = tarTurnPlanet.transform;
                    curTurnPlanet = tarTurnPlanet;
                    isRun = true;
                }, 1.5f);
            }
            else
            {
                Revolution();
            }

        }
    }

    //公转
    public void Revolution()
    {

        temp_angle += _angle_speed * Time.fixedDeltaTime; // 

        _pos_new.x = +Mathf.Cos(temp_angle) * _radius_length;
        _pos_new.y = transform.localPosition.y;// curTurnPlanet.transform.localPosition.y + Mathf.Sin(temp_angle) * _radius_length;
        _pos_new.z = Mathf.Sin(temp_angle) * _radius_length; //transform.localPosition.z;
        transform.localPosition = _pos_new;

    }

    public override void Init()
    {


    }
    //切换星球
    public override void ChangePlanet(planet p)
    {
        tarTurnPlanet = p;
        virtualObj = new GameObject();
        virtualObj.transform.parent = tarTurnPlanet.transform;
        virtualObj.transform.localPosition = transform.localPosition;
        isChangePlant = true;
    }



}
