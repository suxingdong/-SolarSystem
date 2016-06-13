using UnityEngine;
using System.Collections;

public class IronSprite : Role {

    public const string ironProfab = "prefab/sprite/fighter004";

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
    public IronSprite()
    {
        gameObject = ResManager.Instance.createSingle(ironProfab);
        transform = gameObject.transform;
        _angle_speed = 1.4f;
        _radius_length = 0.9f;
        type = Role_Type.IRON;
        //rigidbody = gameObject.AddComponent<Rigidbody>();
        //rigidbody.useGravity = false;
        //rigidbody.velocity = new Vector3(0, 1, 0);
        //mass = rigidbody.mass;
        
        isRun = false;
    }

    public override void Init()
    {
        transform.localPosition = new Vector3(_radius_length, 0, 0);
        transform.Rotate(new Vector3(0,-90,0));
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
                RotateAround(_radius_length);
                //Revolution();
            }

        }
    }

    //公转
    public void Revolution()
    {

        temp_angle += _angle_speed * Time.fixedDeltaTime; // 

        _pos_new.x = Mathf.Cos(temp_angle) * _radius_length;
        _pos_new.y = transform.localPosition.y;// curTurnPlanet.transform.localPosition.y + Mathf.Sin(temp_angle) * _radius_length;
        _pos_new.z = Mathf.Sin(temp_angle) * _radius_length; //transform.localPosition.z;
        transform.rotation = Quaternion.Euler(0, temp_angle, 0);
        transform.localPosition = _pos_new;
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


    public override void Accack(Role target)
    {
        base.Accack(target);

    }
}
