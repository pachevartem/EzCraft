using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ez
{
	
	public class Element : MonoBehaviour
	{

		public Material MyMaterials;

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