// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : NetCoreFoundation
// File             : CoreStudent.cs
// Created          : 2017-05-17  17:36
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using StandardFoundation;

namespace NetCoreFoundation
{
    public class CoreStudent
    {
        public int Get()
        {
            StandardStudent student = new StandardStudent();
            int i = student.Get();
            return 2;
        }
    }
}