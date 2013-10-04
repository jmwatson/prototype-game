using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour
{

    #region Fields
    [SerializeField]
    private float _dashTimer = 0.3f;

	[SerializeField]
    private Vector3 _target;

    private float _speed;

    [SerializeField]
    private float _baseSpeed;
    
    [SerializeField]
    private CharacterController _cc;
    
    [SerializeField]
    private bool _dashing;

    [SerializeField]
    private float _gravity;

    [SerializeField]
    private int _dashMultiplier = 3;

    [SerializeField]
    private bool _isStraifing = false;
    #endregion

    #region Properties
    public Vector3 Target
    {
        get{ return _target; }set
        {
            _target = value;

            if (Dashing)
            {
            }
            else
            {
                StopCoroutine("Move");
                StartCoroutine("Move", _target);
            }
        }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public float BaseSpeed
    {
        get { return _baseSpeed; }
        set { _baseSpeed = value; }
    }

    public CharacterController CC
    {
        get { return _cc; }
        set { _cc = value; }
    }

    public bool Dashing
    {
        get { return _dashing; }
        set { _dashing = value; }
    }

    public float Gravity
    {
        get { return _gravity; }
        set { _gravity = value; }
    }

    public int DashMultiplier
    {
        get { return _dashMultiplier; }
        set { _dashMultiplier = value; }
    }

    public float DashTimer
    {
        get { return _dashTimer; }
        set { _dashTimer = value; }
    }

    public bool IsStraifing
    {
        get { return _isStraifing; }
        set { _isStraifing = value; }
    }
    #endregion

    #region Methods
    void Start()
    {
        _cc = this.GetComponent<CharacterController>();

        Speed = BaseSpeed;
	}

    IEnumerator Move(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
			// An issue with dash caused the model to look down at the ground.
			// It was an issue with the target not getting reset after gravity
			// was applied.
			if (target.y != 0) target.y = 0;
			
			// Set the rotation so the model looks in the direction of movement.
            if (target != Vector3.zero && !IsStraifing)
            {
                var lookDir = Quaternion.LookRotation(target, Vector3.up);
                CC.transform.rotation = lookDir;
            }
			
			// Apply gravity and move the character controller.
            target = target + new Vector3(0, Gravity, 0);
            CC.Move(target * Speed * Time.deltaTime);
			
            yield return null;
        }
    }

    IEnumerator Dash()
    {
        Debug.Log("Dash started");
        Dashing = true;
        if (Target == Vector3.zero) Target = CC.transform.forward;
        Speed = BaseSpeed * DashMultiplier;
        yield return StartCoroutine(Movement.Wait(DashTimer));
        Speed = BaseSpeed;
        Dashing = false;
        Debug.Log("Dash finished");
    }

    static IEnumerator Wait(float duration)
    {
        Debug.Log("Wait started");
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("Wait finished");
    }
    #endregion
}
