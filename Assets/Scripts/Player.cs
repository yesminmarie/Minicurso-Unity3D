using UnityEngine;

public class Player {

	// Atributos = Campo
	string name;
	int currentLife; //vida atual
	int maxLife; //vida total
	int mentalPower;
	public GameObject obj3d; //variável para referência para o modelo 3D do player

	//Propriedade
	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	//Métodos
	public Player(string nome, int vidaTotal, int forcaMental)
	{
		name = nome;
		maxLife = vidaTotal;
		currentLife = maxLife;
		mentalPower = forcaMental;
	}
		
	public int getLife(int tipo)
	{
		if (tipo == 0)
			return currentLife;
		else
			return maxLife;
	}

	public int Attack(ref bool critico)
	{
		int aux = mentalPower / 2;
		int damage = mentalPower + Random.Range (-aux, aux);
		//como são números inteiros, o último é excluído, indo de 0 a 99
		int critical = Random.Range (0, 100);
		if (critical < 5) {// se critical for menor que 5, o ataque será multiplicado por 2
			damage *= 2;
			critico = true;
		} 
		else
			critico = false;
		
		return (damage);
	}

	public bool UpdateLife(int dano)
	{
		currentLife -= dano;
		if (currentLife < 0) { //morreu
			//Comando para destruir o objeto
			Object.Destroy(obj3d);
			return false;
		} else
			return true;
	}
}
