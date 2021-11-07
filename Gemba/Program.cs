
using Gemba.IObjects;
using Gemba.Objects;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gemba
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<Elem> Elems = new List<Elem>(); // 6 шт
            List<IJunction> Junctions = new List<IJunction>(); // 5 шт
            List<IAction> Actions = new List<IAction>();
            List<IVariable> Variables = new List<IVariable>();

            int id = 0; // Переменная для записи Variables

            // Операции элементов
            // 1 - сумма
            // 2 - вычитание
            // 3 - константа
            // 5 - выход

            // Тип переменной
            // 3 - константа
            // 5 - выход
            // 1 - связь

            //#region Создаем элементы

            //Elems.Add(new Summer { ElemId = 1, Operation = 1 });
            //Elems.Add(new Subtraction { ElemId = 2, Operation = 2 });
            //Elems.Add(new Const { ElemId = 3, Operation = 3, ElemValue = "350" });
            //Elems.Add(new Const { ElemId = 4, Operation = 3, ElemValue = "100" });
            //Elems.Add(new Const { ElemId = 5, Operation = 3, ElemValue = "150" });
            //Elems.Add(new Exit { ElemId = 6, Operation = 5, ElemValue = "???????????" });


            //#endregion

            //#region Создаем связи

            //Junctions.Add(new Junction {JuncId = 1, FromElem = 3, FromPort = 1, ToElem = 1, ToPort = 1 });
            //Junctions.Add(new Junction { JuncId = 2, FromElem = 4, FromPort = 1, ToElem = 2, ToPort = 1 });
            //Junctions.Add(new Junction { JuncId = 3, FromElem = 5, FromPort = 1, ToElem = 2, ToPort = 2 });
            //Junctions.Add(new Junction { JuncId = 4, FromElem = 2, FromPort = 1, ToElem = 1, ToPort = 2 });
            //Junctions.Add(new Junction { JuncId = 5, FromElem = 1, FromPort = 1, ToElem = 6, ToPort = 1 });

            //#endregion

            int i1 = 1;
            int i2 = 2;
            int i3 = 3; 
            int i4 = 4;
            int i5 = 5;
            int i6 = 6;
            int j1 = 1;
            int j2 = 2;
            int j3 = 3;
            int j4 = 4;
            int j5 = 5;

            for (int i = 0; i < 20000; i++)
            {
                Elems.Add(new Summer { ElemId = i1, Operation = 1 });
                Elems.Add(new Subtraction { ElemId = i2, Operation = 2 });
                Elems.Add(new Const { ElemId = i3, Operation = 3, ElemValue = "350" });
                Elems.Add(new Const { ElemId = i4, Operation = 3, ElemValue = "100" });
                Elems.Add(new Const { ElemId = i5, Operation = 3, ElemValue = "150" });
                Elems.Add(new Exit { ElemId = i6, Operation = 5, ElemValue = "???????????" });
                Junctions.Add(new Junction { JuncId = j1, FromElem = i3, FromPort = 1, ToElem = i1, ToPort = 1 });
                Junctions.Add(new Junction { JuncId = j2, FromElem = i4, FromPort = 1, ToElem = i2, ToPort = 1 });
                Junctions.Add(new Junction { JuncId = j3, FromElem = i5, FromPort = 1, ToElem = i2, ToPort = 2 });
                Junctions.Add(new Junction { JuncId = j4, FromElem = i2, FromPort = 1, ToElem = i1, ToPort = 2 });
                Junctions.Add(new Junction { JuncId = j5, FromElem = i1, FromPort = 1, ToElem = i6, ToPort = 1 });
                i1 += 6;
                i2 += 6;
                i3 += 6;
                i4 += 6;
                i5 += 6;
                i6 += 6;
                j1 += 5;
                j2 += 5;
                j3 += 5;
                j4 += 5;
                j5 += 5;
            }

            /*
            #region Создали переменные

            foreach (var Elem in Elems)
            {

                if (Elem is Const) // Если константа
                {
                    id++;
                    Const CT = (Const)Elem;
                    Variables.Add(new Variable { VarId = id, VarType = 3, VarValue = CT.ElemValue});
                }
                if (Elem is Exit)
                {
                    Exit exit = (Exit)Elem;
                    id++;
                    Variables.Add(new Variable { VarId = id, VarType = Elem.Operation, VarValue = exit.ElemValue});
                }
            }
            foreach (var Junct in Junctions)
            {
                id++;
                Variables.Add(new Variable { VarId = id, VarType = 1, VarValue = $"link{Junct.JuncId}" });
            }

            #endregion

            #region Создали действия

            foreach (var Elem in Elems)
            {
                List<int> JunctInId = new List<int>();
                int JunctOutId = -1;
                List<int> VarInId = new List<int>();
                int VarOutId = -1;

                foreach (var Junction in Junctions)
                {
                    if (Junction.FromElem == Elem.ElemId)
                    {
                        JunctOutId = Junction.JuncId;
                    }
                    else if (Junction.ToElem == Elem.ElemId)
                    {
                        JunctInId.Add(Junction.JuncId);
                    }
                }

                if (JunctOutId != -1 && JunctInId.Count == 0) // Элемент по типу константы
                {
                    Const CT = (Const)Elem;
                    foreach (var Variable in Variables)
                    {

                        if (CT.ElemValue == Variable.VarValue)
                        {
                            VarInId.Add(Variable.VarId);
                        }
                        else if (Variable.VarValue.IndexOf("link") != -1)
                        {
                            if (JunctOutId == int.Parse(Variable.VarValue.Replace("link", "")))
                            {
                                VarOutId = Variable.VarId;
                            }
                        }
                    }
                    Actions.Add(new Action { Operation = Elem.Operation, In = VarInId, Out1 = VarOutId });
                }

                if (JunctOutId == -1 && JunctInId.Count == 1) // Элемент по типу выхода
                {
                    Exit exit = (Exit)Elem;
                    foreach (var Variable in Variables)
                    {
                        if (exit.ElemValue == Variable.VarValue)
                        {
                            VarOutId = Variable.VarId;
                            
                        }
                        else if (Variable.VarValue.IndexOf("link") != -1)
                        {
                            if (JunctInId[0] == int.Parse(Variable.VarValue.Replace("link", "")))
                            {
                                VarInId.Add(Variable.VarId);
                            }
                        }
                    }
                    Actions.Add(new Action { Operation = Elem.Operation, In = VarInId, Out1 = VarOutId });
                }

                if (JunctOutId != -1 && JunctInId.Count != 0 ) // Для остальных элементов
                {
                    foreach (var Variable in Variables) 
                    {
                        if (Variable.VarValue.IndexOf("link") != -1)
                        {
                            foreach (var item in JunctInId)
                            {
                                if (item == int.Parse(Variable.VarValue.Replace("link", "")))
                                {
                                    VarInId.Add(Variable.VarId);
                                }
                            }
                            if (JunctOutId == int.Parse(Variable.VarValue.Replace("link", "")))
                            {
                                VarOutId = Variable.VarId;
                            }
                        }
                    }
                    Actions.Add(new Action { Operation = Elem.Operation, In = VarInId, Out1 = VarOutId });
                }
            }

            #endregion
            */
            #region Создали переменные1
            for (int i = 0; i < 2000; i++)
            {
                for (int j = i * 60; j < i * 60 + 60; j++)
                {

                    if (Elems[j] is Const) // Если константа
                    {
                        id++;
                        Const CT = (Const)Elems[j];
                        Variables.Add(new Variable { VarId = id, VarType = 3, VarValue = CT.ElemValue });
                    }
                    if (Elems[j] is Exit)  // Выход
                    {
                        Exit exit = (Exit)Elems[j];
                        id++;
                        Variables.Add(new Variable { VarId = id, VarType = Elems[j].Operation, VarValue = exit.ElemValue });
                    }
                }
                for (int k = i * 50; k < i * 50 + 50; k++)  // Связь
                {
                    id++;
                    Variables.Add(new Variable { VarId = id, VarType = 1, VarValue = $"link{Junctions[k].JuncId}" });
                }
            }


            #endregion

            #region Создали действия1
            for (int i = 0; i < 2000; i++)
            {
                for (int j = i * 60; j < i * 60 + 60; j++)
                {
                    List<int> JunctInId = new List<int>();
                    int JunctOutId = -1;
                    List<int> VarInId = new List<int>();
                    int VarOutId = -1;

                    for (int k = i * 50; k < i * 50 + 50; k++)
                    {
                        if (Junctions[k].FromElem == Elems[j].ElemId)
                        {
                            JunctOutId = Junctions[k].JuncId;
                        }
                        else if (Junctions[k].ToElem == Elems[j].ElemId)
                        {
                            JunctInId.Add(Junctions[k].JuncId);
                        }
                    }

                    if (JunctOutId != -1 && JunctInId.Count == 0) // Элемент по типу константы
                    {
                        Const CT = (Const)Elems[j];
                        for (int v = i * 90; v < i * 90 + 90; v++)
                        {

                            if (CT.ElemValue == Variables[v].VarValue)
                            {
                                VarInId.Add(Variables[v].VarId);
                            }
                            else if (Variables[v].VarValue.IndexOf("link") != -1)
                            {
                                if (JunctOutId == int.Parse(Variables[v].VarValue.Replace("link", "")))
                                {
                                    VarOutId = Variables[v].VarId;
                                }
                            }
                        }
                        Actions.Add(new Action { Operation = Elems[j].Operation, In = VarInId, Out1 = VarOutId });
                    }

                    if (JunctOutId == -1 && JunctInId.Count == 1) // Элемент по типу выхода
                    {
                        Exit exit = (Exit)Elems[j];
                        for (int v = i * 90; v < i * 90 + 90; v++)
                        {
                            if (exit.ElemValue == Variables[v].VarValue)
                            {
                                VarOutId = Variables[v].VarId;

                            }
                            else if (Variables[v].VarValue.IndexOf("link") != -1)
                            {
                                if (JunctInId[0] == int.Parse(Variables[v].VarValue.Replace("link", "")))
                                {
                                    VarInId.Add(Variables[v].VarId);
                                }
                            }
                        }
                        Actions.Add(new Action { Operation = Elems[j].Operation, In = VarInId, Out1 = VarOutId });
                    }

                    if (JunctOutId != -1 && JunctInId.Count != 0) // Для остальных элементов
                    {
                        for (int v = i * 90; v < i * 90 + 90; v++)
                        {
                            if (Variables[v].VarValue.IndexOf("link") != -1)
                            {
                                foreach (var item in JunctInId)
                                {
                                    if (item == int.Parse(Variables[v].VarValue.Replace("link", "")))
                                    {
                                        VarInId.Add(Variables[v].VarId);
                                    }
                                }
                                if (JunctOutId == int.Parse(Variables[v].VarValue.Replace("link", "")))
                                {
                                    VarOutId = Variables[v].VarId;
                                }
                            }
                        }
                        Actions.Add(new Action { Operation = Elems[j].Operation, In = VarInId, Out1 = VarOutId });
                    }
                }
            }



            #endregion

            #region Присваивание ID всем действиям
            for (int i = 0; i < 2000; i++)
            {
                id = 0;
                HashSet<int> OutArr = new HashSet<int>();
                for (int j = i * 60; j < i * 60 + 60; j++)
                {
                    if (Actions[j].Operation == 3)
                    {
                        id++;
                        Actions[j].ActionId = id;
                        OutArr.Add(Actions[j].Out1);
                    }
                }
                while (OutArr.Count != 0)
                {
                    for (int j = i * 60; j < i * 60 + 60; j++)
                    {
                        if (Actions[j].ActionId == 0)
                        {
                            int IsExit = 0;
                            foreach (var In in Actions[j].In)
                            {
                                foreach (var Out in OutArr)
                                {
                                    if (Out == In)
                                    {
                                        IsExit++;
                                    }
                                }
                            }

                            if (IsExit == Actions[j].In.Count)
                            {
                                id++;
                                Actions[j].ActionId = id;
                                foreach (var In in Actions[j].In)
                                {
                                    OutArr.Remove(In);
                                }
                                if (Actions[j].Operation == 5)
                                {
                                    OutArr.Remove(Actions[j].Out1);
                                }
                                else
                                {
                                    OutArr.Add(Actions[j].Out1);
                                }

                            }
                        }
                    }
                }
            }
                
            
            #endregion


            System.TimeSpan ts = stopWatch.Elapsed;
            stopWatch.Stop();
            string elapsedTime = System.String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            System.Console.WriteLine("RunTime " + elapsedTime);

            #region Сортировка действий

            Actions.Sort((p1, p2) =>
            {
                return p1.ActionId.CompareTo(p2.ActionId);
            });

            #endregion
            
            //#region Решатель

            //foreach (var Action in Actions)
            //{
            //    if (Action.Operation == 3)
            //    {
            //        Variables[Action.Out1 - 1].SolvVar = float.Parse(Variables[Action.In[0]-1].VarValue);
            //    }
            //    else if (Action.Operation == 1)
            //    {
            //        Variables[Action.Out1 - 1].SolvVar = Variables[Action.In[0]-1].SolvVar + Variables[Action.In[1]-1].SolvVar;
            //    }
            //    else if (Action.Operation == 2)
            //    {
            //        Variables[Action.Out1 - 1].SolvVar = Variables[Action.In[0]-1].SolvVar - Variables[Action.In[1]-1].SolvVar;
            //    }
            //    else if (Action.Operation == 5)
            //    {
            //        Variables[Action.Out1 - 1].SolvVar = Variables[Action.In[0] - 1].SolvVar;
            //        //System.Console.WriteLine(Variables[Action.Out1 - 1].SolvVar);
            //    }
            //}

            //#endregion

        }
    }
}
