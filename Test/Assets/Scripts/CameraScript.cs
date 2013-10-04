using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private float _distance;
    [SerializeField]
    private Vector3 _direction;

    public float Distance
    {
        get { return _distance; }
        set { _distance = value; }
    }

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
	// Use this for initialization
	void Start ()
    {
        StartCoroutine("FollowPlayer", GameObject.FindWithTag("Player").transform);
	}

    IEnumerator FollowPlayer(Transform player)
    {
        while (true)
        {
            transform.position = player.position + Direction * Distance;
            yield return null;
        }
    }
}
