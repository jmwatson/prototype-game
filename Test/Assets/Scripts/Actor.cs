using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Movement))]
[RequireComponent (typeof(CharacterController))]
public class Actor : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Movement _movementScript;

    [SerializeField]
    private bool _sprint;

    [SerializeField]
    private int _sprintMultiplier;

    [SerializeField]
    private CharacterController _cc;
    #endregion

    #region Properties
    public Movement MovementScript
    {
        get { return _movementScript; }
        set { _movementScript = value; }
    }

    public bool Sprint
    {
        get { return _sprint; }
        set { _sprint = value; }
    }

    public int SprintMultiplier
    {
        get { return _sprintMultiplier; }
        set { _sprintMultiplier = value; }
    }

    public CharacterController CC
    {
        get { return _cc; }
        set {_cc = value; }
    }
    #endregion

    #region Methods
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        MovementScript = GetComponent<Movement>();
        CC = GetComponent<CharacterController>();
    }
    #endregion
}
