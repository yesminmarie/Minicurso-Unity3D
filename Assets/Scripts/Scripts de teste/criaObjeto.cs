using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class criaObjeto : MonoBehaviour {

	//o objeto foi criado como público para que ele possa aparecer como parâmetro do componente (script) no editor 
	//lembrando que o script, uma vez anexado no objeto, ele vira um componente
	//neste caso o Prefab Player1 será o objeto a ser arrastado no campo do parâmetro
	public GameObject meuPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F1)) { //criar o player 1
			Vector3 posicaoInicial = new Vector3(4.22f, -0.6699f, -3.68f);
			//Instantiate
			//Quaternion.identity serve para dizer que deverá ser usada a rotação que virá no prefab
			//Instantiate(meuPrefab, posicaoInicial, Quaternion.identity);
			Instantiate(meuPrefab, posicaoInicial, Quaternion.Euler(0.0f, -45.0f, 0.0f));
		}
	}
}
