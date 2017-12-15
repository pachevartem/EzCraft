using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ez
{

	/// <summary>
	/// Описывает работы таймера
	/// </summary>
	public class Timer : MonoBehaviour
	{

		public int StartTimer = 10;  //TODO: сделать считывание с файла 

		/// <summary>
		/// Запустить отсчет
		/// </summary>
		public void BeginTimer()
		{
			StartCoroutine(TimeOff(StartTimer));
		}

		IEnumerator TimeOff(int startTime)   
		{
			while (true)
			{
				yield return new WaitForSeconds(1);
				startTime--;
	
				if (startTime<0)
				{
					Debug.LogError("Вы програли - время кончилось");
				}
			}
		}

	}
}