using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Self_Driving_Car_Programming_Game
{
    public class Translator
    {
        public List<KeyWord> program;
        
        public record KeyWord
        {           
            public Type type;
            public Name name;
            public int value;
        }
        
        public enum Name
        {
            //variables:
            speed, 
            //numbers:
            integer,
            //functions:
            set, get, math,
            //brackets:
            start, end,
            //special chars
            plus, minus, times, divided
        }

        public enum Type
        {
            var, num, fun, brackets, special
        }

        private void AddKeyWord(Type type, Name name, int value)
        {
            KeyWord key = new()
            {
                value = value,
                type = type,
                name = name
            };
            program.Add(key);
        }

        public void Translate(string[] lines)
        {
            program = new List<KeyWord>();
            foreach (string line in lines)
            {
                string word = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if ("(), +-*/".Contains(line[i]))
                    {
                        try ///check whether it is a number
                        {
                            int number = Convert.ToInt32(word);
                            AddKeyWord(Type.num, Name.integer, number);
                        }
                        catch
                        {
                            switch (word)
                            {
                                case "set":
                                    AddKeyWord(Type.fun, Name.set, 0);
                                    break;

                                case "math":
                                    AddKeyWord(Type.fun, Name.math, 0);
                                    break;

                                case "speed":
                                    AddKeyWord(Type.var, Name.speed, 0);
                                    break;                               
                            }
                        }
                        switch (line[i])
                        {
                            case '(':
                                AddKeyWord(Type.brackets, Name.start, 0);
                                break;

                            case ')':
                                AddKeyWord(Type.brackets, Name.end, 0);
                                break;

                            case '+':
                                AddKeyWord(Type.special, Name.plus, 0);
                                break;

                            case '-':
                                AddKeyWord(Type.special, Name.minus, 0);
                                break;

                            case '*':
                                AddKeyWord(Type.special, Name.times, 0);
                                break;

                            case '/':
                                AddKeyWord(Type.special, Name.divided, 0);
                                break;
                        }
                        word = "";                      
                    }
                    else
                    {
                        word += line[i];
                    }
                }
            }
        }

        public void Run(Car car)
        {
            for (int i = 0; i < program.Count; i++)
            {
                switch (program[i].type)
                {
                    case Type.fun:
                        List<KeyWord> subprogram = FindSubProg(program, i + 1);
                        switch (program[i].name)
                        {                                                       
                            case Name.set:
                                RunSet(car, subprogram); 
                                break;                          
                        }
                        i += subprogram.Count + 2; //also with brackets
                        break;
                }
            }
        }

        private void FindBrackets(List<KeyWord> program, int startIndex, int endIndex, 
            out int startBracket, out int endBracket) //returns index of first and last bracket
        {
            Stack<int> stack = new();
            startBracket = -1;
            endBracket = -1;
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (program[i].name == Name.start)
                {
                    stack.Push(i);
                }
                else if (program[i].name == Name.end)
                {
                    startBracket = stack.Pop();
                    endBracket = i;
                }
            }
            if (stack.Count != 0) throw new Exception("end bracket missing!");
        }

        private List<KeyWord> FindSubProg(List<KeyWord> program, int index)
        {
            int start, end;
            FindBrackets(program, index, program.Count - 1, out start, out end);
            if ((start == -1) || (end == -1))
            {
                return program.GetRange(0, 1);
            }
            else
            {
                return program.GetRange(start + 1, end - start - 1);
            }               
        }

        private void RunSet(Car car, List<KeyWord> subprogram) //set(var, number)
        {
            KeyWord _var = new ();
            _var.type = Type.var;

            //get value
            _var.value = RunGet<int>(subprogram[1], FindSubProg(subprogram, 2));

            //set value
            switch (subprogram[0].name)
            {
                case Name.speed:
                    car.speed = _var.value;
                    break;
            }
        }

        private T RunGet<T>(KeyWord function, List<KeyWord> subprogram)
        {
            if (typeof(T) == typeof(int))
            {
                int intValue = 0;

                if ((function.type == Type.var) || (function.type == Type.num))
                {
                    intValue = function.value;
                    return (T)Convert.ChangeType(intValue, typeof(T));
                }  

                switch (function.name)
                {
                    case Name.math:
                        intValue = RunMath(subprogram);
                        break;
                }
                
                return (T)Convert.ChangeType(intValue, typeof(T));
            }

            else if (typeof(T) == typeof(bool))
            {
                bool boolValue = false;
                
                return (T)Convert.ChangeType(boolValue, typeof(T));
            }

            else //should not be called
            {
                return (T)Convert.ChangeType(0, typeof(T));
            }
        }

        private int RunMath(List<KeyWord> subprogram)
        {
            List<KeyWord> sub1 = FindSubProg(subprogram, 0);
            int a = RunGet<int>(subprogram[0], sub1);

            int middle = subprogram.FindIndex(x => x.type == Type.special);

            List<KeyWord> sub2 = FindSubProg(subprogram, middle + 1);
            int b = RunGet<int>(subprogram[middle + 1], sub2);

            switch (subprogram[middle].name)
            {
                case Name.plus:
                    return a + b;

                case Name.minus:
                    return a - b;

                case Name.times:
                    return a * b;

                case Name.divided:
                    return a / b;
            }

            return 0; //should not be called
        }
    }
}
