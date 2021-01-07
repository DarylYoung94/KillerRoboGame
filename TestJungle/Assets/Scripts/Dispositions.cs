using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Dispositions : MonoBehaviour
{
	int[,] dispositions = new int[4, 4];
	
    void Start()
    {
        CreateDispositionArray();

        printArray();
    }

    bool ExistsInColumn (int number, int column)
    {
        for (int i=0; i<4; i++)
        {
            if (dispositions[i, column] == number)
            {
                return true;
            }
        }

        return false;
    }

    bool ExistsInRow (int number, int row)
    {
        for (int i=0; i<4; i++)
        {
            if (dispositions[row,i] == number)
            {
               return true;
            }
        }

        return false;
    }

    bool IsAllowed(int row, int col, int number)
    {
        if (ExistsInRow(number,row) || ExistsInColumn(number,col))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void printArray()
    {
        int rowLength = dispositions.GetLength(0);
        int colLength = dispositions.GetLength(1);
        string msg = "";

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                msg += string.Format("{0} ", dispositions[i, j]);
            }
            msg += "\n";
        }
       // Debug.Log(msg);
    }

    void CreateDispositionArray()
    {
        BuildDispositions(0,0);
    }

    bool BuildDispositions(int row, int col)
    {
        int start_col = col;
        int start_row = row;
        for (int i=start_row; i<4; i++)
        {
            List<int> numbers = new List<int> { 1, 2, 3};
            for (int j=0; j<4; j++)
            {
                if (dispositions[i,j] != 0)
                {
                    numbers.Remove(dispositions[i,j]);
                }
            }

            Shuffle(numbers);
            int index = 0;

            for (int j=start_col; j<4; j++)
            {
                if( i != j && dispositions[i,j] == 0)
                {
                    do
                    {
                        int temp = numbers[index];

                        if (IsAllowed(i,j, temp))
                        {
                            dispositions[i,j] = temp;

                            if (BuildDispositions(i,j))
                            {
                                numbers.RemoveAt(index);
                                return true;
                            }
                            else
                            {
                                dispositions[i,j] = 0;
                            }
                        }
                        
                        index++;
                        
                    } while (index < numbers.Count);

                    return false;
                }
            }

            start_col = 0;
        }

        return true;
    }

    List<int> Shuffle(List<int> list_)  
    {  
        List<int> list = list_;
        System.Random rng = new System.Random();
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            int value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  

        return list;
    }
}

