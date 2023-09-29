using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conway_s_game_of_Life
{
	internal class Program
	{
		public static class MyConsole
		{
			public static void Color(string message, ConsoleColor color)
			{
				Console.ForegroundColor = color;
				Console.WriteLine(message);
				Console.ResetColor();
			}

			public static void ColorSameLine(string message, ConsoleColor color, ConsoleColor color2)
			{
				Console.ForegroundColor = color;
				Console.BackgroundColor = color2;
				Console.Write(message);
				Console.ResetColor();
			}
		}

		public class Board
		{
			public int Height { get; }

			public int Width { get; }

			public int[,] Data { get; }

			public Board(int Width, int Height)
			{
				this.Width = Width;
				this.Height = Height;
				this.Data = new int[Width, Height];
			}

			public void Print()
			{
				for (int r = 0; r < Height; r++)
				{
					for (int c = 0; c < Width; c++)
					{
						MyConsole.ColorSameLine("|", ConsoleColor.Black, ConsoleColor.Black);
						if (Data[c, r] == 0)
						{
							//MyConsole.ColorSameLine($"{Data[r, c]}", ConsoleColor.Blue);
							MyConsole.ColorSameLine($" ", ConsoleColor.Black, ConsoleColor.Black);
						}
						if (Data[c, r] == 1)
						{
							//MyConsole.ColorSameLine($"{Data[r, c]}", ConsoleColor.Blue);
							MyConsole.ColorSameLine($"#", ConsoleColor.White, ConsoleColor.White);
						}
					}
					Console.WriteLine(" ");
				}
			}

		}

		static int checkforneighbors(Board board, int r, int c,int _xsize, int _ysize)
		{
			int neighbor = 0;
			for (int rowmod = -1; rowmod < 2; rowmod++)
			{
				for (int colmod = -1; colmod < 2; colmod++)
				{
					if (c - colmod >= 0 && r - rowmod >= 0 && c - colmod <= _xsize -1 && r - rowmod <= _ysize -1)
					{
						if (board.Data[r - rowmod, c - colmod] == 1)
						{
							neighbor++;
							//Console.WriteLine("neighbor found at " + (r - rowmod) + " " + (c - colmod));
						}
					}
				}
			}
			if (board.Data[r, c] == 1)
			{
				neighbor--; //we have to ignore ourselfs
			}
			//Console.Write(neighbor);
			return neighbor;
		}

		static void Main(string[] args)
		{
			int xsize = 10;
			int ysize = 10;
			int wait = 0;
			Board board = new Board(xsize, ysize);
			board.Data[2, 2] = 1;
			board.Data[3, 3] = 1;
			board.Data[3, 4] = 1;
			board.Data[4, 2] = 1;
			board.Data[4, 3] = 1;
			board.Print();
			System.Threading.Thread.Sleep(wait);
			List<int> addcoordinates = new List<int>();
			List<int> deletecoordinates = new List<int>();
			Console.Clear();
			for (;;)
			{
				for (int r = 0; r < xsize; r++)
				{
					for (int c = 0; c < ysize; c++)
					{
						int neighbor = checkforneighbors(board, r, c, xsize, ysize);
						if (neighbor < 2 || neighbor > 3)
						{
							deletecoordinates.Add(r);
							deletecoordinates.Add(c);
						}
						else if (board.Data[r,c] == 1)
						{
							if (neighbor == 2 || neighbor == 3)
							{
								addcoordinates.Add(r);
								addcoordinates.Add(c);
							}
						}
						else if (board.Data[r, c] == 0)
						{
							if (neighbor == 3)
							{
								addcoordinates.Add(r);
								addcoordinates.Add(c);
							}
							else
							{
								deletecoordinates.Add(r);
								deletecoordinates.Add(c);
							}
						}

					}
					//Console.WriteLine();
				}
				for (int i = 0; i < deletecoordinates.Count; i += 2)
				{ 
					board.Data[deletecoordinates[i], deletecoordinates[i+1]] = 0;
				}
				for (int j = 0; j < addcoordinates.Count; j += 2)
				{
					board.Data[addcoordinates[j], addcoordinates[j + 1]] = 1;
				}

				board.Print();
				addcoordinates.Clear();
				deletecoordinates.Clear(); 
				System.Threading.Thread.Sleep(wait);
				Console.Clear();
			}
			Console.ReadKey();

		}
	}
}