using GraphDB;
using GraphDB.Models.Logic;
using GraphDB.Models.Ontology;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection con = new Connection("neo4j", "bwe319tu");
            TransactionManager transaction = new TransactionManager(con);

            




            var logicelementsender = new LogicElement(LogicType.ValueSender);
            logicelementsender.Value = 1;
            logicelementsender.Activate();

            var logicelementacceptor = new LogicElement(LogicType.ValueMoreThan);
            logicelementacceptor.Value = 2;

            var logicelementdummy = new LogicElement(LogicType.Dummy);


            var dictionary1 = new Dictionary<string, object>()
            {
                {"Name", "Entity1" },
                {"Property 1", "Property 1 value"},
                {"Property 2", "Property 2 value"}
            };

            var dictionary2 = new Dictionary<string, object>()
            {
                {"Name", "Entity2" },
                {"Property 1", "Property 1 value"},
                {"Property 2", "Property 2 value"},
                {"Property 3", "Property 3 value"}
            };

            var dictionary3 = new Dictionary<string, object>()
            {
                {"Name", "Entity3" },
                {"Property 1", "Property 1 value"},
                {"Property 2", "Property 2 value"},
                {"Property 3", "Property 3 value"}
            };

            var dictionary4 = new Dictionary<string, object>()
            {
                {"Name", "Link1" },
                {"Property 1", "Property 1 value"},
                {"Property 2", "Property 2 value"},
                {"Property 3", "Property 3 value"}
            };

            var dictionary5 = new Dictionary<string, object>()
            {
                {"Name", "Link2" },
                {"Property 1", "Property 1 value"},
                {"Property 2", "Property 2 value"},
                {"Property 3", "Property 3 value"}
            };

            var neo4jresults = new List<Neo4jUnwrapedResult>();

            var entity1 = new Entity(new string[]{ "Test", "Sender" });
            entity1.AddLogic(logicelementsender);
            entity1.AddProperty(dictionary1);

            var rlt = transaction.CreateEntity(entity1);
            entity1 = rlt.AgregatedNodes[0];
            neo4jresults.Add(rlt);


            var entity2 = new Entity(new string[] { "Test", "Sender" });
            entity2.AddLogic(logicelementsender);
            entity2.AddProperty(dictionary2);

            rlt = transaction.CreateEntity(entity2);
            entity2 = rlt.AgregatedNodes[0];
            neo4jresults.Add(rlt);

            logicelementacceptor.AddTarget(entity1);
            logicelementacceptor.AddTarget(entity2);
            var entity3 = new Entity(new string[] { "Test", "Acceptor", "Concentrator" });
            entity3.AddLogic(logicelementacceptor);
            entity3.AddProperty(dictionary3);

            rlt = transaction.CreateEntity(entity3);
            entity3 = rlt.AgregatedNodes[0];
            neo4jresults.Add(rlt);




            var link1 = new Link("SEND_VALUE");
            link1.AddProperty(dictionary4);
            link1.AddLogic(logicelementdummy);
            link1.AddConnection(entity1, entity3);

            rlt = transaction.CreateLink(link1);
            link1 = rlt.AgregatedLinks[0];
            neo4jresults.Add(rlt);

            var link2 = new Link("SEND_VALUE");
            link2.AddProperty(dictionary5);
            link2.AddLogic(logicelementdummy);
            link2.AddConnection(entity2, entity3);

            rlt = transaction.CreateLink(link2);
            link1 = rlt.AgregatedLinks[0];
            neo4jresults.Add(rlt);


            Console.WriteLine(new LogicHandler(transaction).Handle(entity3.LogicElement));

            


            rlt = transaction.CountQuery(new[] { "Test" });
            neo4jresults.Add(rlt);


            rlt = transaction.FindEntity(new Entity("Test"));
            neo4jresults.Add(rlt);


            rlt = transaction.FindLink(new Link("SEND_VALUE"));
            neo4jresults.Add(rlt);


            var entity4 = new Entity();
            entity4.AddProperty(dictionary2);

            rlt = transaction.FindEntity(entity4);
            neo4jresults.Add(rlt);


            var link3 = new Link();
            link3.AddProperty(dictionary4);

            rlt = transaction.FindLink(link3);
            neo4jresults.Add(rlt);

            rlt = transaction.GetLinkById(143);
            neo4jresults.Add(rlt);

            rlt = transaction.GetEntitiesByClass(new string[] { "Test" });
            neo4jresults.Add(rlt);

            rlt = transaction.GetLinksByClass( "SEND_VALUE" );
            neo4jresults.Add(rlt);

            entity3.AddProperty("Added", "Added atribute");
            rlt = transaction.UpdateEntity(entity3);

            Console.ReadKey(false);




            //var entity2 = new Entity()
            //{
            //    //Class = "Яблоня",
            //    Name = "Дерево",
            //    IsActive = false,
            //    Atributes = new string[]
            //    {
            //        "Тип"+":"+"Дерево",
            //        "Происхождение"+":"+ "Болгария",
            //        "Дата" +":"+ "%Дата%",
            //        "Время"+":"+ "%Дата%"
            //    },
            //    Logic = new LogicalElement()
            //    {

            //        Value = 0,
            //        Type = LogicalType.ValueActivation
            //    }
            //};

            //IStatementResult result;
            //IResultSummary res = null;
            //IRecord rec = null;


            //InsertEmptyLine("Creation 1");

            //result = transaction.CreateEntity(entity);
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("Creation 2");

            //result = transaction.CreateEntity(entity2.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("Finding 1");

            //result = transaction.FindEntity(entity1.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("Finding 2");

            //var entity3 = new Entity(entity2.Id);
            //result = transaction.FindEntity(entity3.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("Create Link 1");

            //var link = new Link
            //{
            //    //Class = "PARENT",
            //    Name = "Род-вид",
            //    IsActive = false,
            //    AtributesDictionary = new Dictionary<string, string>
            //    {
            //        {"Комментарий", "Излишний"}
            //    },
            //    Source = entity2,
            //    Target = entity1

            //};

            //result = transaction.CreateLink(link.GetParameters(), link.Source.GetParameters(), link.Target.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("Find Link 1");

            //result = transaction.FindLink(link.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("Find Link 2");

            //var link2 = new Link(link.Id);
            //result = transaction.FindLink(link2.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("IsExist 1");

            //result = transaction.IsExist(entity1.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("IsExist 2");

            //result = transaction.IsExist(entity2.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

            //InsertEmptyLine("IsExist 3");

            //result = transaction.IsExist(entity3.GetParameters());
            ////res = result.Consume();
            ////rec = result.Peek();
            //Unwrap(res, rec, result);

        }

    //    private static void Unwrap(IResultSummary res, IRecord rec, IStatementResult result)
    //    {
    //        Console.WriteLine("Unwrapping Result");
    //        UnwrapResponse(result);
    //        //Console.WriteLine("Unwrapping Consuming");
    //        //UnwrapResponse(res.Statement);
    //        //Console.WriteLine("Unwrapping Peek");
    //        //UnwrapResponse(rec);
    //    }

    //    private static void UnwrapResponse(IStatementResult result)
    //    {
    //        //Console.WriteLine("Keys count in response: " + result.Keys.Count);
    //        //if (result.Keys.Count != 0)
    //        //{
    //        //    foreach (var field in result)
    //        //    {
    //        //        foreach (var item in field.Keys)
    //        //        {
    //        //            var obj = field[item];
    //        //            Console.WriteLine(obj.GetType().ToString());
    //        //            if (obj is INode)
    //        //            {
    //        //                var node = obj.As<INode>();
    //        //                ParceNode(node);
    //        //                double st = 2, mg = 4;
    //        //            }
    //        //            else if (obj is IRelationship)
    //        //            {
    //        //                var link = obj.As<IRelationship>();
    //        //                link.
    //        //                ParceLink(link);
    //        //            }
    //        //            else
    //        //            {
    //        //                Console.WriteLine($"Returned value type is ({obj.GetType()})");
    //        //                Console.WriteLine($"Returned value is ({ UnwindValue(obj)})");
    //        //            }
    //        //        }
    //        //    }
    //        //}
    //    }

    //    private static void ParceLink(IRelationship link)
    //    {
    //        Console.WriteLine($"Item is link and has ID({link.Id}) and");
    //        Console.WriteLine($"has type ({link.Type})");
    //        Console.WriteLine("has items:");
    //        foreach (var item in link.Properties)
    //        {
    //            Console.WriteLine($"Key ({item.Key}) Value ({UnwindValue(item.Value)})");
    //        }
    //    }

    //    private static string UnwindValue(object value)
    //    {
    //        if (value is string) return value as string;
    //        if (value is bool) return ((bool)value).ToString();
    //        if (value is int) return ((int)value).ToString();
    //        if (value is long) return ((long)value).ToString();
    //        if (value is List<object>)
    //        {
    //            string Result = string.Empty;
    //            foreach (var item in ((List<object>)value))
    //            {
    //                Result += '\n'+UnwindValue(item);
    //            }
    //            return Result;
    //        }
    //        else return "UNDEFINED";

    //    }

    //    private static void ParceNode(INode node)
    //    {
    //        Console.WriteLine($"Item is node and has ID({node.Id}) and");
    //        Console.WriteLine("has labels:");
    //        foreach (var item in node.Labels)
    //        {
    //            Console.WriteLine(item + ",");
    //        }
    //        Console.WriteLine("has items");
    //        foreach (var item in node.Properties)
    //        {
    //            Console.WriteLine($"Key ({item.Key}) Value ({UnwindValue(item.Value)})");
    //        }
    //    }

    //    private static void UnwrapResponse(IRecord rec)
    //    {


    //        try
    //        {
                 

    //            var enumer = rec.Keys.Select(x => x);
    //            for (int i = 0; i < enumer.ToArray().Length; i++)
    //            {
    //                Console.WriteLine($"'{rec.Keys[i]}' is '{rec.Values[rec.Keys[i]].GetType()}' and has '{rec.Values[rec.Keys[i]] as string}' value");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e.Message);
    //        }

    //    }

    //    private static void InsertEmptyLine(string name)
    //    {
    //        Console.ReadKey(true);
    //        Console.WriteLine();
    //        Console.WriteLine(name);
    //    }

    //    static void UnwrapResponse(Statement stat)
    //    {
            
    //        foreach (var item in stat.Parameters)
    //        {
    //            Console.WriteLine($"'{item.Key}' is '{item.Value.GetType()}' and has '{item.Value as string}' value");
    //        }
             
    //    }
    }

}
