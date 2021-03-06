﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
           var connect = DBManager.getInstance();
           for (int i = 0; i<10; i++)
           {
               connect = DBManager.getInstance();
           }
           
           Console.ReadKey(); 
        }
    }

    public class DBManager { 
        private DBManager() { }
        private static DBManager connect; 
        private static object syncLock = new object();

        public static DBManager getInstance(){
            Console.WriteLine("getting an instance of DBManager...");
            
            //return available instance if any : avoid unnecessary locking 
            if (connect != null)
            {
                return connect;
            }

            //Multithreading context locking access to this block!
            lock(syncLock){
                if (connect == null) {
                    Console.WriteLine("==> Warning !!! Creating new instance of DBManager!!!");
                    connect = new DBManager();
                return connect; 
                }else {                   
                    return connect; 
                }    
            }             
        }    
    }
    
    
    public class Mapper
    {
        private static readonly object SyncLock = new object();
        private static IMapper _instance;
        private Mapper() { }

        public static IMapper GetInstance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                lock (SyncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MapperConfiguration(cfg => cfg.AddProfiles(Assembly.GetExecutingAssembly())).CreateMapper();
                        return _instance;
                    }
                    return _instance;
                }
            }
        }
    }
}
