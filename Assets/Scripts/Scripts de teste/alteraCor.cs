using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alteraCor : MonoBehaviour {

	// 1) Identificar o evento de apertar uma tecla do teclado
	// 2) Modificar as propriedades da câmera

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C)) {
			print ("Entrei no método GetKeyDown(C) ");

			//Find é a função que localiza um GameObject presente na cena, que está na hierarquia
			GameObject obj = GameObject.Find ("Main Camera");
			//muda o Clear Flags de main camera, que está configurado como Skybox, para solid color
			obj.GetComponent<Camera> ().clearFlags = CameraClearFlags.SolidColor;
			//muda a cor do background da câmera
			obj.GetComponent<Camera> ().backgroundColor = new Color (0.3f, 0.8f, 0.4f);
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			print ("Entrei no método GetKeyDown(P) ");

			GameObject piso = GameObject.Find ("Piso");

			//pega o elemento MeshRender para mudar a textura (mainTexture, mas no editor se chama Main Maps) do material vigente
			//será passado o caminho da imagem, dizendo que o type of é uma textura
			//e reforçando o tipo, que é textura (as Texture)
			piso.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Images/steal", typeof(Texture)) as Texture;
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			print ("Entrei no método GetKeyDown(D) ");

			GameObject player1 = GameObject.Find ("Player1");
			//pega o componente MeshRender para mudar a cor do material vigente, mas poderia mudar também o material
			//player1.GetComponent<MeshRenderer>().material.color = Color.blue;

			//agora o material será igualado à quem está na pasta "Resources", 
			//passando o caminho onde está o material
			//o tipo que é material
			//e reforçando o tipo que é material (as Material), pois o Load poderia ser usado para carregar uma imagem, por exemplo.
			//o Load procura apenas dentro da pasta Resources (obrigatoriamente a pasta deve ter esse nome)
			player1.GetComponent<MeshRenderer>().material = Resources.Load("Materials/blue", typeof(Material)) as Material;
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			print ("Entrei no método GetKeyDown(E) ");

			GameObject player2 = GameObject.Find ("Player2");
			//pega o elemento MeshRender para mudar a cor (a instância) do material vigente, mas poderia mudar também o material
			//player2.GetComponent<MeshRenderer>().material.color = Color.red;

			//agora o material será igualado à quem está na pasta "Resources", 
			//passando o caminho onde está o material
			//o tipo que é material
			//e reforçando o tipo que é material (as Material), pois o Load poderia ser usado para carregar uma imagem, por exemplo.
			//o Load procura apenas dentro da pasta Resources (obrigatoriamente a pasta deve ter esse nome)
			player2.GetComponent<MeshRenderer>().material = Resources.Load("Materials/red", typeof(Material)) as Material;
		}
	}
}
