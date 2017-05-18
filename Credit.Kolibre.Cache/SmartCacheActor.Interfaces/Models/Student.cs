// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : SmartCacheActor.Interfaces
// File             : Student.cs
// Created          : 2017-05-18  16:34
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace SmartCacheActor.Interfaces
{
    public class Student
    {
        public string Name { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public int Age { get; set; }

        public long Money { get; set; }

        public bool Enalbed { get; set; }
    }
}