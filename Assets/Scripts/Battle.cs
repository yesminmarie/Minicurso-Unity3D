using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//chaveamento de cenas
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour {

	//classe Player. É um array, pois terá o player 1 e 2
	Player[] players;
	int status; // -1 = game não começou, 0 = game rolando, 1 = player 1 vencedor, 2 = player 2 vencedor
	Vector3[] spawnPoints; //posição
	Quaternion[] rotSpawnPoints; //rotação
	Color[] playerColors;
	public GameObject meuPrefab;
	Text titulo, info, lifePlayer1, lifePlayer2;

	//Bullet
	public GameObject objBullet;
	Vector3 spawnBullet;

	// Use this for initialization
	void Start () {

		//player 1 e player 2
		players = new Player[2];
		status = -1;

		spawnPoints = new Vector3[2]; //aloca espaço para o vetor
		spawnPoints[0] = new Vector3(4.22f, -0.67f, -3.68f);//cria dentro de cada posição um new, usa o construtor para criar o objeto dentro de cada posição
		spawnPoints[1] = new Vector3(-4.11f, -0.67f, 4.53f);

		rotSpawnPoints = new Quaternion[2];
		//Quaternion tem uma excessão, ele possui o método estático Euler que é mais fácil que usar o new Quaternion
		rotSpawnPoints[0] = Quaternion.Euler(0.0f, -45.0f, 0.0f);
		rotSpawnPoints[1] = Quaternion.Euler(0.0f, 135.0f, 0.0f);

		playerColors = new Color[2];
		playerColors [0] = Color.blue;
		playerColors [1] = Color.red;

		titulo = GameObject.Find ("Title").GetComponent<Text> ();
		info = GameObject.Find ("Info").GetComponent<Text> ();

		lifePlayer1 = GameObject.Find ("LifePlayer1").GetComponent<Text> ();
		lifePlayer2 = GameObject.Find ("LifePlayer2").GetComponent<Text> ();

		spawnBullet = new Vector3(0.029f, 0.375f, 0.944f);
	}

	void CreatePlayer(int nPlayer)
	{
		print ("Entrou na CreatePlayer() com arg = " + nPlayer);
		//nPlayer-1 porque passou 1 ao invés de 0 para criar o player azul

		players[nPlayer-1].obj3d = (GameObject) Instantiate(meuPrefab, spawnPoints[nPlayer-1], rotSpawnPoints[nPlayer-1]);
		//coloca um nome no player
		//players [nPlayer - 1].obj3d.name = "Player" + nPlayer;
		players [nPlayer - 1].obj3d.name = players[nPlayer-1].Name;
		//coloca a cor no player
		players [nPlayer - 1].obj3d.GetComponent<MeshRenderer> ().material.color = playerColors [nPlayer - 1];

		//coloca o númerodo jogador
		players [nPlayer - 1].obj3d.GetComponent<numPlayer>().nPlayer = nPlayer;

		ControlaInterface ();
	}

	void ControlaCritico(bool status)
	{
		titulo.enabled = status;
		titulo.text = "C R I T I C O";
		titulo.color = new Color(
			GameObject.Find ("Title").GetComponent<Text> ().color.r,
			GameObject.Find ("Title").GetComponent<Text> ().color.g,
			GameObject.Find ("Title").GetComponent<Text> ().color.b,
			255);
	}
	void ControlaInterface()
	{
		if (players [0] == null) // entro aqui quando aperto F2
		{
			GameObject.Find ("Title").GetComponent<Text> ().text = "Aguardando Plyer 1 Azul";
			lifePlayer2.enabled = true;
			lifePlayer2.text = players [1].getLife (0).ToString () + "/" + players [1].getLife (1).ToString ();
		} 
		else if (players [1] == null) //entro aqui quando aperta F1
		{
			GameObject.Find ("Title").GetComponent<Text> ().text = "Aguardando Plyer 2 Vermelho";
			lifePlayer1.enabled = true;
			lifePlayer1.text = players [0].getLife (0).ToString () + "/" + players [0].getLife (1).ToString ();

		} 
		else { //players[0] e players[1] são diferentes de nulo
			GameObject.Find ("Title").GetComponent<Text> ().enabled = false;
			lifePlayer1.enabled = true;
			lifePlayer2.enabled = true;
			lifePlayer1.text = players [0].getLife (0).ToString () + "/" + players [0].getLife (1).ToString ();
			lifePlayer2.text = players [1].getLife (0).ToString () + "/" + players [1].getLife (1).ToString ();
			//se os dois players existem então o jogo começou, status = 0
			if (status == -1)
				status = 0;
		}
			
	}
	// Update is called once per frame
	void Update () {

		if (status == -1)
			//faz o texto piscar
			titulo.color = new Color (titulo.color.r, titulo.color.g, titulo.color.b, (Mathf.Sin (Time.time * 4.0f)+1.0f)/2.0f);

		if (Input.GetKeyDown (KeyCode.F10)) // reseta o game
		{ 
			//carrega a cena atual
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		if (Input.GetKeyDown (KeyCode.F1)) { 
			//Se não foi criado, ele cria player 1 (Azul)
			if (players [0] == null) {
				//cria o player lógicamente usando a classe Player(string nome, int vidaTotal, int forcaMental)
				players[0] = new Player("1 Azul", 100, 14);
				CreatePlayer (1); //poderia ser 0
				//GameObject.Find("Title").GetComponent<Text>().enabled = false;
			}

		}

		if (Input.GetKeyDown (KeyCode.F2)) {
			//Se não foi criado, ele cria player 2 (Vermelho)
			if (players [1] == null) {
				//cria o player lógicamente usando a classe Player(string nome, int vidaTotal, int forcaMental)
				players[1] = new Player("2 Vermelho", 100, 14);
				CreatePlayer (2); 
				//GameObject.Find("Title").GetComponent<Text>().enabled = false;
			}

		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {

			//cria um objeto temporário para instanciar a bola, ela será crida no centro
			GameObject objTmp = (GameObject) Instantiate (objBullet, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0f, 0f, 0f));
			//diz que o pai do objeto é o player 1
			objTmp.transform.parent = players [0].obj3d.transform;
			//coloca a bola na posição do cano
			objTmp.transform.localPosition = spawnBullet;

			//cria um Rigidbody no objTmp
			Rigidbody tiro = objTmp.GetComponent<Rigidbody> ();

			//adiciona uma força para a bola, fazendo ela ir na direção z com intensidade de 500. Eixo x -1.0f para ela ir na diagonal
			tiro.AddForce(new Vector3(-1.0f, 0.0f, 1.0f) * 500.0f, ForceMode.Acceleration);

			bool foiCritico = false;
			//pega o script Hit do objeto
			Hit scriptAux = objTmp.GetComponent<Hit> ();
			scriptAux.damage_bullet = players[0].Attack (ref foiCritico);
			ControlaCritico (foiCritico);

			//Ataque do Player 1 Azul

			//se o player 1 e o player 2 foram criados e o status = 0, o player 1 pode atacar
//			if (players [0] != null && players [1] != null && status == 0) {
//
//				bool foiCritico = false;
//				int damage = players [0].Attack (ref foiCritico);
//				ControlaCritico (foiCritico);
//				print (players[0].Name + " atacou com " + damage + " de dano");
//				info.enabled = true;
//				info.text = players [0].Name + " atacou com " + damage + " de dano";
//				//se retornar verdadeiro o Player 2 Vermelho não morreu
//				if (players [1].UpdateLife (damage)) {
//					//print (players [1].Name + " ficou com " + players [1].getLife () + " de vida.");
//					lifePlayer2.text = players [1].getLife (0).ToString () + "/" + players [1].getLife (1).ToString ();
//
//				}
//				else{
//					//Player 2 Vermelho, índice 1, morreu
//					print("Player 1 Azul Vencedor!!");
//					GameObject.Find("Title").GetComponent<Text>().enabled = true;
//					GameObject.Find("Title").GetComponent<Text>().color = new Color (titulo.color.r, titulo.color.g, titulo.color.b, 255.0f);
//					GameObject.Find("Title").GetComponent<Text>().text = "Player 1 Azul Vencedor!!";
//					//some o texto do player 2
//					lifePlayer2.enabled = false;
//					status = 1;
//				}
//			}

		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {

			//cria um objeto temporário para instanciar a bola
			GameObject objTmp = (GameObject) Instantiate (objBullet, new Vector3 (0f, 0f, 0f), Quaternion.Euler (0f, 0f, 0f));
			//diz que o pai do objeto é o player 1
			objTmp.transform.parent = players [1].obj3d.transform;
			//coloca a bola na posição do cano
			objTmp.transform.localPosition = spawnBullet;

			//cria um Rigidbody no objTmp
			Rigidbody tiro = objTmp.GetComponent<Rigidbody> ();

			//adiciona uma força para a bola, fazendo ela ir na direção z com intensidade de 500. Eixo x -1.0f para ela ir na diagonal
			tiro.AddForce(new Vector3(1.0f, 0.0f, -1.0f) * 500.0f, ForceMode.Acceleration);

			bool foiCritico = false;
			//pega o script Hit do objeto
			Hit scriptAux = objTmp.GetComponent<Hit> ();
			scriptAux.damage_bullet = players[1].Attack (ref foiCritico);
			ControlaCritico (foiCritico);

			//Ataque do Player 2 Vermelho
			//se o player 1 e o player 2 foram criados e o status = 0, o player 1 pode atacar
//			if (players [0] != null && players [1] != null && status == 0) {
//				bool foiCritico = false;
//				int damage = players [1].Attack (ref foiCritico);
//				ControlaCritico (foiCritico);
//				print (players[1].Name + " atacou com " + damage + " de dano");
//				info.enabled = true;
//				info.text = players [1].Name + " atacou com " + damage + " de dano";
//				//se retornar verdadeiro o Player 1 Azul não morreu
//				if (players [0].UpdateLife (damage)) {
//					//print (players [0].Name + " ficou com " + players [0].getLife () + " de vida.");
//					lifePlayer1.text = players [0].getLife (0).ToString () + "/" + players [0].getLife (1).ToString ();
//
//				}
//				else{
//					//Player 1 Azul, índice 0, morreu
//					print("Player 2 Vermelho Vencedor!!");
//					GameObject.Find("Title").GetComponent<Text>().enabled = true;
//					GameObject.Find("Title").GetComponent<Text>().color = new Color (titulo.color.r, titulo.color.g, titulo.color.b, 255.0f);
//					GameObject.Find("Title").GetComponent<Text>().text = "Player 2 Vermelho Vencedor!!";
//					//some o texto do player 1
//					lifePlayer1.enabled = false;
//					status = 2;
//				}
//			}

		}

	} //final do UPDATE

	int GetAnotherPlayerIndex(int idx)
	{
		if (idx == 1)
			return 2;
		else if (idx == 2)
			return 1;

		return -1;
	}

	public void HitPlayer(int nPlayer, int damage)
	{
		if (players [nPlayer - 1].UpdateLife (damage)) {
			//print (players [1].Name + " ficou com " + players [1].getLife () + " de vida.");
			info.enabled = true;
			//troca o indice para escrever certo no texto
			info.text = players [GetAnotherPlayerIndex(nPlayer) -1].Name + " atacou com " + damage + " de dano";
			//pega o nome que está no Canvas "LifePlayer1 ou 2"
			string tmpNomePlayer = "LifePlayer" + nPlayer;
			GameObject aux = GameObject.Find (tmpNomePlayer);
			aux.GetComponent<Text>().text = players [nPlayer - 1].getLife (0).ToString () + "/" + players [nPlayer - 1].getLife (1).ToString ();

		} else {
			titulo.enabled = true;
			titulo.color = new Color (titulo.color.r, titulo.color.g, titulo.color.b, 255.0f);
			//troca o indice para escrever certo no texto
			titulo.text = "Player " + players[GetAnotherPlayerIndex(nPlayer) -1].Name + " vencedor!!!";
			//pega o nome que está no Canvas "LifePlayer1 ou 2"
			string tmpNomePlayer = "LifePlayer" + nPlayer;
			GameObject aux = GameObject.Find (tmpNomePlayer);
			aux.GetComponent<Text> ().enabled = false; 
			status = nPlayer;
		}
	}
} //final da CLASS
