using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTMLEngineLibrary
{
    public class EngineHtmlService : IEngineHtmlService
    {
        public string GetHtml(string template, object model)
        {
            var matches = Regex.Matches(template, "{{(.*)}}");
            foreach (var match in matches)
            {
                var propName = match.ToString().Remove(0, 2).Insert(0, "").Remove(match.ToString().Length - 4, 2)
                    .Insert(match.ToString().Length - 4, "");

                var val = ParseValue(propName, model);
                template = template.Replace(match.ToString(), val.ToString());
                var resp = ParseConstruction(propName, model);
                if (propName != resp) template = template.Replace(propName, resp);

            }

            if (Regex.Matches(template, "{{(.*)}}").Count != 0) return GetHtml(template, model);
            return template;
            
        }

        public string GetHtml(Stream pathTemplate, object model)
        {
            byte[] buffer = new byte[pathTemplate.Length];
            pathTemplate.Read(buffer);
            string path = buffer.ToString();
            string file = File.ReadAllText(path);
            return GetHtml(file, model);
        }

        public string GetHtml(byte[] bytes, object model)
        {
            throw new NotImplementedException();
        }

        public Stream GetHtmlInStream(string template, object model)
        {
            throw new NotImplementedException();
        }

        public Stream GetHtmlInStream(Stream pathTemplate, object model)
        {
            throw new NotImplementedException();
        }

        public Stream GetHtmlInStream(byte[] bytes, object model)
        {
            throw new NotImplementedException();
        }

        public byte[] GetHtmlInBytes(string template, object model)
        {
            throw new NotImplementedException();
        }

        public byte[] GetHtmlInBytes(Stream pathTemplate, object model)
        {
            throw new NotImplementedException();
        }

        public byte[] GetHtmlInBytes(byte[] bytes, object model)
        {
            throw new NotImplementedException();
        }

        public void GenerateAndSaveInDirectory(string templatePath, string outputPath, string outputNameFile, object model)
        {
            throw new NotImplementedException();
        }

        public void GenerateAndSaveInDirectory(Stream templatePath, string outputPath, string outputNameFile, object model)
        {
            throw new NotImplementedException();
        }

        public void GenerateAndSaveInDirectory(byte[] bytes, string outputPath, string outputNameFile, object model)
        {
            throw new NotImplementedException();
        }

        public Task GenerateAndSaveInDirectoryAsync(string templatePath, string outputPath, string outputNameFile, object model)
        {
            throw new NotImplementedException();
        }

        public Task GenerateAndSaveInDirectoryAsync(Stream templatePath, string outputPath, string outputNameFile, object model)
        {
            throw new NotImplementedException();
        }

        public Task GenerateAndSaveInDirectoryAsync(byte[] bytes, string outputPath, string outputNameFile, object model)
        {
            throw new NotImplementedException();
        }

        object ParseValue(string templatePiece, object model)
        {
            var modelProps = model.GetType().GetProperties();
            object result = templatePiece;
            foreach (var p in modelProps)
            {
                if (templatePiece == p.Name)
                {
                    result = p.GetValue(model)?.ToString();
                    break;
                }

                var splitPropName = templatePiece.Split('.');
                if (splitPropName.Length > 1 && splitPropName[0] == p.Name)
                {
                    string currentProp = splitPropName[0];
                    int currentLevel = 0;
                    var currentInsideModel = p.PropertyType;
                    var currentInsideProperty = p;
                    var currValue = model;
                    while (currentLevel < splitPropName.Length)
                    {
                        currValue = currentInsideProperty.GetValue(currValue);
                        currentInsideModel = currValue.GetType();
                        currentLevel += 1;
                        if(currentLevel < splitPropName.Length) currentProp = splitPropName[currentLevel];
                        currentInsideProperty = currentInsideModel.GetProperty(currentProp);
                    }

                    result = currValue;
                    break;

                }
            }

            return result;
        }

        string ParseConstruction(string templatePiece, object model)
        {
            var splitInstruction = templatePiece.Split(' ');
            if (splitInstruction[0] == "if" && splitInstruction.Length >= 5)
            {
                return ParseIf(templatePiece, splitInstruction, model);
            }

            if (splitInstruction[0] == "foreach")
            {
                return ParseForeach(templatePiece, splitInstruction, model);
            }

            return templatePiece;

            return "";
        }

        string ParseIf(string templatePiece, string[] splitInstruction, object model)
        {
                    bool ifResult = false;

                    var statements = Regex.Matches(templatePiece, "\\((.*)\\)");
                    if (statements.Count == 0) return templatePiece;
                    string returnedIfValue = statements[0].Value.Split(") (")[0].Replace("(","").Replace(")","");
                    string returnedElseValue = statements[0].Value.Split(") (").Length > 1 ? statements[0].Value.Substring(statements[0].Value.Split(") (")[0].Length).Remove(0,3).Insert(0,"") : "";
                    returnedElseValue = returnedElseValue.Remove(returnedElseValue.Length - 1, 1);

                    var innerReturnedIf = Regex.Match(returnedIfValue, "if(.*)");
                    if (innerReturnedIf.Value != "") returnedIfValue = ParseConstruction(returnedIfValue, model); 
                    
                    var innerReturnedElse = Regex.Match(returnedElseValue, "(.*)if(.*)");
                    if (innerReturnedElse.Value != "") returnedElseValue = ParseConstruction(returnedElseValue, model); 
                    
                    var separator = splitInstruction[2];

                    var leftVal = ParseValue(splitInstruction[1], model);
                    var rightVal = splitInstruction[3];

                    int leftInt;
                    int rightInt;

                    switch (separator)
                    {
                        case "==":
                            if (leftVal.ToString() == rightVal) 
                            {
                                ifResult = true;
                            }
                            else
                            {
                                if (Int32.TryParse(leftVal.ToString(), out leftInt) &&
                                    Int32.TryParse(rightVal, out rightInt) && leftInt == rightInt)
                                {
                                    ifResult = true;
                                }
                            }
                            break;
                        case "!=":
                            if (leftVal.ToString() != rightVal) 
                            {
                                ifResult = true;
                            }
                            else
                            {
                                if (Int32.TryParse(leftVal.ToString(), out leftInt) &&
                                    Int32.TryParse(rightVal, out rightInt) && leftInt != rightInt)
                                {
                                    ifResult = true;
                                }
                            }
                            break;
                        case ">":
                            if (Int32.TryParse(leftVal?.ToString(), out leftInt) &&
                                Int32.TryParse(rightVal, out rightInt) && leftInt > rightInt)
                            {
                                ifResult = true;
                            }
                            break;
                        case "<":
                            if (Int32.TryParse(leftVal.ToString(), out leftInt) &&
                                Int32.TryParse(rightVal, out rightInt) && leftInt < rightInt)
                            {
                                ifResult = true;
                            }
                            break;
                        default:
                            ifResult = false;
                            break;
                    }

                    return ifResult ? returnedIfValue : returnedElseValue;
        }

        string ParseForeach(string templatePiece, string[] splitInstruction, object model)
        {
            string returnedValue = "";
            var elemType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == splitInstruction[1]);
            string updatedPiece = Regex.Match(templatePiece, "\\((.*?)\\)").ToString().Replace("(","").Replace(")","");
            var enumType = splitInstruction[2];
            var collection = ParseValue(enumType, model);
            if (collection is IEnumerable enumerable)
            {
                foreach (var e in enumerable)
                {
                    var matches = Regex.Matches(updatedPiece, "{(.*?)}");
                    var addedStr = updatedPiece;
                    foreach (var match in matches)
                    {
                        var updatedValue = ParseValue(match.ToString()?.Replace("{","").Replace("}",""), e);
                        addedStr = addedStr.Replace(match.ToString(), updatedValue != null ? updatedValue.ToString() : "");
                    }

                    returnedValue += addedStr;
                }
            }
            return returnedValue;
        }
    }
}