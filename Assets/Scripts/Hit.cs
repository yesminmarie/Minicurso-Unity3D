using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {
	/*
	//esse script está anexado na bola
	void OnMouseDown()
	{
		//ao clicar na bola irá imprimir esta mensagem
		//só funcionará se o objeto tiver um colisor
		print ("entrei na OnMouseEvent()");
	}

	*/

	float timer_bullet = 0.0f;
	public int damage_bullet;

	void Update()
	{
		//soma Time.deltaTime ao timer_bullet
		timer_bullet += Time.deltaTime;

		//se o tempo for maior que 1s e meio, a bola é destruída
		if (timer_bullet > 1.5f)
			Destroy (this.gameObject);
	}

	//o parâmetro é opcional
	void OnCollisionEnter(Collision col){
		//imprime o nome do game object com quem a bola colidir
		print (col.gameObject.name);
		Battle meuScript = GameObject.Find ("Controle").GetComponent<Battle> ();
		print (damage_bullet);
		meuScript.HitPlayer (col.gameObject.GetComponent<numPlayer>().nPlayer, damage_bullet);

//		if (col.gameObject.name == "Player 1 Azul") {
//			Battle meuScript = GameObject.Find ("Controle").GetComponent<Battle> ();
//			print (damage_bullet);
//			meuScript.HitPlayer (1, damage_bullet);
//		}
//
//		else if(col.gameObject.name == "Player 2 Vermelho"){
//			Battle meuScript = GameObject.Find ("Controle").GetComponent<Battle> ();
//			print (damage_bullet);
//			meuScript.HitPlayer (2, damage_bullet);
//		}
	}
}
