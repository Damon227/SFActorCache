// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : TestConsoleApp
// File             : Program.cs
// Created          : 2017-05-12  10:10
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<string> list = new List<string> { "a", "b", "c" };


            list.Insert(0, "s1");
            list.Insert(list.Count, "s2");
            list.Insert(list.Count, "s3");

            Console.ReadLine();
        }
    }
}