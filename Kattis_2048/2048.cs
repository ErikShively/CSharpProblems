using System;
using System.Collections.Generic;
class solve2048{
	static void MatrixPrint(List<int> Matrix, int H, int W){
		for(int i = 0; i < H; i++)
		{
			List<int> Row = new List<int>();
			for(int j = 0; j < W; j++)
			{
				Row.Add(Matrix[i*W+j]);
			}
			Console.WriteLine(string.Format(string.Join(" ", Row)));
		}
	}
	static List<int> MergeRecursive(List<int> Input, List<int> Output, int Direction){
		if(Input.Count == 0){
			return Output;
		}
		int	Pos1 = Input.Count - 1;
		int	Pos2 = Input.Count - 2;
		if(Direction == 0){
		Pos1 = 0;
		Pos2 = 1;
		}
		if(Input.Count<2){
			if(Direction == 0){
				Output.Add(Input[0]);
			}else{
				Output.Insert(0,Input[0]);
			}
			return Output;
		}else{
			if(Input[Pos1] == Input[Pos2]){
				if(Direction == 0){
					Output.Add(Input[Pos1] + Input[Pos2]);
					Input.RemoveRange(0,2);
				}else{
					Output.Insert(0,Input[Pos1] + Input[Pos2]);
					Input.RemoveRange(Pos2,2);
				}
				return(MergeRecursive(Input,Output,Direction));
			}else{
				if(Direction == 0){
					Output.Add(Input[Pos1]);
				}else{
					Output.Insert(0,Input[Pos1]);
				}
				Input.RemoveAt(Pos1);
				return(MergeRecursive(Input,Output,Direction));
			}
		}
	}

	static List<int> Merge(List<int> Matrix, int H, int W, int Direction){
		int[][] DirectionGuide = new int [][] 
		{new int [] {H,W,1,W,0},
		new int [] {W,H,W,1,0},
		new int [] {H,W,1,W,1},
		new int [] {W,H,W,1,1}};
		int[] Guide = DirectionGuide[Direction];
		List<int> ReturnList = Matrix;
		for(int i = 0; i<Guide[0]; i++)
		{
			List<int> TempList = new List<int>();
			for(int j = 0; j<Guide[1]; j++)
			{
				int jdx = (j * Guide[2]) + (i * Guide[3]);
				if(Matrix[jdx] != 0)
				{
					TempList.Add(Matrix[jdx]);
				}
			}
			List<int> MergeOutput = new List<int>();
			TempList = MergeRecursive(TempList, MergeOutput, Guide[4]);
			int Zeros = Guide[1] - TempList.Count;
			for(int j = 0; j<Guide[1]; j++)
			{
				int jdx = (j * Guide[2]) + (i * Guide[3]);
				if(Guide[4] == 1)
				{
					if(j<Zeros)
					{
						ReturnList[jdx] = 0;
					}
					else
					{
					    ReturnList[jdx] = TempList[j-Zeros];
					}
				}
				else
				{
				    if(j<TempList.Count)
				    {
					    ReturnList[jdx] = TempList[j];
				    }
				    else
				    {
					ReturnList[jdx] = 0;
				    }
				}
			}

		}
		return ReturnList;
	}
	static void Main(){
		List<int> matrix = new List<int>();
		for(int i = 0; i < 4; i++)
		{
			string[] line = Console.ReadLine().Split(' ');
			foreach(string num in line)
			{
				matrix.Add(Convert.ToInt32(num));
			}
		}
		int direction = Convert.ToInt32(Console.ReadLine());
		matrix = Merge(matrix, 4, 4, direction);
		MatrixPrint(matrix, 4,4);
	}
}