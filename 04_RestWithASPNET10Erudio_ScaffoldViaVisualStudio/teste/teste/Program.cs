
Console.WriteLine("Eae, voce vai beber hoje?");

int sim = 0;
int nao = 1;

if (sim > 6)
{
	Console.WriteLine("Sim");
}

else if(nao < 1)
{
	Console.WriteLine("Não");
}

while (sim == 0)
{
	Console.WriteLine("Sim, mas só um pouco!");
}

Console.ReadLine();
