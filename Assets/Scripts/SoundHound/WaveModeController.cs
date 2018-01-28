using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;

public class WaveModeController : MonoBehaviour {

	public bool waveModeActive = false;

	public Transform waveform;
	public Transform targetWaveform;
	public float sensitivity = 1.0f;
	public float snap = 1.0f;

	float mouseAmplitude = 1;
	float mouseFrequency = 1;

	public Wave houndWave;
	public Wave targetWave;

	public LineRenderer HoundLine;
	public LineRenderer TargetLine;

	public float xFactor = 0.1f;
	public float yFactor = 2f;

	//public float bulletTime = 0.25f;
	//float normalTime = 1;

	public float bulletTimeFactor = 0.05f;
	float normalFixedDeltaTime;

	GameObject target;

	void Awake () {
		houndWave = new Wave (1f, 1f);
		targetWave = new Wave (0.5f, 0.5f);
		normalFixedDeltaTime = Time.fixedDeltaTime;
	}

	// Use this for initialization
	void Start () {
		Deactivate ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (CheckClickOnEnemy ()) {
				Activate ();
			}
		}
		if (waveModeActive) {
			UpdateWaves ();

			if (Input.GetButtonUp ("Fire1")) {
				if (isMatchingTarget ()) {
					DamageTarget ();
				}
				Deactivate ();
			}
		}
	}

	void Activate () {
		targetWave = target.GetComponent<Enemy>().wave;
		waveModeActive = true;
		//waveform.gameObject.SetActive (true);
		//targetWaveform.gameObject.SetActive (true);
		HoundLine.gameObject.SetActive(true);
		TargetLine.gameObject.SetActive(true);
		ApplyBulletTime();
	}

	void Deactivate () {
		target = null;
		waveModeActive = false;
		//waveform.gameObject.SetActive (false);
		//targetWaveform.gameObject.SetActive (false);
		HoundLine.gameObject.SetActive(false);
		TargetLine.gameObject.SetActive(false);
		//Time.timeScale = normalTime;
		UnApplyBulletTime();
	}

	void UpdateWaves () {
		Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

		//mouseAmplitude = Mathf.Sin(Time.time);
		//mouseFrequency = Mathf.Sin(Time.time);
		mouseAmplitude += input.y * sensitivity;
		mouseFrequency -= input.x * sensitivity;

		houndWave.amplitude = roundToFloat(mouseAmplitude, snap);
		houndWave.frequency = roundToFloat(mouseFrequency, snap);

		DrawWaves ();
	}

	float roundToFloat (float number, float rounder){
		//System.Math.Round (number, decimals);
		return Mathf.Round (number / rounder) * rounder;
	}

	bool isMatchingTarget () {
		if (houndWave.amplitude == targetWave.amplitude && houndWave.frequency == targetWave.frequency) {
			Debug.Log ("GOOD MATCH!");
			return true;
		}

		Debug.Log ("BAD MATCH!");
		return false;
	}

	void DrawWaves () {
		waveform.localScale = Vector3.up * houndWave.amplitude + Vector3.right * houndWave.frequency + Vector3.forward;
		targetWaveform.localScale = Vector3.up * targetWave.amplitude + Vector3.right * targetWave.frequency + Vector3.forward;

		int halfPositions = HoundLine.positionCount / 2;
		int x;
		for (x = -halfPositions; x < halfPositions; x++) {
			Vector3 pos = Vector3.zero;
			float y = houndWave.amplitude * Mathf.Sin (x * xFactor * houndWave.frequency) * yFactor;
			pos += Vector3.right * (x * xFactor);
			pos -= Vector3.forward * y;
			HoundLine.SetPosition (x + halfPositions, pos);
		}
		for (x = -halfPositions; x < halfPositions; x++) {
			Vector3 pos = Vector3.zero;
			float y = targetWave.amplitude * Mathf.Sin (x * xFactor * targetWave.frequency) * yFactor;
			pos += Vector3.right * (x * xFactor);
			pos -= Vector3.forward * y;
			TargetLine.SetPosition (x + halfPositions, pos);
		}
	}

	bool CheckClickOnEnemy () {
		//Debug.Log ("check click");
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100);
		if (hit) {
			if (hit.collider.tag == "Enemy") {
				target = hit.collider.gameObject;
				return true;
			}
		}
		return false;
	}

	void DamageTarget () {
		target.GetComponent<Enemy>().Hurt();
	}

	void ApplyBulletTime () {
		Time.timeScale = bulletTimeFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
	}

	void UnApplyBulletTime () {
		Time.timeScale = 1;
		Time.fixedDeltaTime = normalFixedDeltaTime;
	}
}
