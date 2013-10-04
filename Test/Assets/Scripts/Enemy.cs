using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Actor
{

    #region Fields

    public readonly float ATTACKDISTANCE = 25f;

    [SerializeField]
    private Transform _player;


    [SerializeField]
    private List<Transform> _targets;

    [SerializeField]
    private Transform _currentTarget;
    
    #endregion

    #region Properties

    public Transform Player
    {
        get { return _player; }
        private set { _player = value; }
    }

    public List<Transform> Target
    {
        get { return _targets; }
        set { _targets = value; }
    }

    public Transform CurrentTarget
    {
        get { return _currentTarget; }
        set { _currentTarget = value; }
    }

    #endregion

    public override void Init()
    {
        base.Init();
        Player = GameObject.FindWithTag("Player").transform;

        // Initialize CurrentTarget
        CurrentTarget = CC.transform;

        // Point to first target
        if (Target.Count == 0)
            CurrentTarget.position = Vector3.zero;
        else
            CurrentTarget = Target[0];
    }

    void Update()
    {
        // Target doesn't move
        var t = Vector3.zero;

        // Set target and pursuit AI
        if (Vector3.Distance(Player.position, CC.transform.position) > ATTACKDISTANCE)
            t += CurrentTarget.position - CC.transform.position;
        else
            t += Player.position - CC.transform.position;

        // Determine if the AI should dash attack
        if (Vector3.Distance(Player.position, CC.transform.position) < 10f
            && !MovementScript.Dashing)
        {
            StartCoroutine("DashCommand");
        }

        if (t == Vector3.zero)
        {
            animation.Stop("RunCycle");
            animation.Play("Idle_1");
        }
        else if (MovementScript.Dashing)
        {
            animation.Stop();
            animation.Stop("RunCycle");
        }
        else
        {
            animation.Stop("Idle_1");
            animation.Play("RunCycle");
        }

        MovementScript.Target = Vector3.Normalize(t);
    }

    IEnumerator DashCommand()
    {
        yield return MovementScript.StartCoroutine("Dash");
        yield return MovementScript.StartCoroutine("Wait", 1);
    }
}
