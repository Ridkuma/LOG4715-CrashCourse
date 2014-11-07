﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CarController))]
public class Style : MonoBehaviour {
	
	private CarController car;
	private float seuilMinSpeed;
	private float seuilMaxSpeed;
	private int points;
	private bool frolage;
	private int cptPoints;
	
	public int CptPoints
	{
		set { this.cptPoints = value; }
	}

	public GUIText textPoint;
	public float factorMinSpeed;
	public float factorMaxSpeed;
	
	public int ajoutFrolage;
	public int ajoutVitesseFolle;
	public int ajoutEnLAir;
	public int ajoutExplosion;

	void Start() {
		this.points = 0;
		this.cptPoints = 0;
		this.car = this.GetComponentInParent<CarController>();
		this.seuilMinSpeed = this.car.MaxSpeed * this.factorMinSpeed;
		this.seuilMaxSpeed = this.car.MaxSpeed * this.factorMaxSpeed;
	}

	void OnTriggerEnter(Collider other)	{
		if (other.tag == "Style") {
			this.frolage = true;
		}
	}

	void OnTriggerExit(Collider other)	{
		if (other.tag == "Style")
		{
			this.frolage = false;
		}
	}

	void FixedUpdate() {
		if (this.car.CurrentSpeed >= this.seuilMinSpeed) {
			if (frolage) {
				this.points += this.ajoutFrolage;
				this.cptPoints += this.ajoutFrolage;
			}
			if(!this.car.AnyOnGround) {
				this.points += this.ajoutEnLAir;
				this.cptPoints += this.ajoutEnLAir;
			}
		}
		if (this.car.CurrentSpeed >= this.seuilMaxSpeed) {
			this.points += this.ajoutVitesseFolle;
			this.cptPoints += this.ajoutVitesseFolle;
		}
		this.textPoint.text = "Score : " + this.points;



		if(this.cptPoints > 1000) {
			this.cptPoints -= 1000;
			Boost carBoost = this.gameObject.GetComponentInParent<Boost>();
			if(carBoost) {
				carBoost.AddBoost();
			}
		}
	}

	public void AddExplosionPoints() {
		this.points += this.ajoutExplosion;
		this.cptPoints += this.ajoutExplosion;
	}
}
