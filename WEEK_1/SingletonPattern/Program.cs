﻿﻿using System;
namespace SingletonPatternExample{
    public class Logger{
        private static Logger? _instance;
        private static readonly object _lockObj = new();
        private Logger(){
            Console.WriteLine("Logger has been started.");
        }
        public static Logger GetLogger(){
            if (_instance == null){
                lock (_lockObj){
                    if (_instance == null){
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }
        public void WriteLog(string message){
            Console.WriteLine("LOG: " + message);
        }
    }
    class Program{
        static void Main(string[] args){
            var log1 = Logger.GetLogger();
            log1.WriteLog("log1 is initialized.");
            var log2 = Logger.GetLogger();
            log2.WriteLog("log2 is initialized.");
            if (log1 == log2){
                Console.WriteLine("Same Logger instance is used.");
            }else{
                Console.WriteLine("Different Logger instances exist.");
            }
        }
    }
}
