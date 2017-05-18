// ***********************************************************************
// Solution         : Credit.Kolibre.Cache
// Project          : CacheActorConsoleApp
// File             : Program.cs
// Created          : 2017-05-10  10:53
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using CacheActor.Interfaces;
using Credit.Kolibre.Foundation.Cache;
using Credit.Kolibre.Foundation.Sys;
using SmartCacheActor.Interfaces;

namespace CacheActorConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ICacheService cacheService = new CacheService();
            IStudentSmartCacheService studentSmartCacheService = new StudentSmartCacheService();
            ITeacherSmartCacheService teacherSmartCacheService = new TeacherSmartCacheService();

            Student student = new Student
            {
                Name = "yuanchengman",
                Age = 24,
                CreateTime = DateTimeOffset.Now
            };

            Student student1 = new Student
            {
                Name = "yuanchengman1",
                Age = 241,
                CreateTime = DateTimeOffset.Now
            };


            KolibreCacheOptions options = new KolibreCacheOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(1000),
                SlidingExpiration = TimeSpan.FromSeconds(3)
            };

            studentSmartCacheService.SetSmartCacheAsync(null, "key1", student, options).Wait();
            Student s1 = studentSmartCacheService.GetSmartCacheAsync(null, "key1").Result;
            Console.WriteLine(s1.ToJson());

            studentSmartCacheService.SetSmartCacheAsync(null, "key2", new List<Student> { student, student1 }, options).Wait();
            List<Student> s2 = studentSmartCacheService.GetSmartCachesAsync(null, "key2").Result;
            Console.WriteLine(s2.ToJson());

            Teacher teacher = new Teacher
            {
                Name = "957",
                IsGood = true
            };

            teacherSmartCacheService.SetSmartCacheAsync(null, "t1", teacher, options).Wait();
            List<Teacher> t1 = teacherSmartCacheService.GetSmartCachesAsync(null, "t1").Result;
            List<Student> t2 = studentSmartCacheService.GetSmartCachesAsync(null, "t1").Result;

            // 测试普通结构缓存

            //string key1 = "normal:yuanchengman";
            //cacheService.SetCacheAsync(key1, student, options).Wait();
            //for (int i = 0; i < 1000; i++)
            //{
            //    Student result1 = cacheService.GetCacheAsync<Student>(key1).Result;
            //    Console.WriteLine(i + ":" + result1.ToJson());
            //}

            Console.ReadLine();
        }
    }


    //ActorId actorId = new ActorId("123456789");
    //ICacheActor cacheActor = ActorProxy.Create<ICacheActor>(actorId, new Uri("fabric:/Credit.Kolibre.Cache/CacheActorService"));

    //Student student = new Student
    //{
    //Name = "yuanchengman",
    //Age = 24,
    //Address = "上海",
    //CreateTime = DateTimeOffset.Now
    //};

    //KolibreCacheEntryOptions options = new KolibreCacheEntryOptions
    //{
    //AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(100),
    //SlidingExpiration = TimeSpan.FromMinutes(3)
    //};

    //// 测试普通结构缓存

    //string key1 = "normal:yuanchengman";
    //cacheActor.SetCacheAsync(key1, student.ToJson(), options).Wait();
    //string result1 = cacheActor.GetCacheAsync(key1).Result;

    //string result3 = cacheActor.GetHashCacheAsync(key1, "HashKey1").Result;

    //Console.WriteLine($@"key : {key1}, value :" + result1);

    //KolibreCacheEntryOptions options1 = new KolibreCacheEntryOptions
    //{
    //AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(-100),
    //SlidingExpiration = TimeSpan.FromMinutes(3)
    //};

    //// 刷新缓存
    //cacheActor.RefreshAsync(key1, options1).Wait();
    //string result2 = cacheActor.GetCacheAsync(key1).Result;
    //Console.WriteLine($@"刷新后 Hash key : {key1}, value :" + result2);

    //////////////////////////////////////////////////////////
    //// 测试 Hash 结构缓存

    //string key2 = "hash:yuanchengman";
    //string hashKey2 = "name";
    //cacheActor.SetHashCacheAsync(key2, hashKey2, "yuanchengman", options).Wait();
    //string hashResult = cacheActor.GetHashCacheAsync(key2, hashKey2).Result;
    //Console.WriteLine($@"key : {key2}:{hashKey2}, value :" + hashResult);

    //// 刷新缓存
    //cacheActor.RefreshHashCacheAsync(key2, options1).Wait();
    //string hashResult2 = cacheActor.GetHashCacheAsync(key2, hashKey2).Result;
    //Console.WriteLine($@"刷新后 Hash key : {key2}:{hashKey2}, value :" + hashResult2);

    /////////////////////////////////////////////////////////
    //// 测试 List 结构缓存

    //string key3 = "list:yuanchengman";
    //cacheActor.LeftPushListCacheAsync(key3, "a", options).Wait();
    //cacheActor.RightPushListCacheAsync(key3, "b", options).Wait();
    //cacheActor.RightPushListCacheAsync(key3, "c", options).Wait();
    //cacheActor.LeftPushListCacheAsync(key3, "s", options).Wait();

    //string listResult1 = cacheActor.LeftPopListCacheAsync(key3).Result;
    //Console.WriteLine($@"List Hash key : {key3}, value : {listResult1} ");
    //string listResult2 = cacheActor.RightPopListCacheAsync(key3).Result;
    //Console.WriteLine($@"List Hash key : {key3}, value : {listResult2} ");

    //// 刷新缓存
    //cacheActor.RefreshListCacheAsync(key3, options1);
    //string listResult3 = cacheActor.LeftPopListCacheAsync(key3).Result;
    //Console.WriteLine($@"刷新后 List Hash key : {key3}, value : {listResult3} ");
}