using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Ez
{


	/// <summary>
	/// Будет хранить в себе исходные данные. 
	/// </summary>
	public class Data : MonoBehaviour
	{
		public int CountColor = 6; //TODO: читаем из файлв при инициализации приложения 6 для костылей
		
		public List<Color> Colors;  //TODO: тоже читать из файла

		public List<Material> OwnMaterils; //TODO: надо будет заменить на список цветов погруженных из документа
		
		public static Data Instanse;

		
		
		#region Singleton

		void Singleton()
		{
			if (Instanse != null && Instanse != this)
			{
				Destroy(gameObject);
				return;
			}
         
			Instanse = this;
			DontDestroyOnLoad(gameObject);
		}
		#endregion
	
		
		
		
		private void Awake()
		{
			Singleton();
		}
			
		
		
		
	}
}