// ***********************************************************************
// Solution         : FengNiaoWu
// Project          : Credit.Kolibre.Foundation
// File             : NewPaginatedList.cs
// Created          : 2017-03-24  2:18 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Xml;

namespace Credit.Kolibre.Foundation.Sys.Collections.Generic
{
    public class NewPaginatedList<T> : List<T>, IPaginatedList<T>
    {
        private readonly Dictionary<string, XmlDictionaryString> dictionary = new Dictionary<string, XmlDictionaryString>();

        #region IPaginatedList<T> Members

//        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
//        {
//            string name = type.Name;
//            string namespa = type.Namespace;
//
//            typeName = new XmlDictionaryString(XmlDictionary.Empty, name, 0);
//            typeNamespace = new XmlDictionaryString(XmlDictionary.Empty, namespa, 0);
//
//            if (!dictionary.ContainsKey(type.Name))
//            {
//                dictionary.Add(name, typeName);
//            }
//            if (!dictionary.ContainsKey(type.Namespace))
//            {
//                dictionary.Add(namespa, typeNamespace);
//            }
//
//            return true;
//        }
//
//        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
//        {
//            XmlDictionaryString tName;
//            XmlDictionaryString tNameSpace;
//            if (dictionary.TryGetValue(typeName, out tName) && dictionary.TryGetValue(typeNamespace, out tNameSpace))
//            {
//                return GetType();
//            }
//            return null;
//        }
        public bool HasNextPage { get; }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPageCount { get; }

        #endregion
    }
}