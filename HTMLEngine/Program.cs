using System;
using System.Collections.Generic;
using System.IO;
using HTMLEngine.Models;
using HTMLEngineLibrary;

namespace HTMLEngine
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string template = File.ReadAllText(@"C:\Users\sreut\RiderProjects\HTMLEngine\Templates\index.template");
            var students = new Student[]
            {
                new Student {FirstName = "Lox", LastName = "Loxov", Document = "880055"},
                new Student {FirstName = "Dolboeb", LastName = "Eblanov", Document = "880057"},
                new Student {FirstName = "Yebok", LastName = "Yebishev", Document = "880064"},
                new Student {FirstName = "Gavnoed", LastName = "Bezmamny", Document = "880091"},
                new Student {FirstName = "Ueban", LastName = "Dolbaebsky", Document = "880099"}
            };
            var group = new Group {Code = "11-106", Students = students};
            var professor = new Professor { FirstName = "Debilov", LastName = "Lox", MiddleName = "Debilovich", Discipline = "pornhub.com", Group = group};

            var service = new EngineHtmlService();
            string valResult = service.GetHtml(template, professor);
            Console.WriteLine(valResult);
        }

    }
}