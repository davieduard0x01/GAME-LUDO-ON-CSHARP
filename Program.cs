using System;

namespace Ludo
{
    class Program
    {
        public static void AguardarJogador()
        {
            Console.WriteLine("Pressione qualquer tecla para lançar o dado.");
            Console.ReadKey();
        }

        public static int MoverPeca(int[] jogador, int posicaoAtual, int resultadoDado, int jogadorAtual)
        {
            if (resultadoDado == 6 && posicaoAtual == -1) // Movimento para o início da corrida
            {
                jogador[0] = 0;
                return 0; // Retorna a nova posição do peão
            }
            else if (posicaoAtual >= 0) // Movimento dentro da corrida
            {
                int novaPosicao = posicaoAtual + resultadoDado;
                if (novaPosicao <= 99) // Verifica se a nova posição está dentro do tabuleiro
                {
                    jogador[posicaoAtual] = -1; // Remove a peça da posição atual

                    // Verificar se a nova posição é uma área segura
                    if (novaPosicao != 9 && novaPosicao != 14 && novaPosicao != 22 && novaPosicao != 27 && novaPosicao != 35 && novaPosicao != 40 && novaPosicao != 48 && novaPosicao != 53 && novaPosicao != 61 && novaPosicao != 66 && novaPosicao != 74 && novaPosicao != 79 && novaPosicao != 87 && novaPosicao != 92)
                    {
                        // Verificar se a nova posição está ocupada por um peão adversário
                        if (jogador[novaPosicao] >= 0 && jogador[novaPosicao] != posicaoAtual)
                        {
                            // Capturar o peão adversário
                            int peaoCapturado = jogador[novaPosicao];
                            jogador[peaoCapturado] = -1; // Remover o peão capturado
                            Console.WriteLine("Você capturou o peão do jogador " + (3 - jogadorAtual) + "!");

                        }

                        jogador[novaPosicao] = posicaoAtual; // Coloca a peça na nova posição
                    }

                    return novaPosicao; // Retorna a nova posição do peão
                }
            }

            return posicaoAtual; // Retorna a posição atual se não houver movimento válido
        }

        public static bool JogoTerminado(int[] jogador1, int[] jogador2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (jogador1[i] != 100 || jogador2[i] != 100) // Verifica se algum peão ainda não chegou ao final
                {
                    return false;
                }
            }

            return true; // Todos os peões chegaram ao final, o jogo terminou
        }

        public static bool VerificarTresSeisConsecutivos(int jogadorAtual, int[] jogador1, int[] jogador2)
        {
            int[] jogadorTabuleiro = jogadorAtual == 1 ? jogador1 : jogador2;
            int contadorSeis = 0;

            for (int i = 0; i < 4; i++)
            {
                if (jogadorTabuleiro[i] == 6)
                {
                    contadorSeis++;
                }
            }

            return contadorSeis == 3; // Retorna verdadeiro se houver três lançamentos de seis consecutivos
        }

        public static int ProximoJogador(int jogadorAtual)
        {
            return jogadorAtual == 1 ? 2 : 1; // Retorna o número do próximo jogador
        }

        public static void MostrarTabuleiro(int[] jogador1, int[] jogador2)
        {
            Console.Clear();
            Console.WriteLine("  DAVI EDUARDO FERREIRA BARBOSA - S . I - PUC-MINAS ");
            Console.WriteLine("  _         _     _     ____      _______     ");
            Console.WriteLine(" | |       | |   | |   |  _ \\    |       |    ");
            Console.WriteLine(" | |       | |   | |   | | | |   | |---| |    ");
            Console.WriteLine(" | |       | |   | |   | | | |   | |   | |    ");
            Console.WriteLine(" | |____   | |___| |   | |_| |   | |---| |    ");
            Console.WriteLine(" |______|  |_______|   |____/    |______ |    ");
            Console.WriteLine();

            Console.WriteLine("Tabuleiro:");
            Console.WriteLine();

            Console.Write("Jogador 1: ");
            for (int i = 0; i < 4; i++)
            {
                Console.Write(jogador1[i] == -2 ? "F " : jogador1[i] == -1 ? "X " : jogador1[i] + " ");
            }
            Console.WriteLine();

            Console.Write("Jogador 2: ");
            for (int i = 0; i < 4; i++)
            {
                Console.Write(jogador2[i] == -2 ? "F " : jogador2[i] == -1 ? "X " : jogador2[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine();
        }

        public static int EscolherPeao(int jogadorAtual, int[] jogador)
        {
            int peaoSelecionado;

            while (true)
            {
                Console.WriteLine("Escolha um peão para mover (0-3):");
                string input = Console.ReadLine();

                if (int.TryParse(input, out peaoSelecionado))
                {
                    if (peaoSelecionado >= 0 && peaoSelecionado <= 3 && jogador[peaoSelecionado] != -2)
                    {
                        return peaoSelecionado;
                    }
                }

                Console.WriteLine("Peão inválido. Tente novamente.");
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Ludo!");

            bool jogoIniciado = false;
            bool novoJogo = true;

            while (novoJogo)
            {
                int[] jogador1 = { -1, -1, -1, -1 }; // Posições iniciais dos peões do jogador 1
                int[] jogador2 = { -1, -1, -1, -1 }; // Posições iniciais dos peões do jogador 2

                int jogadorAtual = 1;

                while (!JogoTerminado(jogador1, jogador2))
                {
                    MostrarTabuleiro(jogador1, jogador2);

                    if (VerificarTresSeisConsecutivos(jogadorAtual, jogador1, jogador2))
                    {
                        Console.WriteLine("Três lançamentos de seis consecutivos. Passando a vez para o próximo jogador...");
                        AguardarJogador();
                        jogadorAtual = ProximoJogador(jogadorAtual);
                        continue;
                    }

                    Console.WriteLine("Jogador " + jogadorAtual + ", é sua vez!");

                    AguardarJogador();

                    Random random = new Random();

                    int resultadoDado = random.Next(1, 7);

                    if (!jogoIniciado && resultadoDado != 6)
                    {
                        Console.WriteLine("Você precisa tirar um 6 para iniciar o jogo. Tente novamente.");
                        continue;
                    }

                    jogoIniciado = true;

                    Console.WriteLine("Resultado do dado: " + resultadoDado);

                    int[] jogador = jogadorAtual == 1 ? jogador1 : jogador2;
                    int peaoSelecionado = EscolherPeao(jogadorAtual, jogador);

                    int posicaoAtual = jogador[peaoSelecionado];

                    Console.WriteLine("Movendo peão " + peaoSelecionado + "...");

                    posicaoAtual = MoverPeca(jogador, posicaoAtual, resultadoDado, jogadorAtual);


                    if (posicaoAtual != -1 && jogador[posicaoAtual] == 100) // Verifica se o peão chegou ao final
                    {
                        Console.WriteLine("Parabéns, você levou o peão até o final!");

                        // Remove o peão do jogo
                        jogador[peaoSelecionado] = -2;
                    }

                    AguardarJogador();

                    jogadorAtual = ProximoJogador(jogadorAtual);
                }

                MostrarTabuleiro(jogador1, jogador2);
                Console.WriteLine("Parabéns, jogador " + jogadorAtual + "! Você venceu o jogo!");

                Console.WriteLine("Deseja jogar novamente? (S/N)");
                string reiniciar = Console.ReadLine();

                if (reiniciar.ToUpper() != "S")
                {
                    novoJogo = false;
                }
                else
                {
                    jogoIniciado = false;
                }
            }

            Console.WriteLine("Obrigado por jogar Ludo! Até mais!");
        }
    }
}


