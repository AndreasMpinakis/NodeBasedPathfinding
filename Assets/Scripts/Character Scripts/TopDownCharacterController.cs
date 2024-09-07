using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownCharacterController : MonoBehaviour/*, IDataPersistence*/
{
    private Rigidbody2D rb;
    private Animator _Animator;

    public float speed;

    private bool _LookingLeft;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //_Animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            _LookingLeft = true;
            //MoveLeftAnimation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;

            _LookingLeft = false;
            //MoveRightAnimation();
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;

            //if (_LookingLeft)
            //    MoveLeftAnimation();
            //else
            //    MoveRightAnimation();

        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;

            //if (_LookingLeft)
            //    MoveLeftAnimation();
            //else
            //    MoveRightAnimation();
        }

        //if (dir == Vector2.zero)
        //    IdleAnimation();

        dir.Normalize();
        rb.velocity = speed * dir;
    }

    //private void MoveLeftAnimation()
    //{
    //    _Animator.SetBool("MovingLeft", true);
    //    _Animator.SetBool("MovingRight", false);
    //}

    //private void MoveRightAnimation()
    //{
    //    _Animator.SetBool("MovingLeft", false);
    //    _Animator.SetBool("MovingRight", true);
    //}

    //private void IdleAnimation()
    //{
    //    if (_LookingLeft)
    //        _Animator.SetFloat("Looking", 1);
    //    else
    //        _Animator.SetFloat("Looking", 0);

    //    _Animator.SetBool("MovingLeft", false);
    //    _Animator.SetBool("MovingRight", false);
    //}

    //public void SaveData(GameData data)
    //{
    //    data.playerPosition = this.transform.position;
    //}

    //public void LoadData(GameData data)
    //{
    //    this.transform.position = data.playerPosition;
    //}
}
