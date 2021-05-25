using System;

namespace การบ้านarray
{
    class Program
    {
        static void Main(string[] args)
        {
            string a,b,c; int newW,newH,i,j;
            a = Console.ReadLine();
            b = Console.ReadLine();
            c = Console.ReadLine();
            double[,] datainput = ReadImageDataFromFile(a);
            double[,] dataConvolution = ReadImageDataFromFile(b);
            newW = dataConvolution.GetLength(1)-1; 
            newH = dataConvolution.GetLength(0)- 1;
            double[,] newdatainput = new double[datainput.GetLength(0)+(newH), datainput.GetLength(1) + (newW)];
            for (i=0;i<datainput.GetLength(0);i++)
            { 
                for (j = 0; j < datainput.GetLength(1); j++)
                { newdatainput[i + (newH / 2), j + (newW / 2)] = datainput[i, j]; }
            }
            for (i = 0; i < newdatainput.GetLength(0); i++)
            {
                for (j = newW / 2; j < newdatainput.GetLength(1) - newW / 2; j++)
                {
                    if (i < newH / 2)
                    { newdatainput[i, j] = newdatainput[i + datainput.GetLength(0), j]; }

                    else if (i >= newdatainput.GetLength(0) - newH / 2)
                    { newdatainput[i, j] = newdatainput[i - datainput.GetLength(0), j]; }
                }
            }
            for (i = newH/2; i < newdatainput.GetLength(0)-newH/2; i++)
            {
                for (j = 0; j < newdatainput.GetLength(1); j++)
                {
                    if (j < newW / 2)
                    { newdatainput[i, j] = newdatainput[i , j + datainput.GetLength(1)]; }

                    else if (j >= newdatainput.GetLength(1) - newW / 2)
                    { newdatainput[i, j] = newdatainput[i , j - datainput.GetLength(1)]; }
                }
            }
            for (i = 0; i < newdatainput.GetLength(0); i++)
            {
                for (j = 0; j < newdatainput.GetLength(1); j++)
                {
                    if ( i<newH/2 && j < newW / 2)
                    { newdatainput[i, j] = newdatainput[i+ datainput.GetLength(0), j + datainput.GetLength(1)]; }

                    else if ( i >= newdatainput.GetLength(0) - newH / 2 && j >= newdatainput.GetLength(1) - newW / 2)
                    { newdatainput[i, j] = newdatainput[i - datainput.GetLength(0), j - datainput.GetLength(1)]; }
                    
                    else if ( i >= newdatainput.GetLength(0) - newH / 2 && j < newW / 2)
                    { newdatainput[i, j] = newdatainput[i - datainput.GetLength(0), j + datainput.GetLength(1)]; }

                    else if (i < newH / 2 && j >= newdatainput.GetLength(1) - newW / 2)
                    { newdatainput[i, j] = newdatainput[i + datainput.GetLength(0), j - datainput.GetLength(1)]; }
                }
            }
            Convolve(newdatainput, dataConvolution, newH, newW,c);
        }

        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }
        
        static void Convolve(double [,] newdatainput ,double [,] dataConvolution,int newH, int newW,string c)
        {
            double[,] afterconvolve = new double[newdatainput.GetLength(0)-newH, newdatainput.GetLength(1)-newW];
            double[,] aftermultiply = new double[newdatainput.GetLength(0), newdatainput.GetLength(1)];  
            int i, j,k,l; 
            for (i=0; i< afterconvolve.GetLength(0); i++)
            {
                for (j = 0; j < afterconvolve.GetLength(1); j++)
                {
                    for (k = 0; k < dataConvolution.GetLength(0); k++)
                    {
                        for (l = 0; l < dataConvolution.GetLength(1); l++)
                        {
                            
                            afterconvolve[i, j] = afterconvolve[i, j]+(newdatainput[i + k, j + l] * dataConvolution[k, l]);
                        }
                    }
                }
            }
            WriteImageDataToFile(c, afterconvolve);
        }

        static void WriteImageDataToFile(string imageDataFilePath,double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i,
                                                imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }

            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
        }
    }
}
