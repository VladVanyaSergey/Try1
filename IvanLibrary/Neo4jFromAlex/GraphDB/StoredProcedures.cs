using GraphDB.Models.Ontology;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDB
{
    internal static class StoredQueries
    {
        public static string CountQuery(Dictionary<string, object> dictionary)
        {
            return $"MATCH (node) WHERE {BuildConditions(dictionary, "node")} RETURN count(node)";
        }

        public static string CreateEntityQuery(Entity item)
        {
            return $"CREATE (node{BuildLabels(item)} {{{BuildParameters(item.Properties)}}}) RETURN node";
        }

        public static string CreateEntityWithoutQuery(Entity item)
        {
            return $"CREATE (node{BuildLabels(item)}) RETURN node";
        }

        public static string FindEntityQuery(Entity item)
        {
            return $"MATCH (node{BuildLabels(item)} {{{BuildParameters(item.Properties)}}}) RETURN node";
        }

        public static string FindEntityQuery(string[] labels)
        {
            return $"MATCH (node{BuildLabels(labels)}) RETURN node";
        }

        public static string CreateLinkQuery(Link item)
        {
            return $"MATCH (source) WHERE id(source) = {item.StartNodeId} " +
                $"MATCH (target) WHERE id(target) = {item.EndNodeId} " +
                $"CREATE (source)-[link:{item.Type} {{{BuildParameters(item.Properties)}}}]->(target) " +
                $"RETURN link, source, target";
        }

        public static string CountQuery(string[] labels)
        {
            return $"MATCH (node{BuildLabels(labels)}) RETURN count(node)";
        }

        public static string GetEntityById(long id)
        {
            return $"MATCH (node) WHERE id(node) = {id} RETURN node";
        }

        public static string GetLinkById(long id)
        {
            return $"MATCH ()-[link]->() WHERE id(link) = {id} RETURN link";
        }
            
        public static string FindLinkQuery(Link item)
        {
            return $"MATCH (source)-[link {{{BuildParameters(item.Properties)}}} ]->(target) RETURN source, link, target";
        }

        public static string UpdateEntityQuery(Entity item)
        {
            return $"MATCH (node) WHERE id(node) = {item.Id} REMOVE node{BuildLabels(item)} {BuildParamsForUpdate(item.Properties, "node")} SET node{BuildLabels(item)}";
        }



        public static string FindLinkQuery(string type)
        {
            return $"MATCH ()-[link:{type}]->() RETURN link";
        }

        public static string UpdateLinkQuery(Link item)
        {
            return $"MATCH ()-[link:{item.Type}]-() WHERE id(link) = {item.Id} SET {BuildParameters(item.Properties, "link")}";
        }

        public static string GetEntityByClass(string[] labels)
        {
            return $"MATCH (node{BuildLabels(labels)}) RETURN node";
        }

        public static string GetLinksByClass(string label)
        {
            return $"MATCH ()-[link:{label}]-() RETURN link";
        }


        private static string BuildLabels(Entity item)
        {
            return BuildLabels(item.Labels);
        }

        private static string BuildLabels(string[] labels)
        {
            if (labels == null)
            {
                return "";
            }
            string rlt = ":";
            foreach (var label in labels)
            {
                rlt += label + ":";
            }
            return rlt.Remove(rlt.Length - 1);
        }

        private static string ExtractId(Dictionary<string, object> dictionary)
        {
            if (dictionary.ContainsKey("Id")) return dictionary["Id"] as string;
            else return "null";
        }

        private static string ExtractMainLabel(Dictionary<string, object> dictionary)
        {
            if (dictionary.ContainsKey("Class")) return dictionary["Class"] as string;
            else return "Undefined";

        }

        private static string BuildParameters(Dictionary<string, object> dictionary)
        {
            var parameterNames = String.Empty;
            foreach (var item in dictionary)
            {

                
                    if (item.Value is string[] ? (item.Value as string[]).Count() == 0 ? true : false : false) continue;
                    parameterNames += $" {item.Key} : ${item.Key},";

            }

            return parameterNames.Remove(parameterNames.Length - 1, 1);
        }

        private static object BuildParamsForUpdate(Dictionary<string, object> properties, string v)
        {
            var parameterNames = String.Empty;
            foreach (var item in properties)
            {
                if (item.Value is string[] ? (item.Value as string[]).Count() == 0 ? true : false : false) continue;
                parameterNames += $" SET {v}.{item.Key} = ${item.Key} ";
            }
            return parameterNames;
        }


        private static string BuildParameters(Dictionary<string, object> dictionary, string prefix)
        {
            var parameterNames = String.Empty;
            foreach (var item in dictionary)
            {
                if (item.Key != "Class" && item.Value != null) parameterNames += $"{prefix}.{item.Key} : ${item.Key},";
            }
            return parameterNames.Remove(parameterNames.Length - 1, 1);
        }

        private static string BuildConditions(Dictionary<string, object> dictionary)
        {
            var conditions = String.Empty;
            foreach (var item in dictionary)
            {
                if (item.Value != null) conditions += $" {item.Key} = '{item.Value as string}' AND"; 
            }

            return conditions.Remove(conditions.Length - 3);
        }

        private static string BuildConditions(Dictionary<string, object> dictionary, string prefix)
        {
            var conditions = String.Empty;
            foreach (var item in dictionary)
            {
                if (item.Value != null) conditions += $" {prefix}.{item.Key} = '{item.Value as string}' AND";
            }

            return conditions.Remove(conditions.Length - 3);
        }


    }
}
