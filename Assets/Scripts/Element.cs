using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ez
{
	
	public class Element : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			print(other.name);
		}
	}
}