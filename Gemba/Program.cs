
using Gemba.ImageJunctionsObjects;
using Gemba.ImageElemsObjects;
using Gemba.ImageObjects;
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

            List<ImageElemObject> ImageElems = new List<ImageElemObject>(); // Список картинок-элементов на плоскости
            List<ImageJunctionObject> ImageJunctions = new List<ImageJunctionObject>(); // Список картинок-связей на плоскости
            List<Elem> Elems = new List<Elem>(); // Список расчетных элементов
            List<Junction> Junctions = new List<Junction>(); // Список связей

            List<Junction> ConstJunctions = new List<Junction>(); // Здесь записаны все связи, у которых левый элемент - константа

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

            #region Создаем картинки элементов на рабочей плоскости

            ImageSummerElem Sum1 = new ImageSummerElem(ref Elems) { ImageElemId = ImageElems.Count + 1};
            Sum1.SetParamsToElem();
            ImageElems.Add(Sum1);

            ImageSubstructionElem Minus1 = new ImageSubstructionElem(ref Elems) { ImageElemId = ImageElems.Count + 1};
            Minus1.SetParamsToElem();
            ImageElems.Add(Minus1);

            ImageConstElem Const1 = new ImageConstElem(ref Elems) { ImageElemId = ImageElems.Count + 1, ImageElemValue = "200"};
            Const1.SetParamsToElem();
            ImageElems.Add(Const1);

            ImageConstElem Const2 = new ImageConstElem(ref Elems) { ImageElemId = ImageElems.Count + 1, ImageElemValue = "100" };
            Const2.SetParamsToElem();
            ImageElems.Add(Const2);

            ImageConstElem Const3 = new ImageConstElem(ref Elems) { ImageElemId = ImageElems.Count + 1, ImageElemValue = "50" };
            Const3.SetParamsToElem();
            ImageElems.Add(Const3);

            ImageExitElem Exit1 = new ImageExitElem(ref Elems) { ImageElemId = ImageElems.Count + 1, ImageElemValue = "????????" };
            Exit1.SetParamsToElem();
            ImageElems.Add(Exit1);

            #endregion

            #region Создаем картинки связей на рабочей плоскости

            ImageJunctionObject Junction1 = new ImageJunctionObject(ref Junctions)
            {
                ImageJunctId = ImageJunctions.Count + 1,
                ImageJunctFromElemId = 3,
                ImageJunctFromPortId = 1,
                ImageJunctToElemId = 1,
                ImageJunctToPortId = 1,
                ImageJunctType = 1488
            };
            Junction1.SetParamsToJunction(ref Elems, ref ConstJunctions);
            ImageJunctions.Add(Junction1);

            ImageJunctionObject Junction2 = new ImageJunctionObject(ref Junctions)
            {
                ImageJunctId = ImageJunctions.Count + 1,
                ImageJunctFromElemId = 4,
                ImageJunctFromPortId = 1,
                ImageJunctToElemId = 2,
                ImageJunctToPortId = 1,
                ImageJunctType = 1488
            };
            Junction2.SetParamsToJunction(ref Elems, ref ConstJunctions);
            ImageJunctions.Add(Junction2);

            ImageJunctionObject Junction3 = new ImageJunctionObject(ref Junctions)
            {
                ImageJunctId = ImageJunctions.Count + 1,
                ImageJunctFromElemId = 5,
                ImageJunctFromPortId = 1,
                ImageJunctToElemId = 2,
                ImageJunctToPortId = 2,
                ImageJunctType = 1488
            };
            Junction3.SetParamsToJunction(ref Elems, ref ConstJunctions);
            ImageJunctions.Add(Junction3);

            ImageJunctionObject Junction4 = new ImageJunctionObject(ref Junctions)
            {
                ImageJunctId = ImageJunctions.Count + 1,
                ImageJunctFromElemId = 2,
                ImageJunctFromPortId = 1,
                ImageJunctToElemId = 1,
                ImageJunctToPortId = 2,
                ImageJunctType = 1488
            };
            Junction4.SetParamsToJunction(ref Elems, ref ConstJunctions);
            ImageJunctions.Add(Junction4);

            ImageJunctionObject Junction5 = new ImageJunctionObject(ref Junctions)
            {
                ImageJunctId = ImageJunctions.Count + 1,
                ImageJunctFromElemId = 1,
                ImageJunctFromPortId = 1,
                ImageJunctToElemId = 6,
                ImageJunctToPortId = 1,
                ImageJunctType = 1488
            };
            Junction5.SetParamsToJunction(ref Elems, ref ConstJunctions);
            ImageJunctions.Add(Junction5);



            #endregion

            #region Пишем действия

            foreach (var ConstJunction in ConstJunctions) // Надо сначала создать переменные
            {
                Actions.Add(new Action { Operation = 3, In = new List<int>() { Elems[ConstJunction.FromElem].ElemId }, Out1 = Elems[ConstJunction.ToElem].ElemId });
            }

            #endregion



            // <----------------------------------------------- Старый макет ----------------------------------------------->

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

            #region Присваивание ID всем действиям

            id = 0;
            List<int> OutArr = new List<int>();
            foreach (var Action in Actions)
            {
                if (Action.Operation == 3)
                {
                    id++;
                    Action.ActionId = id;
                    OutArr.Add(Action.Out1);
                }
            }
            while (OutArr.Count != 0)
            {
                foreach (var Action in Actions)
                {
                    if (Action.ActionId == 0)
                    {
                        int IsExit = 0;
                        foreach (var In in Action.In)
                        {
                            foreach (var Out in OutArr)
                            {
                                if (Out == In)
                                {
                                    IsExit++;
                                }
                            }
                        }

                        if (IsExit == Action.In.Count)
                        {
                            id++;
                            Action.ActionId = id;
                            foreach (var In in Action.In)
                            {
                                OutArr.Remove(In);
                            }
                            if (Action.Operation == 5)
                            {
                                OutArr.Remove(Action.Out1);
                            }
                            else
                            {
                                OutArr.Add(Action.Out1);
                            }
                            
                        }
                    }
                }
            }

            #endregion

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
            System.TimeSpan ts = stopWatch.Elapsed;
            stopWatch.Stop();
            string elapsedTime = System.String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            System.Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
