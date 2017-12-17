using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ez
{	
	public class Element : MonoBehaviour
	{
		[SerializeField]
		private Material MyMaterials;

		public delegate void OnCollision();
		public static OnCollision ColisionColor;
		
		
		public Color Color
		{
			get { return MyMaterials.color; }
			set
			{
				MyMaterials.color = value;
			}
		}
		
		void Awake()
		{
			MyMaterials = gameObject.GetComponent<Renderer>().material;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == 9)
			{
				var e = other.gameObject.GetComponent<Element>();
				if (e.Color == Color)
				{
					print("Одинаковые");
					
				}
				else
				{
					print("Неодинаковые");
				}
			}
		}
	}
}