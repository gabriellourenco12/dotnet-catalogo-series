using System;
using System.Linq;

namespace Cadastro_Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries(false);
						break;
                    case "2":
						ListarSeries(true);
						break;
					case "3":
						InserirSerie();
						break;
					case "4":
						AtualizarSerie();
						break;
					case "5":
						ExcluirSerie();
						break;
					case "6":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;
                    case "F":
						PayRespects();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("AGORA SOMOS APENAS 299, FOI UMA HONRA LUTAR AO SEU LADO");
            Console.WriteLine("COME BACK WITH YOUR SHIELD, OR ON IT");
			Console.ReadLine();
        }

        private static void PayRespects()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("KING LEONIDAS AND THE SPARTANS WERE HONORED");
            Console.ReadKey();
            Console.Clear();
        }

        private static void VisualizarSerie()
        {
            Console.Clear();
            Console.WriteLine("--- VISUALIZAR SÉRIE ---");
            Console.WriteLine();
			int indiceSerie = PegarId();

            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("THIS IS WHERE WE HOLD THEM, THIS IS WHERE WE FIGHT!!");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ExcluirSerie()
        {
            Console.Clear();
            Console.WriteLine("--- EXCLUIR SÉRIE ---");
            Console.WriteLine();
			int indiceSerie = PegarId();
            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine("#ID {0}: - {1}", serie.retornaId(), serie.retornaTitulo());
            Console.WriteLine("VOCÊ REALMENTE DESEJA FAZER ESSA SÉRIE MARCHAR NAS TERMÓPILAS? S OU N");
            var resposta = Console.ReadLine();

            if (resposta.ToUpper() == "S" || resposta.ToUpper() == "SIM")
            {
                repositorio.Exclui(indiceSerie);
                Console.WriteLine("{0} DERROTADA EM BATALHA",serie.retornaTitulo());
                Console.WriteLine("PRESS F TO PAY RESPECTS");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("{0} RETORNOU MAIS FORTE",serie.retornaTitulo());
                Console.ReadKey();
            }  
        }

        private static void AtualizarSerie()
        {
            Console.Clear();
            Console.WriteLine("--- ATUALIZAR SÉRIE ---");
            Console.WriteLine();
			int indiceSerie = PegarId();

			Serie atualizaSerie = PegarInfo(indiceSerie);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
            Console.WriteLine();
            Console.WriteLine("THEN WE WILL FIGHT IN SHADE!!");
            Console.ReadKey();
            Console.Clear();
        }

        private static void InserirSerie()
        {
            Console.Clear();
            Console.WriteLine("--- INSERIR NOVA SÉRIE ---");
            Console.WriteLine();
			Serie novaSerie = PegarInfo(repositorio.ProximoId());

			repositorio.Insere(novaSerie);
            Console.WriteLine();
            Console.WriteLine("THIS IS SPARTA!!");
            Console.ReadKey();
            Console.Clear();
        }
        private static Serie PegarInfo(int index)
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
            Console.WriteLine();
			Console.Write("DIGITE O GÊNERO: ");
			int IN_Genero = int.Parse(Console.ReadLine());

			Console.Write("DIGITE O TÍTULO: ");
			string IN_Titulo = Console.ReadLine();

			Console.Write("DIGITE O ANO DE INÍCIO: ");
			int IN_Ano = int.Parse(Console.ReadLine());

			Console.Write("DIGITE A DESCRIÇÃO: ");
			string IN_Descricao = Console.ReadLine();

            Console.Write("DÊ UMA NOTA PARA SÉRIE: ");
			double IN_Nota = double.Parse(Console.ReadLine());

			Serie novaSerie = new Serie(id: index,
										genero: (Genero)IN_Genero,
										titulo: IN_Titulo,
										ano: IN_Ano,
										descricao: IN_Descricao,
                                        nota: IN_Nota);
            return novaSerie;
        } 
        private static int PegarId()
        {
            int index = 0;
            bool b = false;
            var lista = repositorio.Lista();
            do
            {
                Console.Write("DIGITE O ID DA SÉRIE: ");
                index = int.Parse(Console.ReadLine());
                if (lista.Count > index)
                {
                    b = true;
                }
                else
                {
                    Console.WriteLine("\nID INEXISTENTE!");
                    Console.WriteLine("PRESSIONE UMA TECLA PARA VISUALIZAR A LISTA DE SÉRIES");
                    Console.ReadKey();
                    ListarSeries(b);                    
                }
            } while (b == false); 
            return index;  
        }

        private static void ListarSeries(bool b)
        {
            Console.Clear();
            Console.WriteLine("--- LISTA DE SÉRIES ---");
            Console.WriteLine();
			var lista = repositorio.Lista();
            if (b == true) lista = lista.OrderBy(a => a.Nota).ToList();

			if (lista.Count == 0)
			{
				Console.WriteLine("NENHUMA SÉRIE CADASTRADA.");
                Console.ReadKey();
				return;
			}

			foreach (var serie in lista)
			{   
                var excluido = serie.retornaExcluido();
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "(DEFEATED)":""));
                Console.WriteLine("NOTA: {0}", serie.retornaNota());
                Console.WriteLine();
			}
            Console.WriteLine();
            Console.WriteLine("SPARTANS! PREPARE FOR GLORY!");
            Console.ReadKey();
        }

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("BEM VINDO AO SPARTANS SERIES");
			Console.WriteLine("DIGA SUAS INTENÇÕES:");

			Console.WriteLine("1- LISTAR TODAS AS SÉRIES");
            Console.WriteLine("2- SÉRIES MAIS VOTADAS");
			Console.WriteLine("2- INSERIR NOVA SÉRIE");
			Console.WriteLine("3- ATUALIZAR SÉRIE");
			Console.WriteLine("4- EXCLUIR SÉRIE");
			Console.WriteLine("5- VISUALIZAR SÉRIE");
			Console.WriteLine("C- LIMPAR TELA");
            Console.WriteLine("F- TO PAY RESPECTS FOR KING LEONIDAS");
			Console.WriteLine("X- THIS IS SPARTA (SAIR)");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
