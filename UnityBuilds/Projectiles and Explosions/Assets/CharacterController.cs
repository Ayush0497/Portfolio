using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	public float _speed;
	private float _move;
	public float _jump;

	public bool _isPlayerJumping;

	Rigidbody2D _playerCharacter;

	void Start()
	{
		_playerCharacter = GetComponent<Rigidbody2D>();
	}

	//Moving Left & And Right + Jumping + Shooting
	void Update()
	{
		_move = Input.GetAxis("Horizontal");

		_playerCharacter.velocity = new Vector2(_speed * _move, _playerCharacter.velocity.y);
		if (Input.GetButtonDown("Jump") && _isPlayerJumping == false)
		{
			_playerCharacter.AddForce(new Vector2(_playerCharacter.velocity.x, _jump));
			_isPlayerJumping = true;
		}
	}

	//Jumping & Hit Detection
	private void OnCollisionEnter2D(Collision2D other)
	{
		//Jump Code
		if (other.gameObject.CompareTag("Ground"))
		{
			_isPlayerJumping = false;
		}
	}

}
