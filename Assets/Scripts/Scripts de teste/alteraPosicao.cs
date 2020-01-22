using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alteraPosicao : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.W)) { //para frente

			print ("entrei no método GetKey() com W");

			//Find é a função que localiza um GameObject presente na cena, que está na hierarquia
			GameObject aux = GameObject.Find ("Player1");

			//transform já é uma propriedade acessível, pois ela é comum em todos os objetos,
			//por isso não é necessáro usar o GetComponent
			//aux.GetComponent<Transform>()
			//enquanto o botão W estiver pressionado, o player1 será igualado a 0.1f no eixo z
			//na verdade quando se usa position, ele está igualando um valor e não deslocando de um ponto até outro a partir de um vetor
			//aux.transform.position = new Vector3(aux.transform.position.x, aux.transform.position.y, aux.transform.position.z+0.1f);

			//o mesmo resultado obtido com aux.transform.position é obtido também com Translate
			//no geral, o Translate e o Rotate (para rotacionar) são as formas sempre usadas para movimentar objetos 3d na cena
			aux.transform.Translate(new Vector3(0.0f, 0.0f, 0.1f));
		}

		if (Input.GetKey (KeyCode.S)) { //para trás

			print ("entrei no método GetKey() com S");
			GameObject aux = GameObject.Find ("Player1");
			aux.transform.Translate(new Vector3(0.0f, 0.0f, -0.1f));
		}

		if (Input.GetKey (KeyCode.A)) { //para esquerda

			print ("entrei no método GetKey() com A");
			GameObject aux = GameObject.Find ("Player1");
			aux.transform.Translate(new Vector3(-0.1f, 0.0f, 0.0f));
		}

		if (Input.GetKey (KeyCode.D)) { //para direita

			print ("entrei no método GetKey() com D");
			GameObject aux = GameObject.Find ("Player1");
			aux.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f));
		}

		if (Input.GetKeyDown (KeyCode.F1)) { //rotaciona -45

			print ("entrei no método GetKey() com F1");
			GameObject aux = GameObject.Find ("Player1");
			//aux.transform.rotation = Quaternion.Euler(new Vector3(0.0f, -45f, 0.0f));

			aux.transform.Rotate(new Vector3(0.0f, -45.0f, 0.0f));
		}

		if (Input.GetKeyDown (KeyCode.F2)) { //rotaciona 45

			print ("entrei no método GetKey() com F2");
			GameObject aux = GameObject.Find ("Player1");
			//aux.transform.rotation = Quaternion.Euler(new Vector3(0.0f, -45f, 0.0f));

			aux.transform.Rotate(new Vector3(0.0f, 45.0f, 0.0f));
		}

		if (Input.GetKeyDown (KeyCode.P)) { //aumentar escala

			GameObject aux = GameObject.Find ("Player1");
			aux.transform.localScale = new Vector3 (aux.transform.localScale.x + 0.1f, aux.transform.localScale.y + 0.1f, aux.transform.localScale.z + 0.1f);
		}

		if (Input.GetKeyDown (KeyCode.O)) { //diminuir escala

			GameObject aux = GameObject.Find ("Player1");
			aux.transform.localScale = new Vector3 (aux.transform.localScale.x - 0.1f, aux.transform.localScale.y - 0.1f, aux.transform.localScale.z - 0.1f);
		}
	}
}
