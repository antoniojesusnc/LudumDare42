using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiController : MonoBehaviour {

    [SerializeField]
	PegiInput _input;

    [SerializeField]
    OrbitMovement _orbitMovement;

    [SerializeField]
    float _speed;

    [SerializeField]
    float _orbitCoolDown;

    int _currentOrbit = 1;
    float _orbitTimeStamp;
    float _currentSpeed;

    PlanetController _planet;

	void Start () {
        _currentSpeed = _speed;
        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
        _orbitMovement.Speed = _speed;
        _orbitMovement.CurrentOrbit = _currentOrbit;
    }
	
	void Update () {
        SetOrbitPosition();
		checkShoot();
    }

	private void checkShoot()
	{
		if (_input.Shooting) 
		{
			//Debug.Log ("HI");
			//Debug.DrawRay(transform.position, (_planet.transform.position - transform.position).normalized, Color.green);
			Debug.DrawLine(transform.position,(_planet.transform.position - transform.position).normalized, Color.green);
			RaycastHit HitInfo = new RaycastHit();
			if (Physics2D.Raycast((Vector2)transform.position, (Vector2)(_planet.transform.position-transform.position).normalized,)) 
			{
				HitInfo.collider.GetComponent<SpriteRenderer> ().color = Color.blue;
			}
		}
	}

    private void SetOrbitPosition()
    {
        _orbitTimeStamp -= Time.deltaTime;
        if (_input.Momentum.y != 0 && _orbitTimeStamp <= 0)
        {
            int newOrbit = _currentOrbit + (int)_input.Momentum.y;
            newOrbit = Mathf.Clamp(newOrbit, 1, 3);
            if(newOrbit != _currentOrbit)
            {
                _currentOrbit = newOrbit;
                _orbitTimeStamp = _orbitCoolDown;
            }
        }
    }
}
