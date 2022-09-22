using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            set,
            //brackets:
            start, end
        }

        public enum Type
        {
            var, num, fun, brackets
        }

        public void Translate(string[] lines)
        {
            program = new List<KeyWord>();
            foreach (string line in lines)
            {
                string word = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if ("(), ".Contains(line[i]))
                    {
                        KeyWord key = new ();

                        try ///check wheter it is a number
                        {
                            int number = Convert.ToInt32(word);
                            key.value = number;
                            key.type = Type.num;
                            key.name = Name.integer;
                            program.Add(key);
                            key = new();
                        }
                        catch
                        {
                            switch (word)
                            {
                                case "set":
                                    key.type = Type.fun;
                                    key.name = Name.set;
                                    program.Add(key);
                                    key = new();
                                    break;

                                case "speed":
                                    key.type = Type.var;
                                    key.name = Name.speed;
                                    program.Add(key);
                                    key = new();
                                    break;                               
                            }
                        }
                        switch (line[i])
                        {
                            case '(':
                                key.type = Type.brackets;
                                key.name = Name.start;
                                program.Add(key);
                                key = new();
                                break;

                            case ')':
                                key.type = Type.brackets;
                                key.name = Name.end;
                                program.Add(key);
                                key = new();
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
                        //RunFun();
                        int start, end;
                        FindBrackets(program, i, program.Count - i, out start, out end);
                        List<KeyWord> subprogram = program.GetRange(start + 1, end - start);
                        switch (program[i].name)
                        {                                                       
                            case Name.set:
                                RunSet(car, subprogram);
                                break;
                        }
                        i = end;
                        break;
                }
            }
        }

        private void FindBrackets(List<KeyWord> program, int startIndex, int endIndex, out int startBracket, out int endBracket)
        {
            Stack<int> stack = new();
            startBracket = -1;
            endBracket = -1;
            for (int i = startIndex + 1; startIndex <= endIndex; i++)
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

                if (stack.Count == 0) break;
            }
        }

        private void RunSet(Car car, List<KeyWord> subprogram) //set(var, number)
        {
            KeyWord _var = new ();
            _var.type = Type.var;
            switch (subprogram[1].type)
            {
                /*case Type.function:
                    _var.value = RunFun();
                    break;*/
                case Type.num:
                    _var.value = subprogram[1].value;
                    break;
                case Type.var:
                    _var.value = subprogram[1].value;
                    break;
            }

            switch (subprogram[0].name)
            {
                case Name.speed:
                    car.speed = _var.value;
                    break;
            }
        }
    }
}
