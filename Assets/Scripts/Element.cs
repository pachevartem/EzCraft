using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ez
{
	
	public class Element : MonoBehaviour
	{

		[SerializeField]
		private Material MyMaterials;

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
			print(other.name);
		}
	}
}